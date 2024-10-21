using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;
using UnityEngine.Advertisements;




//public class GameManager : MonoBehaviour, IUnityAdsInitializationListener
public class GameManager : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener

{
    public int bestScore;
    public int score;
    public int currentStage = 0;

    public static GameManager singleton;


    [SerializeField] string _androidGameId;
    [SerializeField] string _iOSGameId;
    [SerializeField] bool _testMode = true;
    private string _gameId;

    private string _adUnitId = "Interstitial_Android"; // Update this to your actual Ad Unit ID

    //references for leaderboard
    public OfflineLeaderboard LeaderboardManager;
    public LeaderboardDisplay leaderboardDisplay;


    private void Awake()
    {
        if (singleton == null)
        {
            singleton = this;
        }
        else if(singleton != this)
        {
            Destroy(gameObject);
        }

        //retrieve the stored value
        bestScore = PlayerPrefs.GetInt("HighScore");

        InitializeAds();
    }

    // Method to initialize Unity Ads
    public void InitializeAds()
    {
        #if UNITY_IOS
            _gameId = _iOSGameId;
        #elif UNITY_ANDROID
            _gameId = _androidGameId;
#elif UNITY_EDITOR
        _gameId = _androidGameId; //Only for testing the functionality in the Editor
#endif
        if (!Advertisement.isInitialized && Advertisement.isSupported)
        {
            Advertisement.Initialize(_gameId, _testMode, this);
        }
    }

    // Called when Unity Ads initialization completes successfully
    public void OnInitializationComplete()
    {
        Debug.Log("Unity Ads initialization complete.");
        // Load an ad after initialization
        Advertisement.Load(_adUnitId, this);
    }

    // Called when Unity Ads initialization fails
    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
    }

    // Method to load an ad
    public void OnUnityAdsAdLoaded(string placementId)
    {
        Debug.Log("Ad Loaded: " + placementId);
    }








    // Method to load the next level
    public void NextLevel()
    {
        currentStage++;
        FindObjectOfType < BallController>().ResetBall();
        FindObjectOfType<HelixController>().LoadStage(currentStage);
        Debug.Log("Next Level called");

    }

    // Method to restart the level
    public void RestartLevel()
    {
        Debug.Log("Game Over: Show Ads");
        //show ads here
        if (Advertisement.isInitialized)
        {
            Advertisement.Show(_adUnitId, this);
        }

        //add the score to the leaderboard
        //replace player with the actual player name
        //TODO! dynamically set the player name
        LeaderboardManager.AddNewScore("Player", bestScore);
        // Update the UI to reflect new scores
        leaderboardDisplay.UpdateLeaderboardUI();
        // Show the leaderboard UI
        if (leaderboardDisplay != null)
        {
            leaderboardDisplay.ShowLeaderboard(); // Call the method to show the leaderboard
        }
        // Start the coroutine to reset the game after a delay
        StartCoroutine(RestartLevelCoroutine());


        ////reset score
        //singleton.score = 0;
        ////reset the ball
        //FindObjectOfType<BallController>().ResetBall();
        ////reload the stage
        //FindObjectOfType<HelixController>().LoadStage(currentStage);
    }

    private IEnumerator RestartLevelCoroutine()
    {
        // Wait for a few seconds to allow the game over screen and ads to display
        yield return new WaitForSeconds(3f); // Adjust the duration as necessary

        //// Reset score
        //singleton.score = 0;

        //// Reset the ball
        //FindObjectOfType<BallController>().ResetBall();

        //// Reload the stage
        //FindObjectOfType<HelixController>().LoadStage(currentStage);
    }

    public void RestartLevelMethod()
    {
        Debug.Log("Restarting Level...");

        // Hide the leaderboard when restarting
        if (leaderboardDisplay != null)
        {
            leaderboardDisplay.HideLeaderboard(); // Call the method to hide the leaderboard
        }

        // Reset score and reload the stage
        singleton.score = 0;
        FindObjectOfType<BallController>().ResetBall();
        FindObjectOfType<HelixController>().LoadStage(currentStage);
    }









    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        Debug.Log("Ad Finished Showing: " + placementId);
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Ad Failed to Load: {placementId} - {error.ToString()} - {message}");
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        Debug.Log($"Ad Show Failed: {placementId} - {error.ToString()} - {message}");
    }

    public void OnUnityAdsShowStart(string placementId)
    {
        Debug.Log("Ad Started Showing: " + placementId);
    }

    public void OnUnityAdsShowClick(string placementId)
    {
        Debug.Log("Ad Clicked: " + placementId);
    }


    public void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;

        if(score > bestScore)
        {
            bestScore = score;
            //score high store / best score in player prefs
            //PlayerPrefs.SetInt("HighScore", bestScore);
            
        }
    }

    public int GetScore()
    {
        return score;
    }


    public void PauseGame()
    {
        Time.timeScale = 0; // Pause the game
    }

    public void ResumeGame()
    {
        Time.timeScale = 1; // Resume the game
    }
}
