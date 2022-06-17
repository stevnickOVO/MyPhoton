using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerProperty : MonoBehaviour
{
    [SerializeField]public PhotonView pv;
    [SerializeField]public string PlayerName;
    [SerializeField]public characterTableObject PlayerObject;
    [SerializeField]public bool YouTeam;
    private void Start()
    {
        pv = GetComponent<PhotonView>();
        DontDestroyOnLoad(this.gameObject);
    }
}
