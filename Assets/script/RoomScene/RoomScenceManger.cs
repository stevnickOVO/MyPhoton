using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using TMPro;
using System.Text;
using Photon.Realtime;
using UnityEngine.UI;

public class RoomScenceManger : MonoBehaviourPunCallbacks
{
    [SerializeField] TMP_Text RoomName;
    [SerializeField] TMP_Text TeamText;

    [SerializeField] Button RedButton;
    [SerializeField] Button BlueButton;
    [SerializeField] Button StartButton;
    // Start is called before the first frame update
    void Start()
    {
        if (PhotonNetwork.CurrentRoom == null)
        {
            SceneManager.LoadScene("LobbyScene");
        }
        else 
        {
            RoomName.text = PhotonNetwork.CurrentRoom.Name;
            UpdatePlayerList();
        }
        StartButton.interactable = PhotonNetwork.IsMasterClient;

        TeamButtonControllor();
    }

    public void UpdatePlayerList()
    {
        StringBuilder sb = new StringBuilder();
        foreach (var kvp in PhotonNetwork.CurrentRoom.Players)
        {
            sb.AppendLine("="+kvp.Value.NickName);
        }
        TeamText.text = sb.ToString();
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        UpdatePlayerList();
    }
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        UpdatePlayerList();
        StartButton.interactable = PhotonNetwork.IsMasterClient;
    }
    public void OnClickStartGame()
    {
        SceneManager.LoadScene("GameScene");
    }
    public void switchBlueTeam()
    {
        PhotonNetwork.LocalPlayer.team = true;
        TeamButtonControllor();
    }
    public void switchRadTeam()
    {
        PhotonNetwork.LocalPlayer.team = false;
        TeamButtonControllor();
    }
    public void OnClickExit()
    {
        PhotonNetwork.LeaveRoom();
    }
    public override void OnLeftRoom()
    {
        SceneManager.LoadScene("LobbyScene");
    }
    public void TeamButtonControllor()
    {
        if (PhotonNetwork.LocalPlayer.team)
        {
            BlueButton.gameObject.SetActive(true);
            RedButton.gameObject.SetActive(false);
        }
        else 
        {
            BlueButton.gameObject.SetActive(false);
            RedButton.gameObject.SetActive(true);
        }
    }
}
