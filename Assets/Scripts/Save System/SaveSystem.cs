using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
public static class SaveSystem
{

    //public static void SaveGameData(PlayerController player, EnemyController enemy)
    //{
    //    BinaryFormatter formatter = new BinaryFormatter();
    //    string path = Application.persistentDataPath + "/gameSave.Saves";
    //    FileStream stream = new FileStream(path, FileMode.Create);

    //    GameData data = new GameData(player, enemy);

    //    formatter.Serialize(stream, data);
    //    stream.Close();
    //}
    public static void SaveGameData(PlayerController player, EnemyController enemy)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/gameSave.Saves";
        FileStream stream = new FileStream(path, FileMode.Create);

        GameData data = new GameData(player, enemy);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static GameData LoadGameData()
    {
        string path = Application.persistentDataPath + "/gameSave.Saves";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            GameData data = formatter.Deserialize(stream) as GameData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }
}
