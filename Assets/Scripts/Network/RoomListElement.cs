using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Realtime;

public class RoomListElement : MonoBehaviour
{
    [SerializeField]
    private TMP_Text roomName;

    public RoomInfo roomInfo;

    public void addRoomButton(RoomInfo _roomInfo)
    {
        roomInfo = _roomInfo;
        roomName.text = _roomInfo.Name;
    }

    public void OnClick()
    {
        Luncher.instance.joinRoom(roomInfo);
    }
}
