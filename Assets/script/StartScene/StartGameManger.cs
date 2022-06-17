using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class StartGameManger : MonoBehaviourPunCallbacks
{
    [SerializeField] GameObject[] backGroundList;



    // Start is called before the first frame update
    private void Awake()
    {
        backGroundRandom();
    }
    void Start()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        
        /*
        if (PhotonNetwork.IsConnected == false)
        {
            SceneManager.LoadScene("StartScene");
        }else PhotonNetwork.JoinLobby();
        */


    }
    public void OnClickStart()
    {
        PhotonNetwork.ConnectUsingSettings();
    }
    public override void OnConnectedToMaster()
    {
        SceneManager.LoadScene("LobbyScene");
    }
    public void backGroundRandom()
    {
        if (backGroundList.Length > 0)
        {
            int ListMax = backGroundList.Length;
            int random = Random.Range(0, ListMax);
            backGroundList[random].gameObject.SetActive(true);
        }
    }

    public void OnChickExit()
    {
        Application.Quit();
    }
}
