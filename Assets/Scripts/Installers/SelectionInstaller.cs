using UnityEngine;
using Zenject;

public sealed class SelectionInstaller : MonoInstaller
{
    [SerializeField] private Selection _selection;

    public override void InstallBindings()
    {
        Container.Bind<ISelection>().FromInstance(_selection).AsSingle().NonLazy();
        Container.QueueForInject(_selection);
    }
}

public sealed class PlayerLifesInstaller : MonoInstaller
{
    [SerializeField] private PlayerLifes _selection;

    public override void InstallBindings()
    {
        Container.Bind<PlayerLifes>().FromInstance(_selection).AsSingle().NonLazy();
        Container.QueueForInject(_selection);
    }
}
