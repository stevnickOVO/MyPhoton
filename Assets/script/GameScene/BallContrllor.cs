using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallContrllor : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] GameObject StartPoint;
    [SerializeField] float speed;

    [SerializeField] GameManger gameManager;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        gameManager = GameObject.Find("GameManger").gameObject.GetComponent<GameManger>();
        StartPoint = gameManager.gameObject.transform.Find("StartPoint").gameObject;
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Goal")
        {
            
        }
    }
}
