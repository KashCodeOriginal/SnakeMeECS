using System;
using OOP.Services.Locator;
using UnityEngine;
using UnityEngine.InputSystem;

namespace OOP.Services.Input
{
    [CreateAssetMenu(fileName = "PlayerInputActionReader", menuName = "Input/PlayerInputActionReader", order = 0)]
    public class PlayerInputActionReader : ScriptableObject, PlayerInputActions.IPlayerInputMapActions, IService
    {
        private PlayerInputActions _playerInputAction;

        public Action<Vector2> OnMovementInput;

        private void OnEnable()
        {
            if (_playerInputAction != null)
            {
                return;
            }

            _playerInputAction = new PlayerInputActions();
            
            _playerInputAction.PlayerInputMap.SetCallbacks(this);
            _playerInputAction.Enable();
        }

        public void OnWASDInput(InputAction.CallbackContext context)
        {
            OnMovementInput?.Invoke(context.ReadValue<Vector2>());
        }
    }
}