using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerSaveData
{
    public List<int> LevelsComplited;
    public List<ItemSaveData> Items;

    public static PlayerSaveData Defoult
    {
        get
        {
            return new PlayerSaveData
            {
                LevelsComplited = new List<int>(),
                Items = new List<ItemSaveData>
                {
                    new ItemSaveData(0, 5),
                    new ItemSaveData(1, 5)
                }
            };
        }
    }

}

[System.Serializable]
public class ItemSaveData
{
    public ItemSaveData(int id, int count)
    {
        ID = id;
        Count = count;
    }

    public int ID;
    public int Count;
}
