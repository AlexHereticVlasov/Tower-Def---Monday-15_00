using UnityEngine;

public abstract class BulletBase : MonoBehaviour
{
    [SerializeField] private float _speed = 5;
    [field: SerializeField] public float Damage { get; private set; } =  5;
    
    public IDamageDeller Deller { get; private set; }

    private Transform _enemy;

    public void Init(Transform enemy, IDamageDeller deller)
    {
        _enemy = enemy;
        Deller = deller;
    }

    private void Update()
    {
        if (_enemy == null)
        {
            Destroy(gameObject);
            return;
        }

        transform.position = Vector3.MoveTowards(transform.position, _enemy.position, _speed * Time.deltaTime);
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IEffectRecepient enemy))
        {
            //enemy.TakeDamage(_damage);
            Apply(enemy);
        }
    }

    protected abstract void Apply(IEffectRecepient enemy);
}
