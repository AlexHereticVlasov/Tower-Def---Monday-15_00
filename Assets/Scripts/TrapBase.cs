using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TrapBase : MonoBehaviour, ISelectable, ITrapReadOnly, IDamageDeller
{
    private readonly List<IEffectRecepient> _enemies = new();
    private int _chargeAmount;
    private int _damageAmount = 10;

    private IExpirience _experience;

    private Coroutine _damageRoutine;

    public int Damage => _damageAmount;

    public event UnityAction Selected;
    public event UnityAction Deselected;
    public event UnityAction ValuesChanged;

    private void Awake()
    {
        _experience = new Experience();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IEffectRecepient enemy))
        {
            _enemies.Add(enemy);
            enemy.Died += OnDied;

            if (_enemies.Count == 1)
                _damageRoutine = StartCoroutine(DealDamage());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out IEffectRecepient enemy))
            HandleEnemyExit(enemy);
    }

    private void OnDied(IEffectRecepient enemy) => HandleEnemyExit(enemy);

    private void HandleEnemyExit(IEffectRecepient enemy)
    {
        _enemies.Remove(enemy);
        enemy.Died -= OnDied;

        if (_enemies.Count == 0)
            StopCoroutine(_damageRoutine);
    }

    private IEnumerator DealDamage()
    {
        yield return new WaitForSeconds(0.5f);

        while (true)
        {
            for (int i = _enemies.Count - 1; i >= 0; i--)
                _enemies[i].TakeDamage(new Damage(_damageAmount, DamageTypes.Fire, this));

            yield return new WaitForSeconds(1);
        }
    }

    public void Select()
    {
        Selected?.Invoke();
    }

    public void Deselect()
    {
        Deselected?.Invoke();
    }

    public void AddExperience(float amount)
    {
        _experience.Add(amount);
    }
}

public interface ITrapReadOnly
{ 
    int Damage { get; }
}

public class Experience : IExperienceReadOnly, IExpirience
{
    private int _level;
    private float _threashold;
    private float _value;

    public int Level => _level;
    public float Threshold => _threashold;
    public float Value => _value;

    public event UnityAction<IExperienceReadOnly> ValuesChanged;

    public Experience() => CalculateThreashold();

    public void Add(float amount)
    {
        _value += amount;
        if (_value >= _threashold)
            LevelUp();
    }

    private void LevelUp()
    {
        _level++;
        _value -= _threashold;
        CalculateThreashold();
    }

    private void CalculateThreashold() => _threashold = (_level + 1) * 25;
}

public interface IExpirience : IExperienceReadOnly
{
    event UnityAction<IExperienceReadOnly> ValuesChanged;
    void Add(float amount);
}

public interface IExperienceReadOnly
{ 
    public int Level { get; }
    public float Threshold { get; }
    public float Value { get; }
}
