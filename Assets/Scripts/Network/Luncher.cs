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
    private TMP_Text joinRoomErrorMessage;

    [SerializeField]
    private TMP_Text roomNameTmpText;

    [SerializeField]
    private Transform roomListContent;

    [SerializeField]
    private GameObject roomListObj;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Yeah connecting to the master");
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Yeah connected to master");
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        ServerMenuManager.instance.openMenu("MainMenu");
        Debug.Log("Yeah hurray joined the lobby");
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
        roomNameTmpText.text = roomNameinputField.text;
        ServerMenuManager.instance.openMenu("RoomMenu");
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
            Instantiate(roomListObj, roomListContent).GetComponent<RoomListElement>().addRoomButton(roomList[i]);
        }
    }
}
