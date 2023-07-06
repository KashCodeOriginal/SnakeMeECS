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

        public async void OnEnter()
        {
            var asyncOperationHandle = Addressables.LoadSceneAsync(AssetsAddressableConstants.MAIN_MENU_SCENE);
            await asyncOperationHandle.Task;
        
            OnLoadComplete();
        }

        private void OnLoadComplete()
        {
            Initializer.StateMachine.SwitchState<MainMenuState>();
        }
    }
    
    public class GameplayState : IState<GameBootstrap>
    {
        public GameBootstrap Initializer { get; }

        public GameplayState(GameBootstrap initializer)
        {
            Initializer = initializer;
        }
    }
}