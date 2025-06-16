using UnityEngine;

namespace SpellSystem
{
    [CreateAssetMenu(fileName = nameof(Spell), menuName = nameof(ScriptableObject) + " / " + nameof(Spell))]
    public class Spell : ScriptableObject
    {
        [field: SerializeField] public SpellEffectBase SpellEffect { get; private set; }
        [field: SerializeField] public int ManaCost { get; private set; }
    }
}