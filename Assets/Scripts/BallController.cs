using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    private bool ignoreNextCollision;
    public Rigidbody rb;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    //When collision is detected
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Ball has collided with the helix");
        rb.velocity = Vector3.zero;
        rb.AddForce(Vector3.up, ForceMode.Impulse); // add instant force to the ball
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
