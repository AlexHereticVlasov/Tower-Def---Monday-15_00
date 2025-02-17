using UnityEngine;
using UnityEngine.Events;

public class Stats : MonoBehaviour, IStatsReadOnly
{
    [SerializeField] private float _value;
    [SerializeField] private float _max;
    [SerializeField] private float _armor;

    public event UnityAction<IStatsReadOnly> Died;
    public event UnityAction<IStatsReadOnly> ValueChanged;

    public float GetArmor() => _armor;

    public float GetHealth() => _value;

    public float GetMaxHealth() => _max;

    public float GetNormilizeHealth() => _value / _max;

    public void TakeDamage(float damage)
    {
        _value -= damage;
        ValueChanged?.Invoke(this);
        if (_value <= 0)
        {
            Die();
        }

    }

    private void Die()
    {
        Died?.Invoke(this);
    }
}

public interface IStatsReadOnly
{
    float GetHealth();
    float GetMaxHealth();
    float GetNormilizeHealth();
    float GetArmor();

    event UnityAction<IStatsReadOnly> Died;
    event UnityAction<IStatsReadOnly> ValueChanged;
}