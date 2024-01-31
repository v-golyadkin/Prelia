using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class LocationInstaller : MonoInstaller
{
    public Transform SpawnPoint;
    public GameObject PlayerPrefab;

    public override void InstallBindings()
    {
        PlayerController playerController = Container.InstantiatePrefabForComponent<PlayerController>(PlayerPrefab, SpawnPoint.position, Quaternion.identity, null);

        //Container.Bind<GameObject>().FromInstance(PlayerPrefab).AsSingle();
        Container.Bind<PlayerController>().FromInstance(playerController).AsSingle();
    }
}
