using UnityEngine;

namespace Inventory
{
    [CreateAssetMenu(fileName = "AllItem", menuName = "ScriptableObject/AllItem")]
    public class AllItems : ScriptableObject
    {
        [SerializeField] private ItemData[] _allItems;

        public ItemData this[int index] => _allItems[index];


        public ItemData GetItemFromId(int id)
        {
            foreach (var item in _allItems)
                if (item.ID == id)
                    return item;

            throw new System.Exception("Проставь Айдишники");
        }
    }
}
