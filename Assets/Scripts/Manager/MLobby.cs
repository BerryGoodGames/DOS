using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Photon.Pun;
using TMPro;
using Photon.Realtime;

public class MLobby : MonoBehaviourPunCallbacks
{
    #region VARIABLES
    // singleton
    public static MLobby Instance { get; private set; } 

    // editor
    [SerializeField] private TMP_InputField nicknameInput;
    [SerializeField] private TMP_InputField roomInput;
    [SerializeField] private GameObject lobbyPanel;
    [SerializeField] private GameObject roomPanel;

    // non-editor
    private static string currentRoomName;
    #endregion

    private void Awake()
    {
        // init singleton
        if (Instance == null)
            Instance = this;
        else Destroy(this);
    }

    #region LOBBY
    public void OnClickConnectToLobby()
    {
        // get nickname and connect to lobby
        ConnectToLobby(nicknameInput.text);
    }

    public static void ConnectToLobby(string nickname)
    {
        // set nickname and connect to lobby
        PhotonNetwork.NickName = nickname;
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        // switch to room panel
        lobbyPanel.SetActive(false);
        roomPanel.SetActive(true);
    }
    #endregion

    #region ROOM
    public static void ConnectToRoom(string roomName)
    {
        currentRoomName = roomName;
        PhotonNetwork.JoinRoom(roomName);
    }

    public void OnClickConnectToRoom()
    {
        ConnectToRoom(roomInput.text);
    }

    public override void OnJoinedRoom()
    {
        print("joined room");
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        // create room if room doesn't exist
        if (returnCode == 32758 /*Game does not exist*/) PhotonNetwork.CreateRoom(currentRoomName);
    }
    #endregion
}
