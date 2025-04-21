using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Path _path;
    [SerializeField] private Stream _stream;

    public event UnityAction<Enemy> EnemySpawned;

    public void Launch() => StartCoroutine(StartSpawn());

    private IEnumerator StartSpawn()
    {
        foreach (var wave in _stream.Waves)
        {
            foreach (var enemy in wave.Enemies)
            {
                Spawn(enemy);
                yield return new WaitForSeconds(wave.Rate);
            }

            yield return new WaitForSeconds(_stream.TimeBetweenWaves);
        }
    }


    private void Spawn(Enemy template)
    {
        var enemy = Instantiate(template, transform.position, Quaternion.identity, transform);
        enemy.Init(_path);
        EnemySpawned?.Invoke(enemy);

    }
}
