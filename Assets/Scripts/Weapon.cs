using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour, IDamageDeller
{
    [SerializeField] private Detector _detector;
    [SerializeField] private BulletBase _bullet;

    [SerializeField] private float _rate;

    private float _time;

    private void Update()
    {
        _time -= Time.deltaTime;


        if (_time <= 0)
        {
            Shoot();
        }
    }

    private void OnValidate()
    {
        if (_rate <= 0)
        {
            _rate = 1;
            Debug.LogWarning("Rate can't be less or equal zero");
        }
    }

    private void Shoot()
    {
        if (_detector.TryGetEnemy(out IEffectRecepient enemy))
        {
            var bullet = Instantiate(_bullet, transform.position, Quaternion.identity);
            bullet.Init(enemy.Transform, this);
            _time = _rate;
        }
    }

    public void AddExperience(float amount)
    {
        
    }
}

public struct Damage
{
    public readonly float Value;
    public readonly DamageTypes Type;
    public readonly IDamageDeller DamageDeller;

    public Damage(float value, DamageTypes type, IDamageDeller deller)
    {
        Value = value;
        Type = type;
        DamageDeller = deller;
    }
}

public enum DamageTypes
{ 
    Weapon = 0,
    Fire = 1,
    Frost = 2,
    Lighting = 3,
    Poison = 4
}

public interface IDamageDeller
{
    void AddExperience(float amount);
}
