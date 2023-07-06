using ElRaccoone.WebSockets;
using Newtonsoft.Json;
using OOP.Data;
using OOP.Infrastructure.ProjectStateMachine.States;
using UnityEngine;
using UnityEngine.Events;

namespace OOP.Services.WebSocketsService
{
    public class WebSocketsService : IWebSocketsService
    {
        public event UnityAction OnGameInitialized;
        
        private const string URL = "wss://dev.match.qubixinfinity.io/snake";

        private readonly WSConnection _wsConnection = new(URL);
        
        public long GameID;
        public int ApplesCount;
        
        public WebSocketsService()
        {
            _wsConnection.OnConnected(() =>
            {
                Debug.Log("WS Connected!");
            });
            
            _wsConnection.OnDisconnected(() => 
            {
                Debug.Log("WS Disconnected!");
            });

            _wsConnection.OnError(error => 
            {
                Debug.Log("WS Error " + error);
            });
            
            _wsConnection.OnMessage(message =>
            {
                var messageType = message.ToDeserialized<BaseTypeDeserializer>();

                switch (messageType.type)
                {
                    case "game-created":
                    {
                        var gameCreated = message.ToDeserialized<GameGet>();

                        GameID = gameCreated.payload.id;
                        
                        OnGameInitialized?.Invoke();
                        
                        break;
                    }
                }
            });

            _wsConnection.Connect();
        }

        public void GetNewGame()
        {
            var newGame = new GameGet
            {
                type = "create-game"
            };
            
            _wsConnection.SendMessage(newGame.ToJson());
        }
    }
}