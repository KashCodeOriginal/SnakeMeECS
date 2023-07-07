using OOP.UI;
using UnityEngine;

namespace OOP.Services.Fabric.UIFactory
{
    public interface IUIInfo
    {
        public GameObject MainMenuScreen { get; }
        public GameObject GameLoadingScreen { get; }
        public GameObject GameplayScreen { get; }
        public GameplayScreen GameplayScreenComponent { get; }
    }
}