using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
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
        if (_detector.TryGetEnemy(out Enemy enemy))
        {
            var bullet = Instantiate(_bullet, transform.position, Quaternion.identity);
            bullet.Init(enemy);
            _time = _rate;
        }
    }
}

public struct Damage
{
    public readonly float Value;
    public readonly DamageTypes Type;

    public Damage(float value, DamageTypes type)
    {
        Value = value;
        Type = type;
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
