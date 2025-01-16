using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class JSONTool
{
    public static void WriteData<JSONData>(JSONData data, string fileName) {
        string savePath = Application.persistentDataPath + Path.AltDirectorySeparatorChar + fileName + ".json";
        string json = JsonUtility.ToJson(data);
        using StreamWriter writer = new StreamWriter(savePath);
        writer.Write(json);
    }

    public static T ReadData<T>(string fileName) where T : IJSONData<T>, new() {
        TextAsset textAsset = Resources.Load<TextAsset>(fileName);
        T data;
        if (textAsset != null)
        {
            data = JsonUtility.FromJson<T>(textAsset.text);
            return data;
        }
        else
        {
            fileName = fileName + ".json";
            if (!FileExists(fileName)) {
                T tData = new T();
                tData = tData.CreateNewFile();
                WriteData(tData, fileName);
            }
            using StreamReader reader = new StreamReader(Application.persistentDataPath + Path.AltDirectorySeparatorChar + fileName);
            string json = reader.ReadToEnd();
            data = JsonUtility.FromJson<T>(json);
            return data;
        }

    }

    public static T TranslateRawToData<T>(string rawText) where T : IJSONData<T>, new() {
        T data = JsonUtility.FromJson<T>(rawText);
        return data;
    }

    public static bool FileExists(string fileName) {
        return File.Exists(Application.persistentDataPath + Path.AltDirectorySeparatorChar + fileName);
    }
}