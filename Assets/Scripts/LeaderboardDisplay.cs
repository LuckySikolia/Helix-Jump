using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardDisplay : MonoBehaviour
{
    //Text objects for showing leaderboard (3 for top players)
    public TMP_Text[] leaderboardTextObjects;
    //reference to the offlineLeaderBoard
    public OfflineLeaderboard LeaderboardManager;
    //reference the leaderboard panel
    public GameObject leaderboardPanel;
    //public Button restartButton;

    private void Start()
    {
        //ensure the panel is inactive at start
        leaderboardPanel.SetActive(false);
    }

    public void ShowLeaderboard()
    {
        leaderboardPanel.SetActive(true);
        UpdateLeaderboardUI();

        //pause game
        GameManager.singleton.PauseGame();
    }

    public void HideLeaderboard()
    {
        leaderboardPanel.SetActive(false); // Hide the leaderboard
        //restartButton.gameObject.SetActive(false); // Optionally hide the restart button

        //resume Game
        GameManager.singleton.ResumeGame();
    }

    public void UpdateLeaderboardUI()
    {
        //load the current leaderboard entries
        LeaderboardManager.LoadLeaderboard();

        for (int i = 0; i < leaderboardTextObjects.Length; i++)
        {
            if(i < LeaderboardManager.leaderboardEntries.Count)
            {
                leaderboardTextObjects[i].text = $"{LeaderboardManager.leaderboardEntries[i].playerName}: {LeaderboardManager.leaderboardEntries[i].score}";
            }
            else
            {
                //clear the unsued text fields if there are fewer entries
                leaderboardTextObjects[i].text = "";
            }
        }
    }
}
