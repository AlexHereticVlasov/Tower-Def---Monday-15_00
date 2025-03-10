
using UnityEngine;

public sealed class MoneyMediator : MonoBehaviour
{
    [SerializeField] private StorageBase _storage;
    [SerializeField] private Spawner[] _spawners;

    private void OnEnable()
    {
        foreach (var spawner in _spawners)
        {
            spawner.EnemySpawned += OnEnemySpawned;
        }
    }

    private void Start()
    {
        _storage.Init(199);
    }

    private void OnDisable()
    {
        foreach (var spawner in _spawners)
        {
            spawner.EnemySpawned -= OnEnemySpawned;
        }
    }

    private void OnEnemySpawned(Enemy enemy)
    {
        enemy.Died += OnDied;
        enemy.ReachedTarget += OnReachedTarget;
    }

    private void OnReachedTarget(Enemy enemy)
    {
        enemy.Died -= OnDied;
        enemy.ReachedTarget -= OnReachedTarget;
    }

    private void OnDied(Enemy enemy)
    {
        enemy.Died -= OnDied;
        enemy.ReachedTarget -= OnReachedTarget;

        _storage.Add(5);
    }
}

