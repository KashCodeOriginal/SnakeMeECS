//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.4.4
//     from Assets/OOP/Services/Input/PlayerInputActions.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @PlayerInputActions : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInputActions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInputActions"",
    ""maps"": [
        {
            ""name"": ""PlayerInputMap"",
            ""id"": ""d502a729-2f5e-489a-b4d3-6bc3ddfe0836"",
            ""actions"": [
                {
                    ""name"": ""WASDInput"",
                    ""type"": ""Value"",
                    ""id"": ""a7dcc8f1-1757-434a-804c-d04ce4405cd4"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WasdPlayerInput"",
                    ""id"": ""357ef359-1a2f-4d12-8ca6-eeaba1344326"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""WASDInput"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""634827fd-5c0e-4d66-83cf-423efc5bcfb2"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""WASDInput"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""090cf936-3ed9-4579-9b6a-4aaf576ee965"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""WASDInput"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""cf017158-13eb-4e70-aca2-a94630798ccd"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""WASDInput"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""e7c74be6-149d-4bd9-9331-693d6e041da0"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""WASDInput"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // PlayerInputMap
        m_PlayerInputMap = asset.FindActionMap("PlayerInputMap", throwIfNotFound: true);
        m_PlayerInputMap_WASDInput = m_PlayerInputMap.FindAction("WASDInput", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }
    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }
    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // PlayerInputMap
    private readonly InputActionMap m_PlayerInputMap;
    private IPlayerInputMapActions m_PlayerInputMapActionsCallbackInterface;
    private readonly InputAction m_PlayerInputMap_WASDInput;
    public struct PlayerInputMapActions
    {
        private @PlayerInputActions m_Wrapper;
        public PlayerInputMapActions(@PlayerInputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @WASDInput => m_Wrapper.m_PlayerInputMap_WASDInput;
        public InputActionMap Get() { return m_Wrapper.m_PlayerInputMap; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerInputMapActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerInputMapActions instance)
        {
            if (m_Wrapper.m_PlayerInputMapActionsCallbackInterface != null)
            {
                @WASDInput.started -= m_Wrapper.m_PlayerInputMapActionsCallbackInterface.OnWASDInput;
                @WASDInput.performed -= m_Wrapper.m_PlayerInputMapActionsCallbackInterface.OnWASDInput;
                @WASDInput.canceled -= m_Wrapper.m_PlayerInputMapActionsCallbackInterface.OnWASDInput;
            }
            m_Wrapper.m_PlayerInputMapActionsCallbackInterface = instance;
            if (instance != null)
            {
                @WASDInput.started += instance.OnWASDInput;
                @WASDInput.performed += instance.OnWASDInput;
                @WASDInput.canceled += instance.OnWASDInput;
            }
        }
    }
    public PlayerInputMapActions @PlayerInputMap => new PlayerInputMapActions(this);
    public interface IPlayerInputMapActions
    {
        void OnWASDInput(InputAction.CallbackContext context);
    }
}
