using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour, ISelectable
{
    [SerializeField] private EnemyMovement _movement;
    [SerializeField] private Stats _health;

    public event UnityAction<Enemy> Died;
    public event UnityAction<Enemy> ReachedTarget;
    public event UnityAction Selected;
    public event UnityAction Deselected;
    public event UnityAction ValuesChanged;

    public IStatsReadOnly Stats => _health;

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

    private void OnDied(IStatsReadOnly stats)
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

    public void Select()
    {
        Selected?.Invoke();
    }

    public void Deselect()
    {
        Deselected?.Invoke();
    }
}
