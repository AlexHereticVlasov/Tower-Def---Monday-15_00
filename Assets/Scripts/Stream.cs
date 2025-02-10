using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(Stream), menuName = nameof(ScriptableObject) + " / " + nameof(Stream))]
public class Stream : ScriptableObject
{
    [SerializeField] private Wave[] _waves;
    [SerializeField] private float _timeBetweenWaves;

    public IEnumerable<Wave> Waves => _waves;
    public float TimeBetweenWaves => _timeBetweenWaves;

    public int EnemiesCount
    {
        get 
        {
            int count = 0;

            foreach (var wave in _waves)
            {
                foreach (var enemy in wave.Enemies)
                {
                    count++;
                }
            }

            return count;
        }
    }

    private void OnValidate()
    {
        if (_timeBetweenWaves <= 0)
        {
            _timeBetweenWaves = 1f;
        }
    }
}
