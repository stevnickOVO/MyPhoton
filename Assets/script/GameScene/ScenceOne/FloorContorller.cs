using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorContorller : MonoBehaviour
{
    public bool isGameStart;
    [SerializeField] Camera camera;
    private void Update()
    {
        if (isGameStart)
        {
            transform.localScale = new Vector3(transform.localScale.x- Time.deltaTime,1, transform.localScale.z - Time.deltaTime);
            camera.orthographicSize = camera.orthographicSize - Time.deltaTime;
        }
    }
}
