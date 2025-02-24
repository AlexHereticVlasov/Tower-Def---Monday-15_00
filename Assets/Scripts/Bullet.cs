public sealed class Bullet : BulletBase
{
    protected override void Apply(Enemy enemy)
    {
        enemy.TakeDamage(Damage);
    }
}
