using OOP.Infrastructure.ProjectStateMachine.Base;

namespace OOP.Infrastructure.ProjectStateMachine.States
{
    public class GameplayState : IState<GameBootstrap>
    {
        public GameBootstrap Initializer { get; }

        public GameplayState(GameBootstrap initializer)
        {
            Initializer = initializer;
        }
    }
}