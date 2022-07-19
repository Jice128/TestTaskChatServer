using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
    public static UIScript Instance { set; get; }

    [SerializeField] private Animator _menuAnimator;
    [SerializeField] private TMP_InputField _nickName;
    [SerializeField] private ChatConnection _connection;
    [SerializeField] private Text _whoIsOnlineText;

    public int _chatMemberColorNumber;
    public string _chatMemberNickName = "No name";
    public bool _weMayConnect = false;


    private enum _chatMemberColor
    {
        red,
        blue,
        green,
        black
    }

    private void Awake()
    {
        Instance = this;
        _chatMemberColorNumber = 3;
    }


    public void OnStartChatButton()
    {
        _chatMemberNickName = _nickName.text;
        if (string.IsNullOrWhiteSpace(_chatMemberNickName)) { return; } //check that users can no have Null name

        else
        {
            _weMayConnect = true;
            _menuAnimator.SetTrigger("ChatMenu");
        }
    }

    public void OnExitChatButton()
    {
        _menuAnimator.SetTrigger("MainMenu");
    }


    
    public void _redToglleChanged(bool newValue)
    {
        if (newValue)
            _chatMemberColorNumber = (int)_chatMemberColor.red;
    }
    public void _blueToglleChanged(bool newValue)
    {
        if (newValue)
            _chatMemberColorNumber = (int)_chatMemberColor.blue;
    }
    public void _greenToglleChanged(bool newValue)
    {
        if (newValue)
            _chatMemberColorNumber = (int)_chatMemberColor.green;
    }
    public void _blackToglleChanged(bool newValue)
    {
        if (newValue)
            _chatMemberColorNumber = (int)_chatMemberColor.black;
    }



    public void ExitBtn()
    {
        Application.Quit();
    }

    private void Start()
    {
        InvokeRepeating("NowOnline", 1f, 1f);
    }



    void NowOnline()
    {

        //search for online users
        _whoIsOnlineText.text = "";
        GameObject[] chatMember = GameObject.FindGameObjectsWithTag("ChatMember");  //not really good using search :( but for simple  I think this will do
        foreach (GameObject _chatMember in chatMember)
        {
           
            int _colorNumber = int.Parse($"{_chatMember.name.Substring(_chatMember.name.Length - 1)}");
            string _color = $"{(_chatMemberColor)_colorNumber}";
            _whoIsOnlineText.text += $"<color={_color}>{_chatMember.name.Substring(0,_chatMember.name.IndexOf('^'))}</color>\n";

        }

    }
       

}
