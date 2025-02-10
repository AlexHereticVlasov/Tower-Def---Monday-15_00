using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    //[SerializeField] private Enemy _template;
    [SerializeField] private Path _path;
    [SerializeField] private Stream _stream;

    private IEnumerator Start()
    {
        //InvokeRepeating(nameof(Spawn), 0, 0.5f);

        Debug.Log(_stream.EnemiesCount);

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
    }
}
