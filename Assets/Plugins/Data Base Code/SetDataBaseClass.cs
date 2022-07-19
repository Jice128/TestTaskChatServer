using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


    public class SetDataBaseClass
    {
        public static string SetDataBase(string DataBaseName)
        {
            string conn = "";
  
         conn = "URI=file:" + Application.dataPath + "/StreamingAssets" + "/"+ DataBaseName; //Path to database
         Debug.Log("Linux Mode" + conn);

           return conn;
        }

    }
