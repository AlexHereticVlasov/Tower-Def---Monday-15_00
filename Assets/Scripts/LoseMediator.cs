using UnityEngine;

public sealed class LoseMediator : MonoBehaviour
{
    [SerializeField] private PlayerLifes _lifes;
    [SerializeField] private Target[] _targets;

    private void OnEnable()
    {
        _lifes.Lose += OnLose;

        foreach (var target in _targets)
        {
            target.EnemyReachedTarget += OnEnemyReachedTarget;
        }
    }

    private void OnDisable()
    {
        _lifes.Lose -= OnLose;

        foreach (var target in _targets)
        {
            target.EnemyReachedTarget -= OnEnemyReachedTarget;
        }
    }

    private void OnEnemyReachedTarget(Enemy enemy)
    {
        //TODO: Add Enemy cost;
        int cost = 1;
        _lifes.Decrease(cost);
    }

    private void OnLose()
    {
        //ToDo: Show Lose panel
    }
}


namespace Speed
{
}

namespace UI
{
    public sealed class GameSpeedViev : MonoBehaviour
    { 
    
    }
}