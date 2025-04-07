using UnityEngine;
using UnityEngine.Events;

public sealed class PlayerLifes : MonoBehaviour, IPlayerLives
{
    [SerializeField] private int _amount;

    public event UnityAction<int> ValueChanged;
    public event UnityAction Lose;

    private void Start()
    {
        ValueChanged?.Invoke(_amount);
    }

    public void Decrease(int value)
    {
        _amount -= value;
        if (_amount <= 0)
        {
            Lose?.Invoke();
        }

        ValueChanged?.Invoke(_amount);
    }
}

public interface IPlayerLives
{
    event UnityAction Lose;
    event UnityAction<int> ValueChanged;

    void Decrease(int value);
}
