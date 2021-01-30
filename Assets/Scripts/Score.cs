using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Score : MonoBehaviour
{
    public int spiritChunkCounter;
    public int scorePlayer1;
    public int scorePlayer2;

    void Awake()
    {
        scorePlayer1 = 0;
        scorePlayer2 = 0;
        spiritChunkCounter = 0;
    }
}
