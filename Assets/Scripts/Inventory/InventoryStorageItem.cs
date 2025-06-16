using System;

namespace Inventory
{
    [Serializable]
    public struct InventoryStorageItem
    {
        public ItemData ItemData;
        public int Amount;

        public InventoryStorageItem(ItemData itemData, int amount)
        {
            ItemData = itemData;
            Amount = amount;
        }

        public string GetInfo() => $"{ItemData.Name}: {Amount}";
    }
}
