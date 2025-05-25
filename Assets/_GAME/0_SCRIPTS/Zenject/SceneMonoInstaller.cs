using UnityEngine;
using Zenject;

public class SceneMonoInstaller : MonoInstaller
{
    [SerializeField] GameSettings gameSettings;
    [SerializeField] UIManager uIManager;
    [SerializeField] FigureSpawner spawner;
    [SerializeField] ActionBar actionBar;

    public override void InstallBindings()
    {
        Container.Bind<GameSettings>().FromInstance(gameSettings).AsSingle();
        Container.Bind<UIManager>().FromInstance(uIManager).AsSingle();
        Container.Bind<FigureSpawner>().FromInstance(spawner).AsSingle();
        Container.Bind<ActionBar>().FromInstance(actionBar).AsSingle();
        Container.Bind<MyGameManager>().AsSingle().NonLazy(); 
    }
}