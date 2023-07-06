using OOP.Data;
using UnityEngine;
using UnityEngine.Events;
using ElRaccoone.WebSockets;
using OOP.Infrastructure.ProjectStateMachine.States;

namespace OOP.Services.WebSocketsService
{
    public class WebSocketsService : IWebSocketsService
    {
        public event UnityAction OnGameInitialized;
        
        private const string URL = "wss://dev.match.qubixinfinity.io/snake";

        private readonly WSConnection _wsConnection = new(URL);

        private long _gameID;

        public WebSocketsService(GameBootstrap gameBootstrap)
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
                
                Debug.Log(message);

                switch (messageType.type)
                {
                    case "game-created":
                    {
                        var gameCreated = message.ToDeserialized<GameGet>();

                        _gameID = gameCreated.payload.id;
                        
                        OnGameInitialized?.Invoke();
                        
                        break;
                    }
                    case "game-ended":
                    {
                        var gameEnded = message.ToDeserialized<GameEndGet>();
                        
                        gameBootstrap.StateMachine.SwitchState<MainMenuState>();
                        
                        Debug.Log(message);

                        break;
                    }
                    case "error":
                    {
                        Debug.Log("Введены неверные данные");
                        break;
                    }
                }
            });

            _wsConnection.Connect();
        }

        public void PostNewGame()
        {
            var newGame = new GameGet
            {
                type = "create-game"
            };
            
            _wsConnection.SendMessage(newGame.ToJson());
        }

        public void PostSnakeCollectedApple(long appleCount, long snakeLenght)
        {
            var gamePost = new GameStatsPost()
            {
                type = "collect-apple",
                payload = new GameStatsPostPayLoad()
                {
                    appleCount = appleCount,
                    snakeLength = snakeLenght,
                    game_id = _gameID
                }
            };
            
            _wsConnection.SendMessage(gamePost.ToJson());
        }

        public void PostEndGame()
        {
            var gameEndPost = new GameEndPost()
            {
                type = "end-game",
                payload = new GameEndPostPayLoad()
                {
                    game_id = _gameID
                }
            };

            _wsConnection.SendMessage(gameEndPost.ToJson());
        }
    }
}