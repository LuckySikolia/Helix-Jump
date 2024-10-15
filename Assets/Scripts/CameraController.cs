using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public BallController target;
    private float offset;


    void Awake()
    {
        offset = transform.position.y - target.transform.position.y;
    }

    void Update()
    {
        Vector3 currentPosition = transform.position;
        currentPosition.y = target.transform.position.y + offset;  //always maintain distance to the ball
        transform.position = currentPosition;

    }


}


    
