// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/Player/PlayerControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControls"",
    ""maps"": [
        {
            ""name"": ""PlayerMovement"",
            ""id"": ""af72726c-3e87-4c5b-a22d-5ca16c67b39b"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""PassThrough"",
                    ""id"": ""12544aa6-2dbc-4911-9073-ee093f73dc72"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Camera"",
                    ""type"": ""PassThrough"",
                    ""id"": ""8b97b111-ba62-403b-904b-929ec43dbc59"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""c961f32d-ab00-49f1-9f66-c5cee4d42296"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""7632f1b1-35df-40df-8647-bc46a01f3b0b"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""5927c8ae-def2-4712-973c-00e789853624"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""5a4898f2-4b0c-4ab5-9f13-d44e98a0b97c"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""ef427adf-9931-4558-98d2-06079ad2f974"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""5bf80101-66cf-4033-a654-8dd829283de4"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Camera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""PlayerActions"",
            ""id"": ""d5b0e2cd-077b-454c-8d4e-28dc62286bbf"",
            ""actions"": [
                {
                    ""name"": ""Shift"",
                    ""type"": ""Button"",
                    ""id"": ""28aa705d-f876-4de8-a6a2-e69141e15829"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Space"",
                    ""type"": ""Button"",
                    ""id"": ""f57fafc1-df08-4ba1-a9ba-ac0b6615b1b6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Roll"",
                    ""type"": ""Button"",
                    ""id"": ""b17d51cd-0432-4d8c-8fb0-0aad5aa777bf"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""LeftHandSwitch"",
                    ""type"": ""Button"",
                    ""id"": ""09e45437-9d10-4be6-a6be-42546a7eb0b6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""RightHandSwitch"",
                    ""type"": ""Button"",
                    ""id"": ""304d1e42-1a8a-4722-9170-b4ecf253d3f3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Light_Attack"",
                    ""type"": ""Button"",
                    ""id"": ""db73a7b7-4189-4c81-9085-92a732dfc1a5"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Heavy_Attack"",
                    ""type"": ""Button"",
                    ""id"": ""e694a450-167a-4fe6-82eb-d701beaeaed0"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""CriticalAttack"",
                    ""type"": ""Button"",
                    ""id"": ""b9fe3231-f356-4ee3-bea8-b2cbadeb60be"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Parry"",
                    ""type"": ""Button"",
                    ""id"": ""c5475651-9d21-4119-aa42-0db2502a9957"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Block"",
                    ""type"": ""Button"",
                    ""id"": ""f412f81a-8676-4413-94a7-c7ed21cdcaa3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Berserker"",
                    ""type"": ""Button"",
                    ""id"": ""c9485d34-a27e-4ac4-8ebf-9c828f3332d8"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""e5a084b8-56ae-43cf-ad84-3ebe253d4b9c"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Shift"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d1b49e35-fcee-41b5-aeed-38073901b955"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Space"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""140efc26-5665-4cac-9b39-b8ccb6ac043f"",
                    ""path"": ""<Keyboard>/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Roll"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""21964d3f-733f-4510-89ce-871be736cb4f"",
                    ""path"": ""<Keyboard>/f1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LeftHandSwitch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""33f8019c-56c5-4b7f-adf4-0e521eadbdec"",
                    ""path"": ""<Keyboard>/f2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RightHandSwitch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7a211cfd-30d5-40e5-96ac-1b770e4c32af"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Light_Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""132096bd-1aa0-49a3-8ce1-c48115e3a24a"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Heavy_Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a2bb2647-80bb-4b5c-8014-e75602bb5736"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CriticalAttack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9c5c2fbf-9ac8-4bd4-beeb-032b02ae6152"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Parry"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0f2300ce-6f36-4ec2-b157-9250538aa5a2"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Block"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7170acef-88e8-48e9-8bb3-4ddd07b9a45f"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Berserker"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""PlayerCamera"",
            ""id"": ""c69d531f-da85-468a-a33f-36cc803ce49c"",
            ""actions"": [
                {
                    ""name"": ""LockOn"",
                    ""type"": ""Button"",
                    ""id"": ""f3b7e73c-adfa-4b05-9bd6-65dbc78008c8"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""SwitchLeftLockOn"",
                    ""type"": ""Button"",
                    ""id"": ""7d196da9-1a0d-4e1e-a580-7346f23eac29"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""SwitchRightLockOn"",
                    ""type"": ""Button"",
                    ""id"": ""bfcce37e-57f5-4439-93c0-af4c9a1e9684"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""373134b9-ec62-4ae4-ae4b-f619893be1c2"",
                    ""path"": ""<Keyboard>/leftCtrl"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LockOn"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6057015e-8d2e-4e1e-8b32-eb63525f506b"",
                    ""path"": ""<Keyboard>/1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SwitchLeftLockOn"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""192d8bcf-cfc2-40f0-985a-be9f704320eb"",
                    ""path"": ""<Keyboard>/2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SwitchRightLockOn"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // PlayerMovement
        m_PlayerMovement = asset.FindActionMap("PlayerMovement", throwIfNotFound: true);
        m_PlayerMovement_Movement = m_PlayerMovement.FindAction("Movement", throwIfNotFound: true);
        m_PlayerMovement_Camera = m_PlayerMovement.FindAction("Camera", throwIfNotFound: true);
        // PlayerActions
        m_PlayerActions = asset.FindActionMap("PlayerActions", throwIfNotFound: true);
        m_PlayerActions_Shift = m_PlayerActions.FindAction("Shift", throwIfNotFound: true);
        m_PlayerActions_Space = m_PlayerActions.FindAction("Space", throwIfNotFound: true);
        m_PlayerActions_Roll = m_PlayerActions.FindAction("Roll", throwIfNotFound: true);
        m_PlayerActions_LeftHandSwitch = m_PlayerActions.FindAction("LeftHandSwitch", throwIfNotFound: true);
        m_PlayerActions_RightHandSwitch = m_PlayerActions.FindAction("RightHandSwitch", throwIfNotFound: true);
        m_PlayerActions_Light_Attack = m_PlayerActions.FindAction("Light_Attack", throwIfNotFound: true);
        m_PlayerActions_Heavy_Attack = m_PlayerActions.FindAction("Heavy_Attack", throwIfNotFound: true);
        m_PlayerActions_CriticalAttack = m_PlayerActions.FindAction("CriticalAttack", throwIfNotFound: true);
        m_PlayerActions_Parry = m_PlayerActions.FindAction("Parry", throwIfNotFound: true);
        m_PlayerActions_Block = m_PlayerActions.FindAction("Block", throwIfNotFound: true);
        m_PlayerActions_Berserker = m_PlayerActions.FindAction("Berserker", throwIfNotFound: true);
        // PlayerCamera
        m_PlayerCamera = asset.FindActionMap("PlayerCamera", throwIfNotFound: true);
        m_PlayerCamera_LockOn = m_PlayerCamera.FindAction("LockOn", throwIfNotFound: true);
        m_PlayerCamera_SwitchLeftLockOn = m_PlayerCamera.FindAction("SwitchLeftLockOn", throwIfNotFound: true);
        m_PlayerCamera_SwitchRightLockOn = m_PlayerCamera.FindAction("SwitchRightLockOn", throwIfNotFound: true);
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

    // PlayerMovement
    private readonly InputActionMap m_PlayerMovement;
    private IPlayerMovementActions m_PlayerMovementActionsCallbackInterface;
    private readonly InputAction m_PlayerMovement_Movement;
    private readonly InputAction m_PlayerMovement_Camera;
    public struct PlayerMovementActions
    {
        private @PlayerControls m_Wrapper;
        public PlayerMovementActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_PlayerMovement_Movement;
        public InputAction @Camera => m_Wrapper.m_PlayerMovement_Camera;
        public InputActionMap Get() { return m_Wrapper.m_PlayerMovement; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerMovementActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerMovementActions instance)
        {
            if (m_Wrapper.m_PlayerMovementActionsCallbackInterface != null)
            {
                @Movement.started -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnMovement;
                @Camera.started -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnCamera;
                @Camera.performed -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnCamera;
                @Camera.canceled -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnCamera;
            }
            m_Wrapper.m_PlayerMovementActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @Camera.started += instance.OnCamera;
                @Camera.performed += instance.OnCamera;
                @Camera.canceled += instance.OnCamera;
            }
        }
    }
    public PlayerMovementActions @PlayerMovement => new PlayerMovementActions(this);

    // PlayerActions
    private readonly InputActionMap m_PlayerActions;
    private IPlayerActionsActions m_PlayerActionsActionsCallbackInterface;
    private readonly InputAction m_PlayerActions_Shift;
    private readonly InputAction m_PlayerActions_Space;
    private readonly InputAction m_PlayerActions_Roll;
    private readonly InputAction m_PlayerActions_LeftHandSwitch;
    private readonly InputAction m_PlayerActions_RightHandSwitch;
    private readonly InputAction m_PlayerActions_Light_Attack;
    private readonly InputAction m_PlayerActions_Heavy_Attack;
    private readonly InputAction m_PlayerActions_CriticalAttack;
    private readonly InputAction m_PlayerActions_Parry;
    private readonly InputAction m_PlayerActions_Block;
    private readonly InputAction m_PlayerActions_Berserker;
    public struct PlayerActionsActions
    {
        private @PlayerControls m_Wrapper;
        public PlayerActionsActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Shift => m_Wrapper.m_PlayerActions_Shift;
        public InputAction @Space => m_Wrapper.m_PlayerActions_Space;
        public InputAction @Roll => m_Wrapper.m_PlayerActions_Roll;
        public InputAction @LeftHandSwitch => m_Wrapper.m_PlayerActions_LeftHandSwitch;
        public InputAction @RightHandSwitch => m_Wrapper.m_PlayerActions_RightHandSwitch;
        public InputAction @Light_Attack => m_Wrapper.m_PlayerActions_Light_Attack;
        public InputAction @Heavy_Attack => m_Wrapper.m_PlayerActions_Heavy_Attack;
        public InputAction @CriticalAttack => m_Wrapper.m_PlayerActions_CriticalAttack;
        public InputAction @Parry => m_Wrapper.m_PlayerActions_Parry;
        public InputAction @Block => m_Wrapper.m_PlayerActions_Block;
        public InputAction @Berserker => m_Wrapper.m_PlayerActions_Berserker;
        public InputActionMap Get() { return m_Wrapper.m_PlayerActions; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActionsActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActionsActions instance)
        {
            if (m_Wrapper.m_PlayerActionsActionsCallbackInterface != null)
            {
                @Shift.started -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnShift;
                @Shift.performed -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnShift;
                @Shift.canceled -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnShift;
                @Space.started -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnSpace;
                @Space.performed -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnSpace;
                @Space.canceled -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnSpace;
                @Roll.started -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnRoll;
                @Roll.performed -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnRoll;
                @Roll.canceled -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnRoll;
                @LeftHandSwitch.started -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnLeftHandSwitch;
                @LeftHandSwitch.performed -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnLeftHandSwitch;
                @LeftHandSwitch.canceled -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnLeftHandSwitch;
                @RightHandSwitch.started -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnRightHandSwitch;
                @RightHandSwitch.performed -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnRightHandSwitch;
                @RightHandSwitch.canceled -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnRightHandSwitch;
                @Light_Attack.started -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnLight_Attack;
                @Light_Attack.performed -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnLight_Attack;
                @Light_Attack.canceled -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnLight_Attack;
                @Heavy_Attack.started -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnHeavy_Attack;
                @Heavy_Attack.performed -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnHeavy_Attack;
                @Heavy_Attack.canceled -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnHeavy_Attack;
                @CriticalAttack.started -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnCriticalAttack;
                @CriticalAttack.performed -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnCriticalAttack;
                @CriticalAttack.canceled -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnCriticalAttack;
                @Parry.started -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnParry;
                @Parry.performed -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnParry;
                @Parry.canceled -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnParry;
                @Block.started -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnBlock;
                @Block.performed -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnBlock;
                @Block.canceled -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnBlock;
                @Berserker.started -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnBerserker;
                @Berserker.performed -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnBerserker;
                @Berserker.canceled -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnBerserker;
            }
            m_Wrapper.m_PlayerActionsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Shift.started += instance.OnShift;
                @Shift.performed += instance.OnShift;
                @Shift.canceled += instance.OnShift;
                @Space.started += instance.OnSpace;
                @Space.performed += instance.OnSpace;
                @Space.canceled += instance.OnSpace;
                @Roll.started += instance.OnRoll;
                @Roll.performed += instance.OnRoll;
                @Roll.canceled += instance.OnRoll;
                @LeftHandSwitch.started += instance.OnLeftHandSwitch;
                @LeftHandSwitch.performed += instance.OnLeftHandSwitch;
                @LeftHandSwitch.canceled += instance.OnLeftHandSwitch;
                @RightHandSwitch.started += instance.OnRightHandSwitch;
                @RightHandSwitch.performed += instance.OnRightHandSwitch;
                @RightHandSwitch.canceled += instance.OnRightHandSwitch;
                @Light_Attack.started += instance.OnLight_Attack;
                @Light_Attack.performed += instance.OnLight_Attack;
                @Light_Attack.canceled += instance.OnLight_Attack;
                @Heavy_Attack.started += instance.OnHeavy_Attack;
                @Heavy_Attack.performed += instance.OnHeavy_Attack;
                @Heavy_Attack.canceled += instance.OnHeavy_Attack;
                @CriticalAttack.started += instance.OnCriticalAttack;
                @CriticalAttack.performed += instance.OnCriticalAttack;
                @CriticalAttack.canceled += instance.OnCriticalAttack;
                @Parry.started += instance.OnParry;
                @Parry.performed += instance.OnParry;
                @Parry.canceled += instance.OnParry;
                @Block.started += instance.OnBlock;
                @Block.performed += instance.OnBlock;
                @Block.canceled += instance.OnBlock;
                @Berserker.started += instance.OnBerserker;
                @Berserker.performed += instance.OnBerserker;
                @Berserker.canceled += instance.OnBerserker;
            }
        }
    }
    public PlayerActionsActions @PlayerActions => new PlayerActionsActions(this);

    // PlayerCamera
    private readonly InputActionMap m_PlayerCamera;
    private IPlayerCameraActions m_PlayerCameraActionsCallbackInterface;
    private readonly InputAction m_PlayerCamera_LockOn;
    private readonly InputAction m_PlayerCamera_SwitchLeftLockOn;
    private readonly InputAction m_PlayerCamera_SwitchRightLockOn;
    public struct PlayerCameraActions
    {
        private @PlayerControls m_Wrapper;
        public PlayerCameraActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @LockOn => m_Wrapper.m_PlayerCamera_LockOn;
        public InputAction @SwitchLeftLockOn => m_Wrapper.m_PlayerCamera_SwitchLeftLockOn;
        public InputAction @SwitchRightLockOn => m_Wrapper.m_PlayerCamera_SwitchRightLockOn;
        public InputActionMap Get() { return m_Wrapper.m_PlayerCamera; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerCameraActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerCameraActions instance)
        {
            if (m_Wrapper.m_PlayerCameraActionsCallbackInterface != null)
            {
                @LockOn.started -= m_Wrapper.m_PlayerCameraActionsCallbackInterface.OnLockOn;
                @LockOn.performed -= m_Wrapper.m_PlayerCameraActionsCallbackInterface.OnLockOn;
                @LockOn.canceled -= m_Wrapper.m_PlayerCameraActionsCallbackInterface.OnLockOn;
                @SwitchLeftLockOn.started -= m_Wrapper.m_PlayerCameraActionsCallbackInterface.OnSwitchLeftLockOn;
                @SwitchLeftLockOn.performed -= m_Wrapper.m_PlayerCameraActionsCallbackInterface.OnSwitchLeftLockOn;
                @SwitchLeftLockOn.canceled -= m_Wrapper.m_PlayerCameraActionsCallbackInterface.OnSwitchLeftLockOn;
                @SwitchRightLockOn.started -= m_Wrapper.m_PlayerCameraActionsCallbackInterface.OnSwitchRightLockOn;
                @SwitchRightLockOn.performed -= m_Wrapper.m_PlayerCameraActionsCallbackInterface.OnSwitchRightLockOn;
                @SwitchRightLockOn.canceled -= m_Wrapper.m_PlayerCameraActionsCallbackInterface.OnSwitchRightLockOn;
            }
            m_Wrapper.m_PlayerCameraActionsCallbackInterface = instance;
            if (instance != null)
            {
                @LockOn.started += instance.OnLockOn;
                @LockOn.performed += instance.OnLockOn;
                @LockOn.canceled += instance.OnLockOn;
                @SwitchLeftLockOn.started += instance.OnSwitchLeftLockOn;
                @SwitchLeftLockOn.performed += instance.OnSwitchLeftLockOn;
                @SwitchLeftLockOn.canceled += instance.OnSwitchLeftLockOn;
                @SwitchRightLockOn.started += instance.OnSwitchRightLockOn;
                @SwitchRightLockOn.performed += instance.OnSwitchRightLockOn;
                @SwitchRightLockOn.canceled += instance.OnSwitchRightLockOn;
            }
        }
    }
    public PlayerCameraActions @PlayerCamera => new PlayerCameraActions(this);
    public interface IPlayerMovementActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnCamera(InputAction.CallbackContext context);
    }
    public interface IPlayerActionsActions
    {
        void OnShift(InputAction.CallbackContext context);
        void OnSpace(InputAction.CallbackContext context);
        void OnRoll(InputAction.CallbackContext context);
        void OnLeftHandSwitch(InputAction.CallbackContext context);
        void OnRightHandSwitch(InputAction.CallbackContext context);
        void OnLight_Attack(InputAction.CallbackContext context);
        void OnHeavy_Attack(InputAction.CallbackContext context);
        void OnCriticalAttack(InputAction.CallbackContext context);
        void OnParry(InputAction.CallbackContext context);
        void OnBlock(InputAction.CallbackContext context);
        void OnBerserker(InputAction.CallbackContext context);
    }
    public interface IPlayerCameraActions
    {
        void OnLockOn(InputAction.CallbackContext context);
        void OnSwitchLeftLockOn(InputAction.CallbackContext context);
        void OnSwitchRightLockOn(InputAction.CallbackContext context);
    }
}
