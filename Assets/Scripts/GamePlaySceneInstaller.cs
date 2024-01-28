using UnityEngine;
using Zenject;

public class GamePlaySceneInstaller : MonoInstaller
{
    [SerializeField] private CharacterStats _characterStats;
    [SerializeField] private PlayerController _playerController;

    public override void InstallBindings()
    {
        BindPlayer();
    }

    private void BindPlayer()
    {
        Container.Bind<CharacterStats>().FromInstance(_characterStats);
        Container.Bind<PlayerController>().FromInstance(_playerController);
    }
}