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


        //reset score
        singleton.score = 0;
        //reset the ball
        FindObjectOfType<BallController>().ResetBall();
        //reload the stage
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
            PlayerPrefs.SetInt("HighScore", score);
            
        }
    }


}
