using UnityEngine;
using Zenject;

public class BuildingBuilder : MonoBehaviour
{
    [Inject] private readonly StorageBase _storage;

    public void TryBuildTower(BuildingData data, TowerNode node)
    {
        if (CanBuild(node, data))
        {
            node.Build(data);
        }
    }

    private bool CanBuild(TowerNode node, BuildingData data)
    {
        if (data is null)
            throw new System.Exception("Building data is not set");

        if (node.IsEmpty == false)
        {
            return false;
        }

        if (_storage.TryGet(data.Cost) == false)
        {
            Debug.Log("Нужно больше золота");
            return false;
        }

        return true;
    }
}
