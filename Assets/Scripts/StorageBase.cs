using UnityEngine;
using UnityEngine.Events;

public abstract class StorageBase : MonoBehaviour
{
    public int Amount { get; private set; } = 200;

    public event UnityAction<int> AmountChanged;

    public void Init(int amount)
    {
        Amount = amount;
        AmountChanged?.Invoke(Amount);
    }

    public void Add(int amount)
    {
        Amount += amount;
        AmountChanged?.Invoke(Amount);
    }

    public bool TryGet(int amount)
    {
        if (Amount >= amount)
        {
            Amount -= amount;
            AmountChanged?.Invoke(Amount);
            return true;
        }

        return false;
    }

}
