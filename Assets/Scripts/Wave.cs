using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(Wave), menuName = nameof(ScriptableObject) + " / " + nameof(Wave))]

public class Wave : ScriptableObject
{
    [SerializeField] private Enemy[] _enemies;
    [SerializeField] private float _rate;

    public IEnumerable<Enemy> Enemies => _enemies;
    public float Rate => _rate;

    private void OnValidate()
    {
        if (_rate <= 0)
        {
            _rate = 0.1f;
        }
    }
}
