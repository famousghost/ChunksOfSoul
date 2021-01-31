using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Score : MonoBehaviour, IPunObservable
{
    public int spiritChunkCounter;
    public int scorePlayer1;
    public int scorePlayer2;
    PhotonView photonView;

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(spiritChunkCounter);
            stream.SendNext(scorePlayer1);
            stream.SendNext(scorePlayer2);
        }
        else
        {
            spiritChunkCounter = (int)stream.ReceiveNext();
            scorePlayer1 = (int)stream.ReceiveNext();
            scorePlayer2 = (int)stream.ReceiveNext();
        }
    }

    public void updateSpiritsChunks()
    {
        photonView.RPC("RPC_updateSpiritChunk", RpcTarget.All, new object[] { spiritChunkCounter });
    }

    public void updateLadderBoard()
    {
        photonView.RPC("RPC_updateLadderBoard", RpcTarget.All, new object[] { scorePlayer1, scorePlayer2 });
    }

    [PunRPC]
    public void RPC_updateSpiritChunk(int spiritChunk)
    {
        spiritChunkCounter = spiritChunk;
    }

    [PunRPC]
    void RPC_updateLadderBoard(int scP1, int scP2)
    {
        scorePlayer1 = scP1;
        scorePlayer2 = scP2;
    }

    void Awake()
    {
        scorePlayer1 = 0;
        scorePlayer2 = 0;
        spiritChunkCounter = 0;
        photonView = PhotonView.Get(this);
    }
}
