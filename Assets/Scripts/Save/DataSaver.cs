using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class DataSaver<TData> where TData : class, IData, new()
{
    private string _fileName;

    public DataSaver(string fileName)
    {
        _fileName = fileName;
    }

    public void SaveData(TData objectData)
    {
        string filePath = Application.persistentDataPath + "/" + _fileName + ".gamesave";
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream fileStream = new FileStream(filePath, FileMode.OpenOrCreate);

        binaryFormatter.Serialize(fileStream, objectData);
        fileStream.Close();
    }

    public TData LoadData()
    {
        string filePath = Application.persistentDataPath + "/" + _fileName + ".gamesave";

        if (!File.Exists(filePath))
            throw new FileNotFoundException();

        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream fileStream = new FileStream(filePath, FileMode.Open);
        TData data = binaryFormatter.Deserialize(fileStream) as TData;

        fileStream.Close();
        return data;
    }
}