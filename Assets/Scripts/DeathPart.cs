using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPart : MonoBehaviour
{
    //change colour
    private void OnEnable()
    {
        GetComponent<Renderer>().material.color = Color.red;
    }

    //Die on touch death part
    public void HitDeathPart()
    {
        GameManager.singleton.RestartLevel();
    }

}
