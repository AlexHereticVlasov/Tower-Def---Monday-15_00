using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour, ISelectable, IEffectRecepient
{
    [SerializeField] private EnemyMovement _movement;
    [SerializeField] private Stats _stats;

    [SerializeField] private List<Effect> activeEffects = new();

    public event UnityAction<Enemy> Died;
    public event UnityAction<Enemy> ReachedTarget;
    public event UnityAction Selected;
    public event UnityAction Deselected;
    public event UnityAction ValuesChanged;

    public IStatsReadOnly Stats => _stats;
    public EnemyMovement Movement => _movement;
    public Transform Transform => transform;

    public void Init(Path path)
    {
        _movement.Init(path);
    }

    private void OnEnable()
    {
        _stats.Died += OnDied;
    }

    private void Update()
    {
        float deltaTime = Time.deltaTime;

        for (int i = activeEffects.Count - 1; i >= 0; i--)
        {
            activeEffects[i].UpdateEffect(deltaTime);
            if (activeEffects[i].IsFinished)
            {
                activeEffects[i].Remove();
                activeEffects.RemoveAt(i);
            }
        }
    }

    private void OnDisable()
    {
        _stats.Died -= OnDied;
    }

    private void OnDied(IStatsReadOnly stats)
    {
        Died?.Invoke(this);
        Destroy(gameObject);
    }

    public void TakeDamage(Damage damage) => _stats.TakeDamage(damage);

    public void HandleReachTarget()
    {
        ReachedTarget?.Invoke(this);

        Destroy(gameObject);
        Debug.Log("Enemy Reachedd Target");
    }

    public void Select()
    {
        Selected?.Invoke();
    }

    public void Deselect()
    {
        Deselected?.Invoke();
    }

    public void ApplyEffect(Effect effect)
    {
        Effect existing = activeEffects.Find(e => e.GetType() == effect.GetType());

        if (existing != null && !existing.IsStackable)
        {
            existing.SetDuration(effect.Duration);
        }
        else
        {
            effect.Apply(this);
            activeEffects.Add(effect);
        }
    }

    public void AddSpeedModifier(float value) => Movement.AddModifier(value);

    public void RemoveSpeedModdifier(float value) => Movement.RemoveModifier(value);

    public void AddArmorModifier(float value) => _stats.AddArmorModifier(value);
    public void RemoveArmorModifier(float value) => _stats.RemoveArmorModifier(value);
    public void Heal(float healAmount) => _stats.Heal(healAmount);
}

public interface IDamageable
{
    public Transform Transform { get; }

    public event UnityAction<Enemy> Died;
    public event UnityAction<Enemy> ReachedTarget;

    void TakeDamage(Damage damage);
}

public interface IEffectRecepient : IDamageable
{
    void ApplyEffect(Effect effect);

    void AddSpeedModifier(float value);

    void RemoveSpeedModdifier(float value);

    void AddArmorModifier(float value);
    void RemoveArmorModifier(float value);

    void Heal(float healAmount);
}


public abstract class Effect
{
    public float Duration { get; protected set; }
    public bool IsStackable { get; protected set; }
    protected IEffectRecepient Target;
    protected float RemainingTime;

    public Effect(float duration, bool isStackable = false)
    {
        Duration = duration;
        RemainingTime = duration;
        IsStackable = isStackable;
    }

    public virtual void Apply(IEffectRecepient target)
    {
        Debug.Log($"Apply {GetType().Name}");
        Target = target;
    }

    public virtual void UpdateEffect(float deltaTime) => RemainingTime -= deltaTime;

    public virtual void Remove() => Debug.Log($"Remove {GetType().Name}");

    public void SetDuration(float duration)
    {
        if (RemainingTime > duration) return;

        RemainingTime = duration;
    }

    public bool IsFinished => RemainingTime <= 0;
}

public class DamageOverTimeEffect : Effect
{
    private readonly float _damagePerSecond;
    private readonly float _tickInterval = 0.2f;
    private float _timeSinceLastTick;
    protected DamageTypes Type;

    public DamageOverTimeEffect(float duration, float damagePerSecond,
        bool isStackable = false, float tickInterval = 0.2f )
        : base(duration, isStackable)
    {
        _damagePerSecond = damagePerSecond;
        _tickInterval = tickInterval;
    }

    public override void UpdateEffect(float deltaTime)
    {
        base.UpdateEffect(deltaTime);
        _timeSinceLastTick += deltaTime;

        if (_timeSinceLastTick >= _tickInterval)
        {
            Target.TakeDamage(new Damage(_damagePerSecond * _tickInterval, Type, null));
            _timeSinceLastTick = 0;
        }
    }
}

public class StunEffect : Effect
{
    public StunEffect(float duration) : base(duration) { }

    public override void Apply(IEffectRecepient target)
    {
        base.Apply(target);
        target.AddSpeedModifier(0);
        //target.Movement.AddModifier(0);
    }

    public override void Remove()
    {
        //Target.Movement.RemoveModifier(0);
        Target.RemoveSpeedModdifier(0);
    }
}

public class FreezeEffect : Effect
{
    private readonly float _speedMultiplier;

    public FreezeEffect(float duration, float multiplier) : base(duration)
    {
        _speedMultiplier = multiplier;
        IsStackable = true;
    }

    public override void Apply(IEffectRecepient target)
    {
        base.Apply(target);
        Target.AddSpeedModifier(_speedMultiplier);
    }

    public override void Remove()
    {
        Target.RemoveSpeedModdifier(_speedMultiplier);
    }
}

public class ArmorBreakEffect : Effect
{
    private float armorReduction;

    public ArmorBreakEffect(float duration, float reduction) : base(duration)
    {
        armorReduction = reduction;
    }

    public override void Apply(IEffectRecepient target)
    {
        base.Apply(target);
        Target.AddArmorModifier(armorReduction);
    }

    public override void Remove()
    {
        Target.RemoveArmorModifier(armorReduction);
    }
}

public class HealEffect : Effect
{
    private float healAmount;

    public HealEffect(float amount) : base(0)
    {
        healAmount = amount;
    }

    public override void Apply(IEffectRecepient target)
    {
        base.Apply(target);
        target.Heal(healAmount);
    }
}