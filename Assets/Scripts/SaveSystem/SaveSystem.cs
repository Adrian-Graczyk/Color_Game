using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveSystem 
{
    private static string saveFileName = "colorblind.save";

    public static void SaveProgress(int sceneIndex) 
    {
        string path = SaveFilePath();

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Create);
        SaveData data = new SaveData(sceneIndex);

        formatter.Serialize(stream, data);

        stream.Close();
    }

    public static SaveData LoadProgress()
    {
        string path = SaveFilePath();

        if (!File.Exists(path))
        {
            Debug.LogWarning("Save file " + path + " was not found");
            return new SaveData();
        }

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Open);

        SaveData saveData = formatter.Deserialize(stream) as SaveData;

        stream.Close();

        return saveData;
    }

    public static void DeleteSaveFile()
    {
        string path = SaveFilePath();

        if (!File.Exists(path))
        {
            Debug.LogWarning("Save file " + path + " was not found");
            return;
        }

        File.Delete(path);
        Debug.Log("Save file " + path + " deleted");
    }

    private static string SaveFilePath()
    {
        return Application.persistentDataPath + "/" + saveFileName;
    }
}
