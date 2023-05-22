using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveSystem 
{
    private static string saveFileName = "colorblind.save";

    public static void SaveProgress(int sceneIndex) 
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/" + saveFileName;
        FileStream stream = new FileStream(path, FileMode.Create);

        SaveData data = new SaveData(sceneIndex);
        formatter.Serialize(stream, data);

        stream.Close();
    }

    public static SaveData LoadProgress()
    {
        string path = Application.persistentDataPath + "/" + saveFileName;

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
}
