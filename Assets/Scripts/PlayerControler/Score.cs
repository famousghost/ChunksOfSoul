using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

[RequireComponent(typeof(PhotonView))]
public class Score : MonoBehaviour, IPunObservable
{
    public int spiritChunkCounter;

    void Awake()
    {
        spiritChunkCounter = 0;
    }

    void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(spiritChunkCounter);
        }
        else
        {
            spiritChunkCounter = (int)stream.ReceiveNext();
        }
    }
}
