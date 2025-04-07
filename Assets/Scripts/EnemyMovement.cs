using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private Path _path;
    [SerializeField] private float _speed;

    private readonly List<float> _modifiers = new();

    private float _lessModifier = 1;
    private int _index = 0;

    public void Init(Path path)
    {
        _path = path;
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _path[_index], Time.deltaTime * _speed * _lessModifier);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out WayPoint _))
        {
            _index++;
        }
    }

    public void AddModifier(float value)
    {
        if (value < 0)
        {
            throw new System.Exception("Invalid speed modifier, value can't be negative");
        }

        _modifiers.Add(value);
        RecalculateActualModdifier();
    }

    public void RemoveModifier(float value)
    {
        _modifiers.Remove(value);
        RecalculateActualModdifier();
    }

    private void RecalculateActualModdifier()
    {
        if (_modifiers.Count == 0)
        {
            _lessModifier = 1;
            return;
        }

        _lessModifier = _modifiers.Min();
    }
}
