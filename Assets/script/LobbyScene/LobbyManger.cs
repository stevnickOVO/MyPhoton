using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using Photon.Realtime;
using System.Text;

public class LobbyManger : MonoBehaviourPunCallbacks
{
    [SerializeField] TMP_InputField PlayerName;
    [SerializeField] TMP_InputField inputRoomName;
    [SerializeField] TMP_Text RoomNameListText;

    [SerializeField] GameObject characterWatchPoint;
    [SerializeField] characterTableObject[] characterTablesList;
    [HideInInspector] public GameObject CurrCharacter;
    private int ChNumber;
    private void Start()
    {
        if (PhotonNetwork.IsConnected == false)
        {
            SceneManager.LoadScene("StartScene");
        }
        else 
        {
            if (PhotonNetwork.CurrentLobby == null)
            {
                PhotonNetwork.JoinLobby();
            }
        }
        

        ChNumber = 0;
        CurrCharacter = characterTablesList[ChNumber].characterObject;
        Instantiate(CurrCharacter, characterWatchPoint.transform.position, Quaternion.EulerRotation(0, 140, 0), gameObject.transform);
        
    }
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }
    public override void OnJoinedLobby()
    {
        print("加入大廳");
    }
    public void OnClickCreateRoom()
    {
        if (inputRoomName.text.Length > 0&&PlayerName.text.Length>0)
        {
            PhotonNetwork.LocalPlayer.NickName = PlayerName.text;
            PhotonNetwork.CreateRoom(inputRoomName.text);

            PhotonNetwork.LocalPlayer.CharaNumber = ChNumber;
        }
    }
    public void OnClickJoinRoom()
    {
        if (inputRoomName.text.Length > 0 && PlayerName.text.Length > 0)
        {
            PhotonNetwork.LocalPlayer.NickName = PlayerName.text;
            PhotonNetwork.JoinRoom(inputRoomName.text);

            PhotonNetwork.LocalPlayer.CharaNumber = ChNumber;
        }
    }
    public override void OnJoinedRoom()
    {
        print("加入房間");
        PhotonNetwork.LocalPlayer.CharaNumber = ChNumber;
        SceneManager.LoadScene("RoomScene");
    }
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        StringBuilder sb = new StringBuilder();
        foreach (RoomInfo allRoomList in roomList)
        {
            if (allRoomList.PlayerCount > 0)
            {
                sb.AppendLine("-" + allRoomList.Name + "(" + allRoomList.PlayerCount + "/6)");
            }
        }
        RoomNameListText.text = sb.ToString();
    }
    public void OnChickCharacter()
    {
        ChNumber++;
        if (ChNumber > characterTablesList.Length - 1)
        {
            ChNumber = 0;
        }
        Destroy(transform.GetChild(0).gameObject);
        CurrCharacter = characterTablesList[ChNumber].characterObject;
        Instantiate(CurrCharacter, characterWatchPoint.transform.position, Quaternion.EulerRotation(0, 140, 0), gameObject.transform);
    }
    public void OnClickExitLobby()
    {
        SceneManager.LoadScene("StartScene");
    }
}
