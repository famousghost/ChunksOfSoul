using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;
using UnityEngine.SceneManagement;
using Photon.Realtime;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    private PhotonView photonView;
    public Transform playerPos;
    public Transform monsterPos;
    public GameObject playerPrefab;
    public GameObject monsterPrefab;
    public int spiritChunkCounter;

    private void Awake()
    {
        if(instance)
        {
            Destroy(instance.gameObject);
        }
        spiritChunkCounter = 0;
        instance = this;
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

    public void restartGame()
    {
        StartCoroutine(reloadSceneCor());
    }

    IEnumerator reloadSceneCor()
    {
        foreach (Player player in PhotonNetwork.PlayerList)
        {
            photonView.RPC("LoadMyScene", player, SceneManager.GetActiveScene().name);
        }
        yield return null;
        PhotonNetwork.IsMessageQueueRunning = false;
        PhotonNetwork.LoadLevel(SceneManager.GetActiveScene().name);
    }

    [PunRPC]
    public void LoadMyScene(string sceneName)
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        Debug.Log("REcieved RPC " + sceneName);
        SceneManager.LoadScene(1);
    }

    void Update()
    {
        if(spiritChunkCounter >= 7)
        {
            restartGame();
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
