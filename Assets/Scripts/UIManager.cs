using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text textScore;
    [SerializeField] private TMP_Text textBest;

    private void Update()
    {
        textBest.text = "Best: " + GameManager.singleton.bestScore;
        textScore.text = "Score: " + GameManager.singleton.score;
    }


}
