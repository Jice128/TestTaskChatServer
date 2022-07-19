using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ChatMemberData : NetworkBehaviour
{
    //this consist data of Users name/color/message
    [SerializeField] ChatConnection _chatConnection;
 
    public int _chatMemberColor;
    public string _chatMemberName;
    [SerializeField] private ChatManager  _chatManager;
    public int _chatMemberId;


    private void Start()
    {
        
        _chatConnection = FindObjectOfType<ChatConnection>();
        _chatMemberColor = UIScript.Instance._chatMemberColorNumber;
        _chatMemberName = UIScript.Instance._chatMemberNickName;
        _chatMemberId = Random.Range(1, 100);
    
        NewChatMemberConnecterServerRpc(_chatMemberName, _chatMemberColor);
    }

  

    [ServerRpc]
    void NewChatMemberConnecterServerRpc(string NickName, int _playerColor)
    {
        Debug.Log($"New member is coming! His name is {NickName} His color is {_playerColor}");
        _chatConnection.NewChatMemberConnected(NickName, _playerColor);
        transform.name = $"{NickName}^color={_playerColor}";
    }


}
