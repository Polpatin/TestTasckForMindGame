using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class CreateAndJoinRooms : MonoBehaviourPunCallbacks
{
    [SerializeField] TMP_InputField CreateInput;
    [SerializeField] TMP_InputField JoinInput;

   public void CreateRoom()
    {
        PlayerPrefs.SetString("IsServerPlayer", "true");
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 4;
        PhotonNetwork.CreateRoom(CreateInput.text,roomOptions);
    }
    public void JoinRoom()
    {
        PlayerPrefs.SetString("IsServerPlayer","false");
        PhotonNetwork.JoinRoom(JoinInput.text);

    }
    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("GameScene");
    }
   
}
