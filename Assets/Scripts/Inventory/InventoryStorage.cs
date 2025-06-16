using System.Collections.Generic;
using UnityEngine.Events;

namespace Inventory
{
    public class InventoryStorage : IInventoryStorage
    {
        public event UnityAction<ItemData> Changed;

        private readonly SortedDictionary<ItemData, int> _items = new(Comparer.Instance);
        public IEnumerable<InventoryStorageItem> Items
        {
            get
            {
                foreach (var item in _items)
                    yield return new InventoryStorageItem(item.Key, item.Value);
            }
        }
        public class Comparer : IComparer<ItemData>
        {
            public static readonly Comparer Instance = new();

            public int Compare(ItemData x, ItemData y)
            {
                if (x.LessThen(y)) return -1;
                if (y.LessThen(x)) return 1;

                int instanceIdX = x.GetInstanceID();
                int instanceIdY = y.GetInstanceID();

                if (instanceIdX < instanceIdY) return -1;
                if (instanceIdX > instanceIdY) return 1;

                return 0;
            }
        }

        public void Add(ItemData item, int amount)
        {
            if (amount <= 0) return;

            _items.TryGetValue(item, out int count);
            _items[item] = amount + count;

            Changed?.Invoke(item);
        }

        public bool TryRemove(ItemData item, int amount)
        {
            if (amount <= 0) return false;

            if (_items.TryGetValue(item, out int count) == false || count < amount) return false;

            count -= amount;
            _items[item] = count;
            Changed?.Invoke(item);

            if (count > 0 == false && _items.Remove(item) == false)
                return false;

            return true;
        }

        public void Clear()
        {
            _items.Clear();
            //Changed?.Invoke(item);
        }

        public int CountOf(ItemData item)
        {
            _items.TryGetValue(item, out int count);
            return count;
        }
    }
}
