using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class FlashLightState : MonoBehaviour, IPunObservable
{
    public bool flashLightOn;
    private PhotonView photonView;

    void Start()
    {
        flashLightOn = true;
        photonView = PhotonView.Get(this);
    }

    public void updateFlashLightStateOnServer()
    {
        photonView.RPC("RPC_updateFlashLightState", RpcTarget.All, new object[] { flashLightOn });
    }

    [PunRPC]
    public void RPC_updateFlashLightState(bool FLO)
    {
        if(!photonView.IsMine)
        {
            return;
        }
        flashLightOn = FLO;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(flashLightOn);
        }
        else
        {
            flashLightOn = (bool)stream.ReceiveNext();
        }
    }
}
