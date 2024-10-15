using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    private bool ignoreNextCollision;
    public Rigidbody rb;
    public float impulseForce = 5f;
    private Vector3 startPosition;


    void Awake()
    {
        startPosition = transform.position;
    }

    //When collision is detected
    private void OnCollisionEnter(Collision collision)
    {
        if (ignoreNextCollision)
            return;

        //Check if ball has colided with the death part and reset level
        DeathPart deathPart = collision.transform.GetComponent<DeathPart>();
        if (deathPart)
        {
            deathPart.HitDeathPart();
        }


        rb.velocity = Vector3.zero;
        rb.AddForce(Vector3.up * impulseForce, ForceMode.Impulse); // add instant force to the ball

        ignoreNextCollision = true;
        Invoke("AllowCollision", .2f);

        //test code to add score when ball touches something
        //GameManager.singleton.AddScore(1);
        //Debug.Log(GameManager.singleton.score);
    }

    private void AllowCollision()
    {
        ignoreNextCollision = false;
    }

    //reset ball method
    public void ResetBall()
    {
        transform.position = startPosition;
    }

}
