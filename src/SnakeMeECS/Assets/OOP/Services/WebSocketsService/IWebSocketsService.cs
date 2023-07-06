using OOP.Services.Locator;
using UnityEngine.Events;

namespace OOP.Services.WebSocketsService
{
    public interface IWebSocketsService : IService
    {
        public event UnityAction OnGameInitialized;
        public void GetNewGame();
    }
}