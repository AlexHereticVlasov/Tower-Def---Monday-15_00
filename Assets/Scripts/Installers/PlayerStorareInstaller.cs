using UnityEngine;
using Zenject;

public sealed class PlayerStorareInstaller : MonoInstaller
{
    [SerializeField] private PlayerStorage _playerStorage;

    public override void InstallBindings()
    {
        Container.Bind<StorageBase>().FromInstance(_playerStorage).AsSingle().NonLazy();
        Container.QueueForInject(_playerStorage);
    }
}
