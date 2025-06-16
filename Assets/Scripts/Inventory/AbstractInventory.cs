using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory
{
    public abstract class AbstractInventory : MonoBehaviour, IInventory
    {
        [SerializeField] private AllItems _allItems;

        public ItemSaveData[] Items
        {
            get
            {
                var result = new List<ItemSaveData>();

                foreach (var item in Storage.Items)
                    result.Add(new ItemSaveData(item.ItemData.ID, item.Amount));


                return result.ToArray();
            }
        }

        public IInventoryStorage Storage { get; private set; } = new InventoryStorage();


        public void Load(PlayerSaveData data)
        {
            foreach (var item in data.Items)
            {
                if (item.Count < 0)
                    throw new System.Exception($"{_allItems[item.ID].Name} amount must be positive");

                Storage.Add(_allItems[item.ID], item.Count);
            }
        }

        public void Add(Cost cost)
        {
            foreach (var item in cost.Resources)
                AddItem(item);
        }

        public void AddItem(Item item) => Storage.Add(item.ItemData, item.Amount);

        public bool TrySpend(Cost cost)
        {
            if (IsEnough(cost))
            {
                foreach (var item in cost.Resources)
                    Storage.TryRemove(item.ItemData, item.Amount);

                return true;
            }

            return false;
        }

        private bool IsEnough(Cost cost)
        {
            foreach (var item in cost.Resources)
                if (Storage.CountOf(item.ItemData) < item.Amount)
                    throw new System.Exception($"Not Enough {item.Amount - Storage.CountOf(item.ItemData)} {item.ItemData.Name}");

            return true;
        }
    }

    [CreateAssetMenu(fileName = nameof(Cost), menuName = nameof(ScriptableObject) + " / " + nameof(Cost))]
    public class Cost : ScriptableObject
    {
        [field: SerializeField] public Item[] Resources { get; private set; }

        public void GetValue(float multiplier)
        {
            //return value
        }
    }
}
