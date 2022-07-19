using UnityEngine;
using Unity.Netcode;
using Unity.Netcode.Transports.UNET;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

public class ChatConnection : NetworkBehaviour
{
    [SerializeField] private string ipAdress;
    [SerializeField] private int port;
    [SerializeField] private UNetTransport transport;
    [SerializeField] private ChatManager _chatManager;



    void Start()
    {
        startServer();
        InvokeRepeating("realtimeChatTextToClient", 1f, 1f);  //each second we will send whole chat to clients (last 20 messages)
    }

    private void startServer()
    {
        transport = NetworkManager.Singleton.GetComponent<UNetTransport>();
        transport.ConnectAddress = ipAdress;
        NetworkManager.Singleton.StartServer();
        Debug.Log($"Server started at {ipAdress} and port {port}");
    }

  

  

    public void NewChatMemberConnected(string playerName, int playercolor)
    {
        //new user in chat
        _chatManager.WeGotNewOne(playerName, playercolor);
    }


    public void SendChatMessage(string Message, string NickName, int Color)
    {
        NewChatMessageServerRpc(Message, NickName, Color);

    }




    [ServerRpc(RequireOwnership = false)]
    void NewChatMessageServerRpc(string Message, string NickName, int PlayerColor)
    {
        //some client send new message to server
       
        _chatManager.WeGotNewMesage(Message, NickName, PlayerColor);

    }


    void realtimeChatTextToClient()
    {   
      
        realtimeChatToClient_ClientRpc(_chatManager.sendChatToClients());
        realtimeOnlineState_ClientRpc(_chatManager.sendOnlineStateToClients());
    }


    [ClientRpc]
    public void realtimeChatToClient_ClientRpc(string chatText)
    {
        //this will be onclient side. Server sends toclients whole chat text (20 last messages)
           
    }

    [ClientRpc]
    public void realtimeOnlineState_ClientRpc(string onlineState)
    {
        //выполняется на стороне клиента, отсылаем ему 
    }

} 

