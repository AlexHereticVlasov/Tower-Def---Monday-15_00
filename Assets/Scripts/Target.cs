using UnityEngine;
using UnityEngine.Events;

public class Target : MonoBehaviour
{
    public UnityAction<Enemy> EnemyReachedTarget;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Enemy enemy))
        {
            enemy.HandleReachTarget();
            EnemyReachedTarget?.Invoke(enemy);
        }
    }
}
