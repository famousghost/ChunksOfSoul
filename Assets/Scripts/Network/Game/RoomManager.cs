using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using System.IO;
using Photon.Realtime;

public class RoomManager : MonoBehaviourPunCallbacks
{

    public static RoomManager instance;

    // Start is called before the first frame update
    void Awake()
    {
        if(instance)
        {
            Destroy(instance);
        }
        instance = this;
    }

    public override void OnEnable()
    {
        base.OnEnable();
        SceneManager.sceneLoaded += onSceneLoaded;
    }

    public override void OnDisable()
    {
        base.OnDisable();
    }

    public void onSceneLoaded(Scene scene, LoadSceneMode loadMode)
    {
        if(scene.buildIndex == 1)
        {
            PhotonNetwork.Instantiate(Path.Combine("PhotonPrefab", "PlayerManager"), Vector3.zero, Quaternion.identity);
        }
    }
}
