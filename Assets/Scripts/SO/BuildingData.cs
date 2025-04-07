using UnityEngine;

[CreateAssetMenu(fileName = nameof(BuildingData), menuName = nameof(ScriptableObject) + " / " + nameof(BuildingData))]
public class BuildingData : ScriptableObject
{
    [field: SerializeField] public BuilingBase Template { get; private set; }
    [field: SerializeField] public Sprite Icon { get; private set; }
    [field: SerializeField] public int Cost { get; private set; }
}
