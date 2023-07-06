using OOP.Data;
using OOP.Infrastructure.ProjectStateMachine.Base;
using OOP.Services.Fabric.UIFactory;
using OOP.Services.WebSocketsService;
using UnityEngine.AddressableAssets;

namespace OOP.Infrastructure.ProjectStateMachine.States
{
    public class GameLoadingState : IState<GameBootstrap>, IEnterable, IExitable
    {
        public GameBootstrap Initializer { get; private set; }
        
        private readonly IUIFactory _uiFactory;
        private readonly IWebSocketsService _webSocketsService;

        public GameLoadingState(GameBootstrap initializer, IUIFactory uiFactory, IWebSocketsService webSocketsService)
        {
            Initializer = initializer;
            _uiFactory = uiFactory;
            _webSocketsService = webSocketsService;
        }

        public void OnEnter()
        {
            ShowUI();
            
            _webSocketsService.OnGameInitialized += StartGame;
        }

        public void OnExit()
        {
            _webSocketsService.OnGameInitialized -= StartGame;
        }


        private async void StartGame()
        {
            var asyncOperationHandle = Addressables.LoadSceneAsync(AssetsAddressableConstants.GAMEPLAY_SCENE);
            await asyncOperationHandle.Task;
            
            Initializer.StateMachine.SwitchState<GameplayState>();

            HideUI();
        }

        private void ShowUI()
        {
            _uiFactory.CreateGameLoadingScreen();
        }

        private void HideUI()
        {
            _uiFactory.DestroyGameLoadingScreen();
        }
    }
}