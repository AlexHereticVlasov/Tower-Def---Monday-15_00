using UnityEngine;

public abstract class BulletBase : MonoBehaviour
{
    [SerializeField] private float _speed = 5;
    [SerializeField] private float _damage = 5;

    private Enemy _enemy;

    public void Init(Enemy enemy)
    {
        _enemy = enemy;
    }

    private void Update()
    {
        if (_enemy == null)
        {
            Destroy(gameObject);
            return;
        }

        transform.position = Vector3.MoveTowards(transform.position, _enemy.transform.position, _speed * Time.deltaTime);
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Enemy enemy))
        {
            enemy.TakeDamage(_damage);
        }
    }
}
