using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnSpiritChunks : MonoBehaviour, IPunObservable
{
    public GameObject[] objectToSpawnList1;
    public GameObject[] objectToSpawnList2;
    public GameObject[] objectToSpawnList3;
    public GameObject[] objectToSpawnList4;
    private PhotonView photonView;
    public int rnd;

    void Start()
    {
        rnd = 0;
        spawnChunks();
        photonView = PhotonView.Get(this);
    }

    void activeSpiritChunks(GameObject[] array)
    {
        foreach(GameObject obj in array)
        {
            obj.SetActive(true);
        }
    }

    public void spawnChunks()
    {
        foreach(GameObject obj in objectToSpawnList1)
        {
            obj.SetActive(false);
        }
        foreach (GameObject obj in objectToSpawnList2)
        {
            obj.SetActive(false);
        }
        foreach (GameObject obj in objectToSpawnList3)
        {
            obj.SetActive(false);
        }
        foreach (GameObject obj in objectToSpawnList4)
        {
            obj.SetActive(false);
        }

        if (rnd <= 50)
        {
            activeSpiritChunks(objectToSpawnList1);
        }
        if (rnd > 50 && rnd <= 100)
        {
            activeSpiritChunks(objectToSpawnList2);
        }
        if (rnd > 100 && rnd <= 150)
        {
            activeSpiritChunks(objectToSpawnList3);
        }
        if (rnd > 150 && rnd <= 200)
        {
            activeSpiritChunks(objectToSpawnList4);
        }
    }

    public void randomChunkPos()
    {
        rnd = Random.Range(0, 201);
    }

    public void updateRndValue()
    {
        photonView.RPC("RPC_updateRndValue", RpcTarget.All, rnd);
    }

    [PunRPC]
    public void RPC_updateRndValue(int r)
    {
        rnd = r;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(rnd);
        }
        else
        {
            rnd = (int)stream.ReceiveNext();
        }
    }
}
