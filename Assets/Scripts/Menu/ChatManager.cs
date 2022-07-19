using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using Mono.Data.Sqlite;
using Mono.Data;
using System.Data;
using TMPro;

public class ChatManager : MonoBehaviour
{
    [SerializeField] private Text _chatRealtimeText;   // wholeChatText
    [SerializeField] private string DataBaseName;      //databaase Name
    [SerializeField] private Text _whoIsOnlineText;   //field "who is online"




    private enum _chatTextColor  //colors for users
    {
        red,
        blue,
        green,
        black
    }


  public void WeGotNewOne(string NickName, int color)
  {
      //we got newcomer! Print hello message
     
        string _msgfortable = $" Привет всем! Я {NickName} мой цвет {(_chatTextColor)color}";

        IDbConnection _dbConnect;
        IDbCommand _dbCmd;
        IDataReader _reader;
        string _connectionString = SetDataBaseClass.SetDataBase(DataBaseName + ".db");

        _dbConnect = new SqliteConnection(_connectionString);
        _dbConnect.Open();
        _dbCmd = _dbConnect.CreateCommand();
       
        string _sqlQuerry = "Insert Into ChatTable(DateTime,Msg,UserName,UserColor) " +
                               "Values('"+ DateTime.Now +"', '"+_msgfortable+"', '"+ NickName +"', '"+  (_chatTextColor)color +"')";
        _dbCmd.CommandText = _sqlQuerry;
        _reader = _dbCmd.ExecuteReader();
        while (_reader.Read())
        {


        }

        _reader.Close();
        _reader = null;
        _dbCmd.Dispose();
        _dbCmd = null;
        _dbConnect.Close();
        _dbConnect = null;
            
  }
       
    public void WeGotNewMesage(string Message, string Nick, int MessageColor)
    {
        //User sending new message to server

        Debug.Log($"Пользователь {Nick} цветом  {(_chatTextColor)MessageColor}  прислал сообщение  {Message}"); ;

        IDbConnection _dbConnect;
        IDbCommand _dbCmd;
        IDataReader _reader;
        string _connectionString = SetDataBaseClass.SetDataBase(DataBaseName + ".db");

        _dbConnect = new SqliteConnection(_connectionString);
        _dbConnect.Open();
        _dbCmd = _dbConnect.CreateCommand();
        
        string _sqlQuerry = "Insert Into ChatTable(DateTime,Msg,UserName,UserColor) " +
                            "Values('" + DateTime.Now + "', '" + Message + "', '" + Nick + "', '" + (_chatTextColor)MessageColor + "')";
        _dbCmd.CommandText = _sqlQuerry;
        _reader = _dbCmd.ExecuteReader();
       
        _reader.Close();
        _reader = null;
        _dbCmd.Dispose();
        _dbCmd = null;
        _dbConnect.Close();
        _dbConnect = null;

    }


    private void Start()
    {
        InvokeRepeating("realtimeChatText", 1f, 1f);  //each second we will send  whole chat text to clients (20 messages)
    }

  

    private void realtimeChatText()
    {
  
        //whole text to form

    IDbConnection _dbConnect;
    IDbCommand _dbCmd;
    IDataReader _reader;
   
    string _connectionString = SetDataBaseClass.SetDataBase(DataBaseName + ".db");

        
        _dbConnect = new SqliteConnection(_connectionString);
        _dbConnect.Open();
        _dbCmd = _dbConnect.CreateCommand();

        

        string _sqlQuerry = "SELECT UserColor,DateTime,UserName,Msg FROM `ChatTable` WHERE `IDmsg` > (SELECT MAX(`IDmsg`) FROM `ChatTable`) -20 ORDER BY IDmsg ASC LIMIT 20";
     
        _dbCmd.CommandText = _sqlQuerry;
        _chatRealtimeText.text = "";
        _reader = _dbCmd.ExecuteReader();
        while (_reader.Read())
        {
            _chatRealtimeText.text += $"<color={_reader.GetString(0)}>{_reader.GetString(1)} {_reader.GetString(2)}: {_reader.GetString(3)}</color>\n";
        }

        _reader.Close();
        _reader = null;
        _dbCmd.Dispose();
        _dbCmd = null;
        _dbConnect.Close();
        _dbConnect = null;
 
    }

    public string sendChatToClients()
    {
        
        return _chatRealtimeText.text;
    }

    public string sendOnlineStateToClients()
    {
        
        return _whoIsOnlineText.text;
    }


}
