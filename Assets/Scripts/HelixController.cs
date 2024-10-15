using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelixController : MonoBehaviour
{
    //know last screen tap
    private Vector2 lastTapPosition;
    private Vector3 startRotation;


    // Start is called before the first frame update
    void Awake()
    {
        startRotation = transform.localEulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        //for touchscreen
        if (Input.GetMouseButton(0))
        {
            Vector2 currentTapPosition = Input.mousePosition;

            if(lastTapPosition == Vector2.zero)
            {
                lastTapPosition = currentTapPosition;
            }

            float delta = lastTapPosition.x - currentTapPosition.x;
            lastTapPosition = currentTapPosition;

            transform.Rotate(Vector3.up * delta);
        }

        if (Input.GetMouseButtonUp(0))
        {
            lastTapPosition = Vector2.zero;
        }
    }
}
