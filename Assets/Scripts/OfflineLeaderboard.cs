using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script to create an offline leaderboard using PlayerPrefs

//defining the leaderboard data structure
[System.Serializable]
public class LeaderboardEntry
{
    public string playerName;
    public int score;

    public LeaderboardEntry(string name, int score)
    {
        playerName = name;
        this.score = score;
    }
}
public class OfflineLeaderboard : MonoBehaviour
{
    //storing the leaderboard in PlayerPrefs
    private const string LeaderboardKey = "Leaderboard";

    //max number of entries
    public int maxEntries = 3;
    public List<LeaderboardEntry> leaderboardEntries = new List<LeaderboardEntry>();

    void Start()
    {
        //load the leaderboard when the game starts
        LoadLeaderboard();
    }

    public void AddNewScore(string playerName, int score)
    {
        //create a new entry
        LeaderboardEntry newEntry = new LeaderboardEntry(playerName, score);

        //add the entry to the list
        leaderboardEntries.Add(newEntry);

        //sort the entries by score(highest first)
        leaderboardEntries.Sort((x, y) => y.score.CompareTo(x.score));

        //trim the leaderboard to the top entries
        if(leaderboardEntries.Count > maxEntries)
        {
            leaderboardEntries.RemoveAt(leaderboardEntries.Count - 1);
        }

        //save the updated leaderboard
        SaveLeaderboard();
    }

    public void SaveLeaderboard()
    {
        //serialize the leaderboard to a JSON string
        string json = JsonUtility.ToJson(new LeaderboardWrapper(leaderboardEntries));

        //save it using PlayerPrefs
        PlayerPrefs.SetString(LeaderboardKey, json);
        PlayerPrefs.Save();
    }

    public void LoadLeaderboard()
    {
        if (PlayerPrefs.HasKey(LeaderboardKey))
        {
            //load the leaderboard JSON string from PlayerPrefs
            string json = PlayerPrefs.GetString(LeaderboardKey);

            //deserialize the json back into a list of leaderboard entries
            leaderboardEntries = JsonUtility.FromJson<LeaderboardWrapper>(json).leaderboardEntries;
        }
    }

    public void ResetAllScores()
    {
        leaderboardEntries.Clear(); // Clear the list of leaderboard entries

        PlayerPrefs.DeleteKey(LeaderboardKey); // Remove the stored leaderboard from PlayerPrefs
        PlayerPrefs.Save(); // Save the changes

        Debug.Log("1.All offline leaderboard scores have been reset."); // Log confirmation
    }
}


//a wrapper class to store the leaderboard list in JSON
[System.Serializable]
public class LeaderboardWrapper
{
    public List<LeaderboardEntry> leaderboardEntries;
    public LeaderboardWrapper(List<LeaderboardEntry> entries)
    {
        leaderboardEntries = entries;
    }
}