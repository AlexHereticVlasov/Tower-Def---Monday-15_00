using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class Detector : MonoBehaviour
{
    [SerializeField] private List<Enemy> _enemiesInRange;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Enemy enemy))
        {
            _enemiesInRange.Add(enemy);
            enemy.Died += OnDied;
            enemy.ReachedTarget += OnReachedTarget;
        }
    }

    private void OnReachedTarget(Enemy enemy) => HandleExitTrigger(enemy);

    private void OnDied(Enemy enemy) => HandleExitTrigger(enemy);

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Enemy enemy))
            HandleExitTrigger(enemy);
    }

    private void HandleExitTrigger(Enemy enemy)
    {
        enemy.Died -= OnDied;
        enemy.ReachedTarget -= OnReachedTarget;
        _enemiesInRange.Remove(enemy);
    }

    public bool TryGetEnemy(out Enemy enemy)
    {
        enemy = GetEnemy();
        return enemy != null;
    }

    private Enemy GetEnemy()
    {
        if (_enemiesInRange.Count == 0)
            return null;

        Enemy result = _enemiesInRange[0];
        float distance = float.MaxValue;

        foreach (var enemy in _enemiesInRange)
        {
            if (enemy == null) continue;

            float currentDistance = Vector3.Distance(transform.position, enemy.transform.position);

            if (currentDistance < distance)
            {
                distance = currentDistance;
                result = enemy;
            }
        }

        return result;
    }
}
