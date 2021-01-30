using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpdateGoal : MonoBehaviour
{
    public TMP_Text goalValue;
    public TMP_Text scorePlayer1;
    public TMP_Text scorePlayer2;
    public Score score;
    public int currentScore;
    public int currentScorePlayer1;
    public int currentScorePlayer2;


    void Start()
    {
        score = GetComponent<Score>();
        currentScorePlayer1 = 0;
        currentScorePlayer2 = 0;
        currentScore = 0;
        scorePlayer1.text = "Points: " + score.scorePlayer1;
        scorePlayer2.text = "Points: " + score.scorePlayer2;
    }

    void Update()
    {
        if(currentScore != score.spiritChunkCounter)
        {
            currentScore = score.spiritChunkCounter;
            goalValue.text = currentScore + "/7 Chunks of soul";
        }
        if (currentScorePlayer1 != score.scorePlayer1 || currentScorePlayer2 != score.scorePlayer2)
        {
            currentScorePlayer1 = score.scorePlayer1;
            currentScorePlayer2 = score.scorePlayer2;
            scorePlayer1.text = "Points: " + score.scorePlayer1;
            scorePlayer2.text = "Points: " + score.scorePlayer2;
        }
    }
}
