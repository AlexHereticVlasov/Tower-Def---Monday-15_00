using UnityEngine;

namespace Inventory
{
    [System.Serializable]
    public class Item
    {
        [field: SerializeField] public ItemData ItemData { get; private set; }
        [field: SerializeField] public int Amount { get; private set; }

        public Item(ItemData itemData, int amount)
        {
            ItemData = itemData;
            Amount = amount;
        }
    }
}
