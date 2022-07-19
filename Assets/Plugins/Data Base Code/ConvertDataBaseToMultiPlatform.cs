using System.IO;
using UnityEngine;

public class ConvertDataBaseToMultiPlatform : MonoBehaviour
{
    public string DataBaseName;

    public void Awake()
    {
        GenerateConnectionString(DataBaseName+".db");
        
    }
    public void GenerateConnectionString(string DatabaseName)
    {
        string dbPath = Application.dataPath + "/StreamingAssets/" + DatabaseName;
        Debug.Log("dbPath â convert= " + dbPath);
      


    }



 
}