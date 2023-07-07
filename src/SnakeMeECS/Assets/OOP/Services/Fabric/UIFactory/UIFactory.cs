using System.Threading.Tasks;
using OOP.Data;
using OOP.Services.AssetsAddressables;
using OOP.UI;
using UnityEngine;

namespace OOP.Services.Fabric.UIFactory
{
    public class UIFactory : IUIFactory
    {
        public UIFactory(IAssetsAddressablesProvider assetsAddressablesProvider)
        {
            _assetsAddressablesProvider = assetsAddressablesProvider;
        }

        private readonly IAssetsAddressablesProvider _assetsAddressablesProvider;

        public GameObject MainMenuScreen { get; private set; }

        public GameObject GameLoadingScreen { get; private set; }

        public GameObject GameplayScreen { get; private set; }

        public GameplayScreen GameplayScreenComponent { get; private set; }


        public async Task<GameObject> CreateMenuScreen()
        {
            var screenPrefab = await _assetsAddressablesProvider.GetAsset<GameObject>(AssetsAddressableConstants.MAIN_MENU_SCREEN);
            MainMenuScreen = Object.Instantiate(screenPrefab);

            return MainMenuScreen;
        }

        public void DestroyMenuScreen()
        {
            Object.Destroy(MainMenuScreen);
        }

        public async Task<GameObject> CreateGameLoadingScreen()
        {
            var screenPrefab = await _assetsAddressablesProvider.GetAsset<GameObject>(AssetsAddressableConstants.GAME_LOADING_SCREEN);
            GameLoadingScreen = Object.Instantiate(screenPrefab);

            return GameLoadingScreen;
        }

        public void DestroyGameLoadingScreen()
        {
            Object.Destroy(GameLoadingScreen);
        }

        public async Task<GameObject> CreateGameplayScreen()
        {
            var screenPrefab = await _assetsAddressablesProvider.GetAsset<GameObject>(AssetsAddressableConstants.GAMEPLAY_SCREEN);
            GameplayScreen = Object.Instantiate(screenPrefab);

            GameplayScreenComponent = GameplayScreen.GetComponent<GameplayScreen>();

            return GameplayScreen;
        }

        public void DestroyGameplayScreen()
        {
            Object.Destroy(GameplayScreen);
        }
    }
}