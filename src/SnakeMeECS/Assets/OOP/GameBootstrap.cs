using OOP.Infrastructure.ProjectStateMachine.Base;
using OOP.Infrastructure.ProjectStateMachine.States;
using OOP.Services.AssetsAddressables;
using OOP.Services.Fabric.UIFactory;
using OOP.Services.Input;
using OOP.Services.Locator;
using OOP.Services.WebSocketsService;
using UnityEngine;

namespace OOP
{
    public class GameBootstrap : MonoBehaviour, IService
    {
        [SerializeField] private PlayerInputActionReader _playerInputActionReader;

        public StateMachine<GameBootstrap> StateMachine { get; private set; }

        private void Awake()
        {
            RegisterServices();
            
            DontDestroyOnLoad(this);
        }

        private void RegisterServices()
        {
            ServiceLocator.Container.RegisterSingleWithInterface(_playerInputActionReader);
            ServiceLocator.Container.RegisterSingleWithInterface<IWebSocketsService>(new WebSocketsService(this));
            ServiceLocator.Container.RegisterSingleWithInterface<IAssetsAddressablesProvider>(new AssetsAddressablesProvider());
            ServiceLocator.Container.RegisterSingleWithInterface(this);
            ServiceLocator.Container.RegisterSingleWithInterface<IUIFactory>(
                new UIFactory(GetAsset<IAssetsAddressablesProvider>()));
            
            StateMachine = new StateMachine<GameBootstrap>(new BootstrapState(this),
                new MainMenuState(this, GetAsset<IUIFactory>(), GetAsset<IWebSocketsService>()),
                new GameLoadingState(this, GetAsset<IUIFactory>(), GetAsset<IWebSocketsService>()),
                new GameplayState(this, GetAsset<IUIFactory>()));
            
            StateMachine.SwitchState<BootstrapState>();
        }

        private T GetAsset<T>() where T : IService
        {
            return ServiceLocator.Container.Single<T>();
        }
    }
}
