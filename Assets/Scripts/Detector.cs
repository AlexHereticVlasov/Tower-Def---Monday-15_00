using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class Detector : MonoBehaviour
{
    private readonly List<IEffectRecepient> _enemiesInRange = new();

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IEffectRecepient enemy))
        {
            _enemiesInRange.Add(enemy);
            enemy.Died += OnDied;
            enemy.ReachedTarget += OnReachedTarget;
        }
    }

    private void OnReachedTarget(IEffectRecepient enemy) => HandleExitTrigger(enemy);

    private void OnDied(IEffectRecepient enemy) => HandleExitTrigger(enemy);

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out IEffectRecepient enemy))
            HandleExitTrigger(enemy);
    }

    private void HandleExitTrigger(IEffectRecepient enemy)
    {
        enemy.Died -= OnDied;
        enemy.ReachedTarget -= OnReachedTarget;
        _enemiesInRange.Remove(enemy);
    }

    public bool TryGetEnemy(out IEffectRecepient enemy)
    {
        enemy = GetEnemy();
        return enemy != null;
    }

    public bool TryGetEnemies(out IReadOnlyList<IEffectRecepient> enemies)
    {
        enemies = _enemiesInRange;
        return _enemiesInRange.Count > 0;
    }

    private IEffectRecepient GetEnemy()
    {
        if (_enemiesInRange.Count == 0)
            return null;

        var result = _enemiesInRange[0];
        float distance = float.MaxValue;

        foreach (var enemy in _enemiesInRange)
        {
            if (enemy == null) continue;

            float currentDistance = Vector3.Distance(transform.position, enemy.Transform.position);

            if (currentDistance < distance)
            {
                distance = currentDistance;
                result = enemy;
            }
        }

        return result;
    }
}
