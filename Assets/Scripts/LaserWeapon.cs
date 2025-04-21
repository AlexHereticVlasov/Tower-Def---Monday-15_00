using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public sealed class LaserWeapon : BaseWeapon
{
    [SerializeField] LineRenderer _line;

    private void Start()
    {
        _line.SetPosition(0, transform.position);
        _line.SetPosition(1, transform.position);
    }

    private void Update()
    {
        Attack();
    }

    protected override void Attack()
    {
        if (Detector.TryGetEnemy(out IEffectRecepient enemy))
        {
            _line.SetPosition(0, transform.position);
            _line.SetPosition(1, enemy.Transform.position);

            enemy.TakeDamage(new Damage(5 * Time.deltaTime, DamageTypes.Fire, this));
            return;
        }

        _line.SetPosition(1, transform.position);
    }
}
