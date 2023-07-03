using System.Threading.Tasks;
using OOP.Services.Locator;
using UnityEngine;

namespace OOP.Services.Fabric.UIFactory
{
    public interface IUIFactory : IUIInfo, IService
    {
        public Task<GameObject> CreateMenuScreen();
        public void DestroyMenuScreen();
        public Task<GameObject> CreateGameLoadingScreen();
        public void DestroyGameLoadingScreen();
        public Task<GameObject> CreateGameplayScreen();
        public void DestroyGameplayScreen();
    }
}