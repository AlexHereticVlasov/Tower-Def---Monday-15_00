using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using PathIO = System.IO.Path;


public static class Saver
{
    public static void SaveData<T>(T data, string fileName) where T : class
    {
        string path = GetPath(fileName);

        try
        {
            string json = JsonUtility.ToJson(data, prettyPrint: true);
            File.WriteAllText(path, json);
            Debug.Log($"Data saved to: {path}");
        }
        catch (Exception e)
        {
            Debug.LogError($"Save failed: {e.Message}");
        }
    }

    public static bool TryLoadData<T>(out T data, string fileName) where T : class
    {
        string path = GetPath(fileName);
        data = null;

        if (File.Exists(path) == false)
        {
            Debug.LogWarning($"File not found: {path}");
            return false;
        }

        try
        {
            string json = File.ReadAllText(path);
            data = JsonUtility.FromJson<T>(json);
            return true;
        }
        catch (Exception e)
        {
            Debug.LogError($"Load failed: {e.Message}");
            return false;
        }
    }

    public static void DeleteFile(string fileName)
    {
        string path = GetPath(fileName);
        if (File.Exists(path))
        {
            File.Delete(path);
            Debug.Log($"File deleted: {path}");
        }
    }

    private static IList<string> GetSaveFileNames()
    {
        List<string> saveNames = new();
        string saveDirectory = Application.persistentDataPath;

        try
        {
            if (Directory.Exists(saveDirectory) == false)
            {
                Debug.LogWarning("Save directory does not exist: " + saveDirectory);
                return saveNames;
            }

            string[] files = Directory.GetFiles(saveDirectory, "*.json");

            foreach (string filePath in files)
            {
                try
                {
                    string fileName = PathIO.GetFileNameWithoutExtension(filePath);
                    saveNames.Add(fileName);
                }
                catch (Exception ex)
                {
                    Debug.LogError($"Error processing file {filePath}: {ex.Message}");
                }
            }

            saveNames.Sort((a, b) =>
                File.GetLastWriteTime(GetPath(b)).CompareTo(
                File.GetLastWriteTime(GetPath(a))));
        }
        catch (Exception e)
        {
            Debug.LogError("Error accessing save directory: " + e.Message);
        }

        return saveNames;
    }

    public static List<SaveFileInfo> GetSaveFileInfo()
    {
        return GetSaveFileNames().Select(name =>
        {
            string path = GetPath(name);
            return new SaveFileInfo(name, File.GetLastWriteTime(path), new FileInfo(path).Length);
        }).ToList();
    }

    private static string GetPath(string fileName)
    {
        string path = PathIO.Combine(Application.persistentDataPath, $"{fileName}.json");
        Debug.Log($"Using path: {path}");
        return path;
    }
}
