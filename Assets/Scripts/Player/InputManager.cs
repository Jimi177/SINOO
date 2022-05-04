using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    Animator animator;
    PlayerControls playerControls;
    PlayerAttacker playerAttacker;
    PlayerAnimatorManager playerAnimatorManager;
    PlayerLocomotion playerLocomotion;
    PlayerStats playerStats;
    WeaponSlotManager weaponSlotManager;
    PlayerInventory playerInventory;
    CameraManager cameraManager;
    PlayerManager playerManager;
    

    public Vector2 movementInput;
    public Vector2 cameraInput;

    public float moveAmount;

    public float verticalInput;
    public float horizontalInput;
    public float cameraXInput;
    public float cameraYInput;

    public bool shiftInput;//sprint
    public bool spaceInput;//jump
    public bool xInput; //roll
    public bool ctrlInput; //lockOn


    public bool F1_Input;
    public bool F2_Input;
    public bool key_OneInput;
    public bool key_TwoInput;
    public bool mouse_01Input;
    public bool mouse_02Input;
    public bool critical_Attack_Input; //mouse2 HOLD
    public bool parry_Input; //E
    public bool block_Input; //R
    public bool special_Input;//F
    public bool canDoComboActive;


    public bool isRightHandActive;
    public bool isLeftHandActive;
    public bool lockOnActive;

    public Transform criticalAttackRayCastStartPoint;

    public void Awake()
    {
        playerAnimatorManager = GetComponentInChildren<PlayerAnimatorManager>();
        weaponSlotManager = GetComponentInChildren<WeaponSlotManager>();
        playerLocomotion = GetComponent<PlayerLocomotion>();
        playerStats = GetComponent<PlayerStats>();
        playerInventory = GetComponentInChildren<PlayerInventory>();
        animator = GetComponentInChildren<Animator>();
        cameraManager = FindObjectOfType<CameraManager>();
        playerAttacker = GetComponent<PlayerAttacker>();
        playerManager = GetComponent<PlayerManager>();
    }

    public void Update()
    {
        
    }

    public void HandleAllInputs()
    {
        HandleMovementInput();
        HandleSprintInput();
        HandleJumpInput();
        HandleRollInput();
        HandleLeftHandInput();
        HandleRightHandInput();
        HandleLockOn();
        HandleCombatInput();
        HandleCriticalAttackInput();
        HandleBuffsInput();
    }

    private void OnEnable()
    {
        if(playerControls == null)
        {
            playerControls = new PlayerControls();

            //Movement
            playerControls.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();
            playerControls.PlayerMovement.Camera.performed += i => cameraInput = i.ReadValue<Vector2>();
            //Actions
            playerControls.PlayerActions.Shift.performed += i => shiftInput = true;
            playerControls.PlayerActions.Shift.canceled += i => shiftInput = false;
            playerControls.PlayerActions.Space.performed += i => spaceInput = true;
            playerControls.PlayerActions.Roll.performed += i => xInput = true;
            playerControls.PlayerActions.LeftHandSwitch.performed += i => F1_Input = true;
            playerControls.PlayerActions.RightHandSwitch.performed += i => F2_Input = true;
            playerControls.PlayerActions.Light_Attack.performed += i => mouse_01Input = true;
            playerControls.PlayerActions.Heavy_Attack.performed += i => mouse_02Input = true;
            playerControls.PlayerActions.CriticalAttack.performed += i => critical_Attack_Input = true;
            playerControls.PlayerActions.Parry.performed += i => parry_Input = true;
            playerControls.PlayerActions.Block.performed += i => block_Input = true;
            playerControls.PlayerActions.Block.canceled += i => block_Input = false;
            playerControls.PlayerActions.Berserker.performed += i => special_Input = true;
            //Camera
            playerControls.PlayerCamera.LockOn.performed += i => ctrlInput = true;
            playerControls.PlayerCamera.SwitchLeftLockOn.performed += i => key_OneInput = true;
            playerControls.PlayerCamera.SwitchRightLockOn.performed += i => key_TwoInput = true;
        }

        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    private void HandleMovementInput()
    {
        verticalInput = movementInput.y;
        horizontalInput = movementInput.x;

        cameraXInput = cameraInput.y;
        cameraYInput = cameraInput.x;

        moveAmount = Mathf.Clamp01(Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput));

        if(lockOnActive && playerLocomotion.isSprinting == false)
        {
            playerAnimatorManager.UpdateAnimatorValutes(horizontalInput, verticalInput, playerLocomotion.isSprinting);
        }
        else
        {
            playerAnimatorManager.UpdateAnimatorValutes(0, moveAmount, playerLocomotion.isSprinting);
        }
        
    }

    private void HandleSprintInput()
    {
        if(shiftInput)
        {
            playerLocomotion.isSprinting = true;
        }
        else
        {
            playerLocomotion.isSprinting = false;
        }
    }

    private void HandleJumpInput()
    {
        if(spaceInput)
        {
            spaceInput = false;
            playerLocomotion.HandleJumping();
        }
    }

    private void HandleRollInput()
    {
        if(xInput)
        {
            xInput = false;
            playerLocomotion.HandleRolling();
        }
    }

    private void HandleLeftHandInput()
    {
        if(F1_Input)
        {
            F1_Input = false;

            if (playerInventory.leftWeapon != null)
            {
                isLeftHandActive = !isLeftHandActive;
                weaponSlotManager.HideOrWithdrawWeapon(playerInventory.leftWeapon, true, playerInventory.leftWeapon.movementSpeed);
                
            }
        }
    }

    private void HandleRightHandInput()
    {
        if(F2_Input)
        {
            F2_Input = false;

            if (playerInventory.rightWeapon != null)
            {
                isRightHandActive = !isRightHandActive;
                weaponSlotManager.HideOrWithdrawWeapon(playerInventory.rightWeapon, false, playerInventory.rightWeapon.movementSpeed);
            } 
        }
    }

    private void HandleLockOn()
    {
        if(ctrlInput && lockOnActive == false)
        {
            ctrlInput = false;
            cameraManager.HandleLockOn();
            if(cameraManager.nearestLockOnTransform != null)
            {
                cameraManager.currentLockOnTransform = cameraManager.nearestLockOnTransform;
                lockOnActive = true;
            }
        }
        else if(ctrlInput && lockOnActive)
        {
            ctrlInput = false;
            lockOnActive = false;
            cameraManager.ClearLockOn();
        }

        if (lockOnActive && key_OneInput)
        {
            key_OneInput = false;
            cameraManager.HandleLockOn();
            if(cameraManager.leftLockOnTarget != null)
            {
                cameraManager.currentLockOnTransform = cameraManager.leftLockOnTarget;
            }
        }

        if (lockOnActive && key_TwoInput)
        {
            key_TwoInput = false;
            cameraManager.HandleLockOn();
            if (cameraManager.rightLockOnTarget != null)
            {
                cameraManager.currentLockOnTransform = cameraManager.rightLockOnTarget;
            }
        }
        //cameraManager.LockCameraPivot();
    }

    private void HandleCombatInput()
    {
        if(isRightHandActive)
        {
            if (mouse_01Input)
            {
                mouse_01Input = false;

                if (playerManager.canDoCombo)
                {
                    canDoComboActive = true;
                    playerAttacker.HandleWeaponCombo(playerInventory.rightWeapon);
                    canDoComboActive = false;
                }
                else
                {
                    playerAttacker.HandleLightAttack(playerInventory.rightWeapon);
                }
            }

            if (mouse_02Input)
            {
                mouse_02Input = false;

                if (playerManager.canDoCombo)
                {
                    canDoComboActive = true;
                    playerAttacker.HandleWeaponCombo(playerInventory.rightWeapon);
                    canDoComboActive = false;
                }
                else
                {
                    playerAttacker.HandleHeavytAttack(playerInventory.rightWeapon);
                }
            }
        }

        if(isLeftHandActive)
        {
            if(parry_Input)
            {
                parry_Input = false;

                playerAttacker.HandleSpecial();
            }
        }

        if(isLeftHandActive)
        {
            if(block_Input)
            {
                playerAttacker.HandleBlock();
            }
            else
            {
                playerManager.isBlocking = false;
            }
        }
    }

    private void HandleBuffsInput()
    {
        if (special_Input)
        {
            special_Input = false;

            if (playerStats.isBerserker)
            {
                playerStats.PlayerBerserker();
            }
            else if(playerStats.isTemplar)
            {
                playerStats.FireSword();
            }
            else if(playerStats.isKnight)
            {
                playerStats.HolyAttack();
            }
        }
    }

    private void HandleCriticalAttackInput()
    {
        if(isRightHandActive)
        {
            if (critical_Attack_Input)
            {
                critical_Attack_Input = false;
                playerAttacker.AttemptBackStabOrRipost();
            }
        }
        
    }
}
