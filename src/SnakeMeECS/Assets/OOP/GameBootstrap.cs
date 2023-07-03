using System;
using OOP.Services.AssetsAddressables;
using OOP.Services.Fabric.UIFactory;
using OOP.Services.Input;
using OOP.Services.Locator;
using UnityEngine;

namespace OOP
{
    public class GameBootstrap : MonoBehaviour, IService
    {
        [SerializeField] private PlayerInputActionReader _playerInputActionReader;
        
        private void Awake()
        {
            RegisterServices();
        }

        private void RegisterServices()
        {
            RegisterInputService();
            ServiceLocator.Container.RegisterSingleWithInterface<IAssetsAddressablesProvider>(new AssetsAddressablesProvider());
            ServiceLocator.Container.RegisterSingleWithInterface(this);
            //ServiceLocator.Container.RegisterSingleWithInterface<IUIFactory>(new UIFactory());
        }

        private void RegisterInputService()
        {
            ServiceLocator.Container.RegisterSingleWithInterface(_playerInputActionReader);
        }
    }
}
