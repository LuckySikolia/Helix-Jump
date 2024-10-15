using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    private bool ignoreNextCollision;
    public Rigidbody rb;
    public float impulseForce = 5f;
    private Vector3 startPosition;
    public int perfectPass = 0;
    public bool isSupperSpeedActive;


    void Awake()
    {
        startPosition = transform.position;
    }

    //When collision is detected
    private void OnCollisionEnter(Collision collision)
    {
        if (ignoreNextCollision)
            return;

        if (isSupperSpeedActive)
        {
            if (!collision.transform.GetComponent<Goal>())
            {
                Destroy(collision.transform.parent.gameObject);
                Debug.Log("Destory platform");
            }
        }
        else
        {
            //Check if ball has colided with the death part and reset level
            DeathPart deathPart = collision.transform.GetComponent<DeathPart>();
            if (deathPart)
            {
                deathPart.HitDeathPart();
            }
        }

        


        rb.velocity = Vector3.zero;
        rb.AddForce(Vector3.up * impulseForce, ForceMode.Impulse); // add instant force to the ball

        ignoreNextCollision = true;
        Invoke("AllowCollision", .2f);


        perfectPass = 0; //when we hit something reset
        isSupperSpeedActive = false;

        //test code to add score when ball touches something
        //GameManager.singleton.AddScore(1);
        //Debug.Log(GameManager.singleton.score);
    }

    private void Update()
    {
        if(perfectPass >= 3 && isSupperSpeedActive)
        {
            isSupperSpeedActive = true;
            rb.AddForce(Vector3.down * 10, ForceMode.Impulse);
        }
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


    //super speed
}
