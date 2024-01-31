using UnityEngine;
using Zenject;

public class PlayerInstaller : MonoInstaller
{
    //[SerializeField] private CharacterStats _characterStats;
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private Unit _playerUnit;

    public override void InstallBindings()
    {
        BindPlayer();
    }

    private void BindPlayer()
    {
        //Container.Bind<Unit>().FromInstance(_playerUnit).AsSingle();
        //Container.Bind<CharacterStats>().FromInstance(_characterStats);
        //Container.Bind<PlayerController>().FromInstance(_playerController).AsSingle();
    }
}