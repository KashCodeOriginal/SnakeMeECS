using OOP.Infrastructure.ProjectStateMachine.Base;

namespace OOP.Infrastructure.ProjectStateMachine.States
{
    public class BootstrapState : IEnterable
    {
        public void OnEnter()
        {
            throw new System.NotImplementedException();
        }
    }
    
    public class MainMenuState : IEnterable, IExitable
    {
        public void OnEnter()
        {
            throw new System.NotImplementedException();
        }

        public void OnExit()
        {
            throw new System.NotImplementedException();
        }
    }
}