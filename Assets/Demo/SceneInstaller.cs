using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SceneInstaller : MonoInstaller
{
    [SerializeField] private Transform[] waypoints;
    public override void InstallBindings()
    {
        Container.Bind<PlayerConfig>().AsSingle();
        Container.BindInstance(waypoints).AsSingle();
    }
}
