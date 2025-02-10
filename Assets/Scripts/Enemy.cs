using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyMovement _movement;
    [SerializeField] private Health _health;

    public event UnityAction<Enemy> Died;
    public event UnityAction<Enemy> ReachedTarget;

    public void Init(Path path)
    {
        _movement.Init(path);
    }

    private void OnEnable()
    {
        _health.Died += OnDied;
    }

    private void OnDisable()
    {
        _health.Died -= OnDied;
    }

    private void OnDied()
    {
        Died?.Invoke(this);
        Destroy(gameObject);
    }

    internal void TakeDamage(float damage)
    {
        _health.TakeDamage(damage);   
    }

    public void HandleReachTarget()
    {
        ReachedTarget?.Invoke(this);

        Destroy(gameObject);
        Debug.Log("Enemy Reachedd Target");
    }
}
