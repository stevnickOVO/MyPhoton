using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGameOutCollider : MonoBehaviour
{
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Player")
        {
            gameObject.transform.parent.GetComponent<GameManger>().playerOut(other.gameObject);
        }
    }
}
