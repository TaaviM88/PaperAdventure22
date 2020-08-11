using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveSystem
{
    private static readonly string SAVE_FOLDER = Application.dataPath + "/Saves/";
    private const string SAVE_EXTENSION = "txt";
    public static void Init()
    {
        if (!Directory.Exists(SAVE_FOLDER))
        {
            //Create Save Folder
            Directory.CreateDirectory(SAVE_FOLDER);
        }
    }

    public static void Save(string saveString)
    {
        int saveNumber = 1;
        while(File.Exists("save_" + saveNumber + "."+ SAVE_EXTENSION))
        {
            saveNumber++;
        }
        File.WriteAllText(SAVE_FOLDER + "saveFile_" + saveNumber+"." + SAVE_EXTENSION, saveString);
        Debug.Log("Tallennetaan");
    }

    public  static string Load()
    {
        DirectoryInfo directoryInfo = new DirectoryInfo(SAVE_FOLDER);
        Debug.Log(directoryInfo);
        FileInfo[] saveFiles = directoryInfo.GetFiles("*." + SAVE_EXTENSION);
        FileInfo mostRecentFile = null;
        foreach(FileInfo fileInfo in saveFiles)
        {
            if(mostRecentFile == null)
            {
                mostRecentFile = fileInfo;
            }
            else
            {
                if (fileInfo.LastWriteTime > mostRecentFile.LastWriteTime)
                {
                    mostRecentFile = fileInfo;
                }
            }
        }
          
        
        if(mostRecentFile != null)
        {
            Debug.Log(mostRecentFile.FullName);
            string saveString = File.ReadAllText(mostRecentFile.FullName);
            return saveString;
        }
        else
        {
            return null;
        }

        //if(File.Exists(SAVE_FOLDER + "saveFile.json"))
        //{
        //    string saveString = File.ReadAllText(SAVE_FOLDER + "saveFile_.json");
        //    return saveString;
        //}
        //else
        //{
        //    Debug.Log("ei löydy savea");
        //    return null;
        //}
    }
}
