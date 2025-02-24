using UnityEngine;

public sealed class ExploingBullet : BulletBase
{
    [SerializeField] private float _radius = 5;
    [SerializeField] private LayerMask _mask;

    protected override void Apply(Enemy enemy)
    {
        var targets =  Physics.OverlapSphere(transform.position, _radius, _mask);

        foreach (var unit in targets)
        {
            if (unit.TryGetComponent(out Enemy enemy1))
            {
                //ToDo: Calculate damage from distance to target
                enemy1.TakeDamage(Damage);
            }
        }
    }
}
