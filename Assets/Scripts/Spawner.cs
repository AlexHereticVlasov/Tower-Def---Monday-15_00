using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Path _path;
    [SerializeField] private Stream _stream;

    public event UnityAction<Enemy> EnemySpawned;

    private IEnumerator Start()
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

public abstract class StorageBase : MonoBehaviour
{
    public int Amount { get; private set; } = 200;

    public event UnityAction<int> AmountChanged;

    public void Init(int amount)
    {
        Amount = amount;
        AmountChanged?.Invoke(Amount);
    }

    public void Add(int amount)
    {
        Amount += amount;
        AmountChanged?.Invoke(Amount);
    }

    public bool TryGet(int amount)
    {
        if (Amount >= amount)
        {
            Amount -= amount;
            AmountChanged?.Invoke(Amount);
            return true;
        }

        return false;
    }

}
