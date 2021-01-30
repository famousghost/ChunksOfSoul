using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpdateGoal : MonoBehaviour
{
    public TMP_Text goalValue;
    public Score score;
    public int currentScore;

    void Start()
    {
        score = GetComponent<Score>();
        currentScore = 0;
    }

    void Update()
    {
        if(currentScore != score.spiritChunkCounter)
        {
            currentScore = score.spiritChunkCounter;
            goalValue.text = currentScore + "/7 Chunks of soul";
        }
    }
}
