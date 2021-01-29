using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Score : MonoBehaviour, IPunObservable
{
    public int spiritChunkCounter;
    public bool playerTaken;

    void Awake()
    {
        spiritChunkCounter = 0;
    }

    void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(spiritChunkCounter);
            stream.SendNext(playerTaken);
        }
        else
        {
            spiritChunkCounter = (int)stream.ReceiveNext();
            playerTaken = (bool)stream.ReceiveNext();
        }
    }
}
