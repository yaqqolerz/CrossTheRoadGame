using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreText : MonoBehaviour
{
    [SerializeField] TMP_Text scoreText;

    public void UpdateScore(int score)
    {
        scoreText.text = score.ToString();
    }
}
