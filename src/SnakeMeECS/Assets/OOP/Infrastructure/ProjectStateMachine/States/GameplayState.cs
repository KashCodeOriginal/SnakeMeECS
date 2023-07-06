using OOP.Infrastructure.ProjectStateMachine.Base;
using OOP.Services.Fabric.UIFactory;

namespace OOP.Infrastructure.ProjectStateMachine.States
{
    public class GameplayState : IState<GameBootstrap>, IEnterable, IExitable
    {
        private readonly IUIFactory _uiFactory;
        public GameBootstrap Initializer { get; private set; }

        public GameplayState(GameBootstrap initializer, IUIFactory uiFactory)
        {
            _uiFactory = uiFactory;
            Initializer = initializer;
        }

        public void OnEnter()
        {
            _uiFactory.CreateGameplayScreen();
        }
        
        public void OnExit()
        {
            _uiFactory.DestroyGameplayScreen();
        }
    }
}