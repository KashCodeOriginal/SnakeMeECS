using OOP.Data;
using OOP.Infrastructure.ProjectStateMachine.Base;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace OOP.Infrastructure.ProjectStateMachine.States
{
    public class BootstrapState : IEnterable, IState<GameBootstrap>
    {
        public GameBootstrap Initializer { get; }

        public BootstrapState(GameBootstrap initializer)
        {
            Initializer = initializer;
        }

        public void OnEnter()
        {
            Initializer.StateMachine.SwitchState<MainMenuState>();
        }
    }
}