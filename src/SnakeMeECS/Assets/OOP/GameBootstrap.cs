using System;
using OOP.Services.Input;
using OOP.Services.Locator;
using UnityEngine;

namespace OOP
{
    public class GameBootstrap : MonoBehaviour
    {
        [SerializeField] private PlayerInputActionReader _playerInputActionReader;
        
        private void Awake()
        {
            RegisterServices();
        }

        private void RegisterServices()
        {
            RegisterInputService();
        }

        private void RegisterInputService()
        {
            ServiceLocator.Container.RegisterSingleWithInterface(_playerInputActionReader);
        }
    }
}
