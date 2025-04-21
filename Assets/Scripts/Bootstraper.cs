using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bootstraper : MonoBehaviour
{
    [SerializeField] private Spawner[] _spawners;

    private void Start()
    {
        
    }

    public void StartGame()
    {
        foreach (var spawner in _spawners)
        {
            spawner.Launch();
        }
    }
}
