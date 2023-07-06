using System.Threading.Tasks;
using OOP.UI;
using OOP.Services.Fabric.UIFactory;
using OOP.Infrastructure.ProjectStateMachine.Base;
using OOP.Services.WebSocketsService;

namespace OOP.Infrastructure.ProjectStateMachine.States
{
    public class MainMenuState : IState<GameBootstrap>, IEnterable, IExitable
    {
        public GameBootstrap Initializer { get; }
        
        private readonly IUIFactory _uiFactory;
        private readonly IWebSocketsService _webSocketsService;

        private MainMenuScreen _mainMenuScreen;

        public MainMenuState(GameBootstrap initializer, IUIFactory uiFactory, IWebSocketsService webSocketsService)
        {
            _uiFactory = uiFactory;
            _webSocketsService = webSocketsService;
            Initializer = initializer;
        }

        public async void OnEnter()
        {
            await ShowUI();
        }

        private void OnPlayButtonClicked()
        {
            _webSocketsService.PostNewGame();
            
            Initializer.StateMachine.SwitchState<GameLoadingState>();
        }

        private async Task ShowUI()
        {
            var mainMenuScreenInstance = await _uiFactory.CreateMenuScreen();
            _mainMenuScreen = mainMenuScreenInstance.GetComponent<MainMenuScreen>();

            _mainMenuScreen.OnPlayButtonClicked += OnPlayButtonClicked;
        }

        private void HideUI()
        {
            _uiFactory.DestroyMenuScreen();
        }

        public void OnExit()
        {
            _mainMenuScreen.OnPlayButtonClicked -= OnPlayButtonClicked;

            HideUI();
        }
    }
}