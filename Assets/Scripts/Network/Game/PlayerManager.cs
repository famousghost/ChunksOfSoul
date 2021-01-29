using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;
using UnityEngine.SceneManagement;
using Photon.Realtime;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    private PhotonView photonView;
    public Transform playerPos;
    public Transform monsterPos;
    public GameObject playerController;

    private void Awake()
    {
        photonView = GetComponent<PhotonView>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (photonView.IsMine)
        {
            createController();
        }
    }

    public void createController()
    {
        int randIfplayerIsmonsterOrNot = Random.Range(0, 101);
        if (PhotonNetwork.NickName == "Monster")
        {
            Debug.Log("robie potwora");
            playerController = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefab", "MonsterPrefab"), monsterPos.position, monsterPos.rotation, 0, new object[] { photonView.ViewID });
        }
        else
        {
            Debug.Log("robie czleka");
            playerController = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefab", "PlayerPrefab"), playerPos.position, playerPos.rotation, 0, new object[] { photonView.ViewID });
        }
    }

    public void gameover()
    {
        PhotonNetwork.Destroy(playerController);
        createController();
    }
}
