using System.Threading.Tasks;
using NativeWebSocket;
using OOP.Data;
using UnityEngine;
using UnityEngine.Events;
using OOP.Infrastructure.ProjectStateMachine.States;

namespace OOP.Services.WebSocketsService
{
    public class WebSocketsService : IWebSocketsService
    {
        public event UnityAction OnGameInitialized;
        
        private const string URL = "wss://dev.match.qubixinfinity.io/snake";

        private WebSocket _wsConnection;

        private long _gameID;

        public WebSocketsService(GameBootstrap gameBootstrap)
        {
            InitializeWebSocket(gameBootstrap);
        }

        private async void InitializeWebSocket(GameBootstrap gameBootstrap)
        {
            _wsConnection = new WebSocket(URL);

            Tick();

            _wsConnection.OnOpen += () =>
            {
                Debug.Log("WS Connected!");
            };

            _wsConnection.OnError += (error) =>
            {
                Debug.Log("WS Error " + error);
            };

            _wsConnection.OnClose += (e) =>
            {
                Debug.Log("WS Disconnected!");
            };


            _wsConnection.OnMessage += (bytes) =>
            {
                var message = System.Text.Encoding.UTF8.GetString(bytes);

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
            };

            await _wsConnection.Connect();
        }

        public async void PostNewGame()
        {
            var newGame = new GameGet
            {
                type = "create-game"
            };
            
            await _wsConnection.SendText(newGame.ToJson());
        }

        public async void PostSnakeCollectedApple(long appleCount, long snakeLenght)
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
            
            await _wsConnection.SendText(gamePost.ToJson());
        }

        public async void PostEndGame()
        {
            var gameEndPost = new GameEndPost()
            {
                type = "end-game",
                payload = new GameEndPostPayLoad()
                {
                    game_id = _gameID
                }
            };

            await _wsConnection.SendText(gameEndPost.ToJson());
        }

        private async Task Tick()
        {
            while (true)
            {
                #if !UNITY_WEBGL || UNITY_EDITOR
                _wsConnection.DispatchMessageQueue();
                await Task.Yield();
                #endif
            }
        }

        public async void DestroyConnection()
        {
            await _wsConnection.Close();
        }
    }
}