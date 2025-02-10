using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] private float _value;

    public event UnityAction Died;

    public void TakeDamage(float damage)
    {
        _value -= damage;
        if (_value <= 0)
        {
            Die();
        }

    }

    private void Die()
    {
        Died?.Invoke();
    }
}
