using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Stats : MonoBehaviour, IStatsReadOnly
{
    [SerializeField] private float _value;
    [SerializeField] private float _max;
    [SerializeField] private float _regeneration;
    [SerializeField] private float _armor;

    private float _actualArmor;

    private readonly List<float> _armorModifier = new();

    public event UnityAction<IStatsReadOnly> Died;
    public event UnityAction<IStatsReadOnly> ValueChanged;

    private void Start()
    {
        _actualArmor = _armor;
    }

    public float GetArmor() => _actualArmor;

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

    public void AddArmorModifier(float value)
    {
        _armorModifier.Add(value);
        UpdateArmorValue();
    }

    private void UpdateArmorValue() => _actualArmor = _armorModifier.Sum() + _armor;

    public void RemoveArmorModifier(float value)
    {
        _armorModifier.Remove(value);

        //ToDO: Use Linq
        float sum = 0;
        foreach (var item in _armorModifier)
        {
            sum += item;
        }

        _actualArmor = sum + _armor;

    }

    private void Die()
    {
        Died?.Invoke(this);
    }

    internal void Heal(float healAmount)
    {
        if (healAmount < 0)
        {
            throw new System.Exception($"Heal amount = {healAmount}, must be positive");
        }

        _value += healAmount;
        ValueChanged?.Invoke(this);
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