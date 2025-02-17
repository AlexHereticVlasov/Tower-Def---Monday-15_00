using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TrapBase : MonoBehaviour, ISelectable
{
    private List<Enemy> _enemies = new();
    private int _chargeAmount;

    private Coroutine _damageRoutine;

    public event UnityAction Selected;
    public event UnityAction Deselected;
    public event UnityAction ValuesChanged;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Enemy enemy))
        {
            _enemies.Add(enemy);
            enemy.Died += OnDied;

            if (_enemies.Count == 1)
                _damageRoutine = StartCoroutine(DealDamage());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Enemy enemy))
            HandleEnemyExit(enemy);
    }

    private void OnDied(Enemy enemy) => HandleEnemyExit(enemy);

    private void HandleEnemyExit(Enemy enemy)
    {
        _enemies.Remove(enemy);
        enemy.Died -= OnDied;

        if (_enemies.Count == 0)
            StopCoroutine(_damageRoutine);
    }

    private IEnumerator DealDamage()
    {
        yield return new WaitForSeconds(0.5f);

        while (true)
        {
            for (int i = _enemies.Count - 1; i >= 0; i--)
                _enemies[i].TakeDamage(10);

            yield return new WaitForSeconds(1);
        }
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
