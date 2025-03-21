using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
public class Connect : MonoBehaviourPunCallbacks
{
    void Start()
    {
        //Temporary way of deciding the ID of each player. May be changed in future.
        ConnectToPhoton(Random.Range(1, 10000).ToString());
    }
    //Connecting to Photon Servers
    private void ConnectToPhoton(string name)
    {
        PhotonNetwork.AuthValues = new AuthenticationValues(name);
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.NickName = name;
        PhotonNetwork.ConnectUsingSettings();
    }
    //Creating or joining preexisting room.
    private void CreateOrJoinRoom(string roomname)
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.IsOpen = true;
        roomOptions.MaxPlayers = 8;
        roomOptions.IsVisible = true;
        PhotonNetwork.JoinOrCreateRoom(roomname, roomOptions, TypedLobby.Default);
    }
    public override void OnConnectedToMaster()
    {
        //When connected to Photon servers join lobby.
        if (!PhotonNetwork.InLobby)
        {
            PhotonNetwork.JoinLobby();
        }
    }
    public override void OnJoinedLobby()
    {
        //
        CreateOrJoinRoom("ROOM");
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("JOINED");
        PhotonNetwork.Instantiate("Player", Vector3.zero, Quaternion.identity);
    }



}
