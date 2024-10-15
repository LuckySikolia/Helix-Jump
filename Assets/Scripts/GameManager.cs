using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int bestScore;
    public int score;

    public int currentStage = 0;
    public static GameManager singleton;

    private void Awake()
    {
        if(singleton == null)
        {
            singleton = this;
        }
        else if(singleton != this)
        {
            Destroy(gameObject);
        }

        //retrieve the stored value
        bestScore = PlayerPrefs.GetInt("HighScore");
    }

    public void NextLevel()
    {
        currentStage++;
        FindObjectOfType < BallController>().ResetBall();
        FindObjectOfType<HelixController>().LoadStage(currentStage);
        Debug.Log("Next Level called");

    }

    public void RestartLevel()
    {
        Debug.Log("Game Over");
        //show ads here
        //reset score
        singleton.score = 0;
        //reset the ball
        FindObjectOfType<BallController>().ResetBall();
        //reload the stage
        FindObjectOfType<HelixController>().LoadStage(currentStage);
    }

    public void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;

        if(score > bestScore)
        {
            bestScore = score;
            //score high store / best score in player prefs
            PlayerPrefs.SetInt("HighScore", score);
            
        }
    }


}
