public sealed class Bullet : BulletBase
{
    protected override void Apply(IEffectRecepient enemy)
    {
        enemy.TakeDamage(Damage);
        //enemy.ApplyEffect(new DamageOverTimeEffect(10, 0.5f, false));
        enemy.ApplyEffect(new StunEffect(0.25f));
        //enemy.ApplyEffect(new FreezeEffect(5, 0.5f));
    }
}
