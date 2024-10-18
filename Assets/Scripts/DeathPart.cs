using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPart : MonoBehaviour
{
    private AudioSource audioSource;

    //Attach the witch scream sound to death part
    //public AudioClip witchScreamClip; //this is the scream sound
    public void OnEnable()
    {
        // Get the AudioSource component assigned at runtime
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        //audioSource.clip = (AudioClip)Resources.Load("BeepSound");
        //audioSource.clip;// assign your witchScreamClip here (from HelixController or GameManager);
       audioSource.playOnAwake = false; // Ensure it doesn't play on spawn// Use a simple test sound


        GetComponent<Renderer>().material.color = Color.red;     

    }

    //Die on touch death part
    public void HitDeathPart()
    {
        Debug.Log("HitDeathPart called!");  // Add this for debug
        if (audioSource != null)
        {
            Debug.Log("Playing sound");
            audioSource.Play();  // Play the scream sound
        }
        else
        {
            Debug.LogError("AudioSource is null");
        }

        GameManager.singleton.RestartLevel();
    }

}
