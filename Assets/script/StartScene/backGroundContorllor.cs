using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backGroundContorllor : MonoBehaviour
{
    [SerializeField] float Speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition += new Vector3(-Speed,0,0);
        if (transform.localPosition.x <= -1600)
        {
            transform.localPosition=new Vector3(1600,0,0);
        }
    }
}
