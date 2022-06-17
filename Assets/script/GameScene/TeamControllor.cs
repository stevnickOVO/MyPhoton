using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class TeamControllor : MonoBehaviourPunCallbacks
{
    //[SerializeField] public GameObject[] teamList=new GameObject[4];
    [SerializeField] public List<GameObject> teamList = new List<GameObject>();
    private PhotonView PV;
    private int currPlayer;
    private void Update()
    {
        PlayerChange();
    }
    public void PlayerChange()
    {
        try
        {
            if (Input.GetKeyUp(KeyCode.Q) && PV.IsMine)
            {
                teamList[currPlayer].GetComponent<PlayerCotorllor>().isControllor = false;
                teamList[currPlayer].GetComponent<PlayerCotorllor>().camraObject.SetActive(false);
                if (currPlayer >= teamList.Count - 1)
                {
                    currPlayer = 0;
                }
                else currPlayer++;

                teamList[currPlayer].GetComponent<PlayerCotorllor>().isControllor = true;
                teamList[currPlayer].GetComponent<PlayerCotorllor>().camraObject.SetActive(true);
            }
        }
        catch 
        {
            print("Nothing");
        }
        
    }
    public void CamraOpen()
    {
        currPlayer = teamList.Count - 1;
        teamList[currPlayer].GetComponent<PlayerCotorllor>().isControllor = true;
        teamList[currPlayer].GetComponent<PlayerCotorllor>().camraObject.SetActive(true);

        PV = teamList[currPlayer].GetComponent<PlayerCotorllor>().pv;
    }
}
