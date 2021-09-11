using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Transform playerTransform;
    public float minX, maxX;

    void Start()
    {
        playerTransform = GameObject.Find("Player").transform; // Player objesinin transformu bulunur. 
    }

    void Update()
    {
        transform.position = new Vector3(Mathf.Clamp(playerTransform.position.x,minX,maxX),transform.position.y,transform.position.z);
        
    }
}
