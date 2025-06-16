using Inventory;
using System;
using System.Collections;
using UnityEngine;

public class Bootstraper : MonoBehaviour
{
    [SerializeField] private Spawner[] _spawners;
   

    private void Start()
    {
        
    }

    public void StartGame()
    {
        foreach (var spawner in _spawners)
        {
            spawner.Launch();
        }
    }
}

public class MapBootstapper : MonoBehaviour
{
    [SerializeField] private PlayerInventory _inventory;

    private PlayerSaveData _data;

    private void Awake()
    {
        LoadSaveFile();

        _inventory.Load(_data);
    }

    private void LoadSaveFile()
    {
        if (Saver.TryLoadData(out PlayerSaveData data, "Test"))
        {
            _data = data;
            return;
        }

        _data = PlayerSaveData.Defoult;
    }
}

public class SaveFileInfo
{
    public SaveFileInfo(string name, DateTime lastMoified, long size)
    {
        Name = name;
        LastModified = lastMoified;
        Size = size;
    }

    public string Name { get; }
    public DateTime LastModified { get; }
    public long Size { get; }

    public string GetData()
    {
        return $"File: {Name}, LastMoified: {LastModified}, Size: {Size}";
    }
}