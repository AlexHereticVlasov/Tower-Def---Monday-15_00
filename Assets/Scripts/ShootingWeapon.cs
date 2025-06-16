using System.Collections.Generic;
using UnityEngine;

public abstract class BaseWeapon : MonoBehaviour, IDamageDeller
{
    [field: SerializeField] public Detector Detector { get; private set; }

    protected abstract void Attack();

    public void AddExperience(float amount)
    {

    }
}

public sealed class ShootingWeapon : BaseWeapon
{
    
    [SerializeField] private BulletBase _bullet;

    [SerializeField] private float _rate;

    private float _time;

    private void Update()
    {
        _time -= Time.deltaTime;


        if (_time <= 0)
        {
            Attack();
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

    protected override void Attack()
    {
        if (Detector.TryGetEnemy(out IEffectRecepient enemy))
        {
            var bullet = Instantiate(_bullet, transform.position, Quaternion.identity);
            bullet.Init(enemy.Transform, this);
            _time = _rate;
        }
    }

    
}

public sealed class MagicWeapon : BaseWeapon
{
    protected override void Attack()
    {
        if (Detector.TryGetEnemies(out IReadOnlyList<IEffectRecepient> targets))
        {
            foreach (var target in targets)
            {
                var damage = new Damage(1, DamageTypes.Fire, this);
                target.TakeDamage(damage);
                //ToDO: Reset Rate
            }
        }
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

