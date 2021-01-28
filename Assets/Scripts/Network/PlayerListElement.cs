using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayerListElement : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private TMP_Text playerNickName;

    private Player player;

    public void addPlayerNameToList(Player _player)
    {
        player = _player;
        playerNickName.text = _player.NickName;
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        if(player == otherPlayer)
        {
            Destroy(gameObject);
        }
    }

    public override void OnLeftRoom()
    {
        Destroy(gameObject);
    }
}
