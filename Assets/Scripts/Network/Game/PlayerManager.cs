using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class PlayerManager : MonoBehaviour
{
    private PhotonView photonView;
    public Transform playerPos;
    public Transform monsterPos;

    private void Awake()
    {
        photonView = GetComponent<PhotonView>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if(photonView.IsMine)
        {
            createController();
        }
    }

    public void createController()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            Debug.Log("Instaniate player controller");
            PhotonNetwork.Instantiate(Path.Combine("PhotonPrefab", "PlayerPrefab"), playerPos.position, playerPos.rotation);
        }
        else
        {
            Debug.Log("Monster player controller");
            PhotonNetwork.Instantiate(Path.Combine("PhotonPrefab", "MonsterPrefab"), monsterPos.position, monsterPos.rotation);
        }
    }
}
