using System.Collections.Generic;
using UnityEngine;

namespace Inventory
{
    [CreateAssetMenu(fileName = nameof(Recept), menuName = nameof(ScriptableObject) + " / " + nameof(Recept))]
    public class Recept : ScriptableObject
    {
        [SerializeField] private InventoryStorageItem[] _components;

        public IEnumerable<InventoryStorageItem> Components => _components;
        [field: SerializeField] public ItemData Result { get; private set; }
        [field: SerializeField] public float Duration { get; internal set; }

        public bool IsEnough(IInventoryStorage storage)
        {
            foreach (var item in _components)
                if (storage.CountOf(item.ItemData) < item.Amount)
                    return false;

            return true;
        }
    }
}
