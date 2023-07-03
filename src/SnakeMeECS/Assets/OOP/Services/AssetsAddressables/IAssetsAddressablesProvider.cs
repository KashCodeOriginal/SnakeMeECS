using UnityEngine;
using OOP.Services.Locator;
using System.Threading.Tasks;
using UnityEngine.AddressableAssets;

namespace OOP.Services.AssetsAddressables
{
    public interface IAssetsAddressablesProvider : IService
    {
        public void Initialize();
        public Task<T> GetAsset<T>(string address) where T : Object;
        public Task<T> GetAsset<T>(AssetReference assetReference) where T : Object;
        public void CleanUp();
    }
}