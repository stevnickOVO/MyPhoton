using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using Photon.Realtime;

public class GameManger : MonoBehaviourPunCallbacks
{
    [SerializeField] GameObject [] playerCharacterList;
    [SerializeField] TMP_Text TimeText;
    [SerializeField]public float startTime;
    [SerializeField] float roundTime;

    [SerializeField] GameObject ballPrefab;
    [SerializeField] GameObject ballStartPoint;
    [SerializeField] GameObject redTeam;
    [SerializeField] GameObject blueTeam;
    [SerializeField] GameObject[] redTeamPoint;
    [SerializeField] GameObject[] blueTeanPoint;
    [SerializeField] TMP_Text BlueSorceText;
    [SerializeField] int BlueSorce;
    [SerializeField] TMP_Text RedSorceText;
    [SerializeField] int RedSorce;
    [SerializeField] GameObject gameEndUI;
    [SerializeField] TMP_Text winText;

    // Start is called before the first frame update
    void Start()
    {

        if (PhotonNetwork.CurrentRoom == null)
        {
            SceneManager.LoadScene("LobbyScene");
        }
        else CreatePlayer();
        gameEndUI.SetActive(false);
        createBall();
    }

    // Update is called once per frame
    void Update()
    {
        StartTimeControllor();
    }
    public void CreatePlayer()
    {
        if (PhotonNetwork.LocalPlayer.team)
        {
            for (int a = 0; a < blueTeanPoint.Length ; a++)
            {
                GameObject player=PhotonNetwork.Instantiate(playerCharacterList[PhotonNetwork.LocalPlayer.CharaNumber].name, blueTeanPoint[a].transform.position, Quaternion.EulerAngles(0,-180,0));
                blueTeam.GetComponent<TeamControllor>().teamList.Add(player);
            }
            blueTeam.GetComponent<TeamControllor>().CamraOpen();
        }
        else
        {
            for (int a=0;a< redTeamPoint.Length;a++)
            {
                GameObject player = PhotonNetwork.Instantiate(playerCharacterList[PhotonNetwork.LocalPlayer.CharaNumber].name, redTeamPoint[a].transform.position, Quaternion.identity);
                redTeam.GetComponent<TeamControllor>().teamList.Add(player);
            }
            redTeam.GetComponent<TeamControllor>().CamraOpen();
        }
    }
    public void StartTimeControllor()
    {
        if (startTime <= 0)
        {
            roundTime -= Time.deltaTime;
            int tText = (int)roundTime;
            TimeText.text = tText.ToString();
            if (roundTime <= 0)
            {
                GameOver();
            }
        }
        else
        {
            startTime -= Time.deltaTime;
            int tText = (int)startTime;
            if (startTime <= 1)
            {
                TimeText.text = "Start!!";
            }
            else
            {
                TimeText.text = tText.ToString();
            }
        }
    }
    public void playerOut(GameObject playerObject)
    {
        PhotonNetwork.Destroy(playerObject);
    }
    public void BallShootInGoal(GameObject ball,bool teamColor)
    {
        PhotonNetwork.Destroy(ball);
        createBall();

        if (teamColor)
        {
            BlueSorce++;
            BlueSorceText.text = BlueSorce.ToString();
        }
        else
        {
            RedSorce++;
            RedSorceText.text = RedSorce.ToString();
        }
    }
    public void createBall()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.Instantiate("Ball", ballStartPoint.transform.position, Quaternion.identity);
        }
    }
    public void EndGameText()
    {
        if (BlueSorce > RedSorce)
        {
            winText.text = "Blue Win!!";
        }
        else if (BlueSorce < RedSorce)
        {
            winText.text = "Red Win!!";
        }
        else 
        {
            winText.text = "Draw";
        }

    }
    public void GameOver()
    {
        gameEndUI.SetActive(true);
        EndGameText();
        Time.timeScale = 0;
    }
    public void OnClickEndGame()
    {
        Time.timeScale = 1;
        gameEndUI.SetActive(false);
        Application.Quit();
    }
}
