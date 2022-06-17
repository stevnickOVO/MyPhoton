using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GoalContorllor : MonoBehaviour
{
    public enum teamColor { red,bule}
    public teamColor myTeamColor;
    public bool witchTeam;

    [SerializeField] GameManger gameManger;
    private void Start()
    {
        gameManger = GameObject.Find("GameManger").GetComponent<GameManger>();
        switch (myTeamColor)
        {
            case teamColor.bule:
                witchTeam = true;
                break;
            case teamColor.red:
                witchTeam = false;
                break;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ball")
        {
            gameManger.BallShootInGoal(other.gameObject,witchTeam);
        }
    }
}
