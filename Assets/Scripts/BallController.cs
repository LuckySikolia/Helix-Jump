using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    private bool ignoreNextCollision;
    public Rigidbody rb;
    public float impulseForce = 5f; 



    // Start is called before the first frame update
    void Start()
    {
        
    }

    //When collision is detected
    private void OnCollisionEnter(Collision collision)
    {
        if (ignoreNextCollision)
            return;

        Debug.Log("Ball has collided with the helix");

        rb.velocity = Vector3.zero;
        rb.AddForce(Vector3.up * impulseForce, ForceMode.Impulse); // add instant force to the ball

        ignoreNextCollision = true;
        Invoke("AllowCollision", .2f);


    }

    private void AllowCollision()
    {
        ignoreNextCollision = false;
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
