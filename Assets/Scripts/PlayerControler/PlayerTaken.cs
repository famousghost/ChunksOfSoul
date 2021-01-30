using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

[RequireComponent(typeof(PhotonView))]
public class PlayerTaken : MonoBehaviour, IPunObservable
{
    public bool take;

    void Awake()
    {
        take = false;
    }

    void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(take);
        }
        else
        {
            take = (bool)stream.ReceiveNext();
        }
    }
}
