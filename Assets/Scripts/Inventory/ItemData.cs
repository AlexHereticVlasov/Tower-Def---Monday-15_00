using UnityEngine;

namespace Inventory
{
    [CreateAssetMenu(fileName = nameof(ItemData), menuName = nameof(ScriptableObject) + " / " + nameof(ItemData))]
    public class ItemData : ScriptableObject
    {
        [field: SerializeField] public int ID { get; private set; }

        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public Sprite Sprite { get; private set; }
        [field: SerializeField] public string Description { get; private set; }

        public bool LessThen(ItemData other) => Name.CompareTo(other.Name) < 0;
    }
}
