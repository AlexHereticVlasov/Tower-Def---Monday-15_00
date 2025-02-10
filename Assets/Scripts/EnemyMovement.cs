using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private Path _path;
    [SerializeField] private float _speed;

    private int _index = 0;

    public void Init(Path path)
    {
        _path = path;
    }


    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _path[_index], Time.deltaTime * _speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out WayPoint _))
        {
            _index++;
        }
    }
}
