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
    public GameObject[] playerPos;
    public GameObject[] ghostPos;
    public GameObject playerController;
    public Button menuLeaveBtn;
    public Button gameoverLeaveBtn;

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
            menuLeaveBtn = GameObject.FindGameObjectWithTag("MenuExitButton").GetComponent<Button>();
            gameoverLeaveBtn = GameObject.FindGameObjectWithTag("GameOverExitButton").GetComponent<Button>();
            menuLeaveBtn.onClick.AddListener(taskOnClick);
            gameoverLeaveBtn.onClick.AddListener(taskOnClick);
        }
    }

    public void taskOnClick()
    {
        leaveRoom();
    }

    public Transform generateRndPos(GameObject[] posArray)
    {
        Transform pos = posArray[0].transform;
        int rnd = Random.Range(0, 201);
        if (rnd <= 50)
        {
            pos.position = posArray[0].transform.position;
            pos.rotation = posArray[0].transform.rotation;
        }
        if (rnd > 50 && rnd <= 100)
        {
            pos.position = posArray[1].transform.position;
            pos.rotation = posArray[1].transform.rotation;
        }
        if (rnd > 100 && rnd <= 150)
        {
            pos.position = posArray[2].transform.position;
            pos.rotation = posArray[2].transform.rotation;
        }
        if (rnd > 150 && rnd <= 200)
        {
            pos.position = posArray[3].transform.position;
            pos.rotation = posArray[3].transform.rotation;
        }
        return pos;
    }

    public void createController()
    {
        Transform playerPosition = generateRndPos(playerPos);
        Transform ghostPosition = generateRndPos(ghostPos);

        if (PhotonNetwork.NickName == "Monster")
        {
            Debug.Log("robie potwora");
            playerController = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefab", "MonsterPrefab"), ghostPosition.position, ghostPosition.rotation, 0, new object[] { photonView.ViewID });
        }
        else
        {
            Debug.Log("robie czleka");
            playerController = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefab", "PlayerPrefab"), playerPosition.position, playerPosition.rotation, 0, new object[] { photonView.ViewID });
        }
    }

    public void leaveRoom()
    {
        PhotonNetwork.Destroy(playerController);
        PhotonNetwork.Destroy(this.gameObject);
        PhotonNetwork.LeaveRoom();
        Application.Quit();
    }

    public void gameover()
    {
        PhotonNetwork.Destroy(playerController);
        createController();
    }
}
