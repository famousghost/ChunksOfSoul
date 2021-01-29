using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using Photon.Realtime;

public class Luncher : MonoBehaviourPunCallbacks
{
    public static Luncher instance;

    [SerializeField]
    private TMP_InputField roomNameinputField;

    [SerializeField]
    private TMP_InputField playerNickNameinputField;

    [SerializeField]
    private TMP_Text joinRoomErrorMessage;

    [SerializeField]
    private TMP_Text roomNameTmpText;

    [SerializeField]
    private Transform roomListContent;

    [SerializeField]
    private GameObject roomListObj;

    [SerializeField]
    private Transform playerListContent;

    [SerializeField]
    private GameObject playerListObj;

    [SerializeField]
    private GameObject startButton;

    [SerializeField]
    private bool nickNameCreated;

    private void Awake()
    {
        nickNameCreated = false;
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        startButton.SetActive(false);
        Debug.Log("Yeah connecting to the master");
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Yeah connected to master");
        PhotonNetwork.JoinLobby();
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        bool monster = false;
        bool human = false;
        if (PhotonNetwork.PlayerList.Length == 2)
        {
            foreach(Player player in PhotonNetwork.PlayerList)
            {
                if(player.NickName == "Human")
                {
                    human = true;
                }
                if(player.NickName == "Monster")
                {
                    monster = true;
                }
            }
            if (monster && human)
            {
                startButton.SetActive(PhotonNetwork.IsMasterClient);
            }
        }
    }

    public override void OnJoinedLobby()
    {
        if (!nickNameCreated)
        {
            ServerMenuManager.instance.openMenu("NickNameCreation");
            nickNameCreated = true;
        }
        else
        {
            ServerMenuManager.instance.openMenu("MainMenu");
        }
        Debug.Log("Yeah hurray joined the lobby");
    }

    public void addHuman()
    {
        addNickName("Human");
    }

    public void addMonster()
    {
        addNickName("Monster");
    }

    public void addNickName(string nickname)
    {
        if (string.IsNullOrEmpty(nickname))
        {
            return;
        }
        PhotonNetwork.NickName = nickname;
        ServerMenuManager.instance.openMenu("MainMenu");
    }

    public void createRoom()
    {
        if (string.IsNullOrEmpty(roomNameinputField.text))
        {
            return;
        }
        PhotonNetwork.CreateRoom(roomNameinputField.text);
        roomNameTmpText.text = roomNameinputField.text;
        ServerMenuManager.instance.openMenu("Connecting");
    }

    public void StartGame()
    {
        bool monster = false;
        bool human = false;
        if (PhotonNetwork.PlayerList.Length == 2)
        {
            foreach (Player player in PhotonNetwork.PlayerList)
            {
                if (player.NickName == "Human")
                {
                    human = true;
                }
                if (player.NickName == "Monster")
                {
                    monster = true;
                }
            }
            if (monster && human)
            {
                PhotonNetwork.LoadLevel(1);
            }
        }
    }

    public void leaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        ServerMenuManager.instance.openMenu("Connecting");
    }

    public void joinRoom(RoomInfo roomInfo)
    {
        PhotonNetwork.JoinRoom(roomInfo.Name);
        ServerMenuManager.instance.openMenu("Connecting");
    }

    public override void OnLeftRoom()
    {
        ServerMenuManager.instance.openMenu("MainMenu");
    }

    public override void OnJoinedRoom()
    {
        Player[] playerList = PhotonNetwork.PlayerList;

        foreach(Transform player in playerListContent)
        {
            Destroy(player.gameObject);
        }

        for (int i = 0; i < playerList.Length; i++)
        {
            Instantiate(playerListObj, playerListContent).GetComponent<PlayerListElement>().addPlayerNameToList(playerList[i]);
        }
        roomNameTmpText.text = roomNameinputField.text;
        ServerMenuManager.instance.openMenu("RoomMenu");

        startButton.SetActive(PhotonNetwork.IsMasterClient);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        joinRoomErrorMessage.text = "Room creation failed: " + message;
        ServerMenuManager.instance.openMenu("ErrorMenu");
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach(Transform element in roomListContent)
        {
            Destroy(element.gameObject);
        }
        for (int i = 0; i < roomList.Count; i++)
        {
            if (roomList[i].RemovedFromList)
                continue;
            Instantiate(roomListObj, roomListContent).GetComponent<RoomListElement>().addRoomButton(roomList[i]);
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Instantiate(playerListObj, playerListContent).GetComponent<PlayerListElement>().addPlayerNameToList(newPlayer);
    }
}
