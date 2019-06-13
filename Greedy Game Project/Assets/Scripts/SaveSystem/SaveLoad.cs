using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveLoad
{
    public static void Save(GlobalControl globalControl)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/game.data";
        FileStream stream = new FileStream(path, FileMode.Create);

        Game gameData = new Game(globalControl);

        formatter.Serialize(stream, gameData);
        stream.Close();
    }

    public static Game Load()
    {
        string path = Application.persistentDataPath + "/game.data";

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            Game data = formatter.Deserialize(stream) as Game;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Archivo de guardado no encontrado en " + path);
            return null;
        }
    }
}