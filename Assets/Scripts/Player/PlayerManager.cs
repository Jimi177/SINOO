using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : CharacterManager
{
    InputManager inputManager;
    PlayerLocomotion playerLocomotion;
    PlayerAnimatorManager playerAnimatorManager;
    PlayerStats playerStats;
    Animator animator;
    CameraManager cameraManager;

    Collider playerCollider;


    //Manager
    public bool isInteracting;
    public bool isPerformingAttack;
    public bool canDoCombo;


    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
        playerLocomotion = GetComponent<PlayerLocomotion>();
        playerAnimatorManager = GetComponentInChildren<PlayerAnimatorManager>();
        playerStats = GetComponent<PlayerStats>();
        animator = GetComponentInChildren<Animator>();
        cameraManager = FindObjectOfType<CameraManager>();
        backStabCollider = GetComponentInChildren<CriticalDamageCollider>();

        playerCollider = GetComponent<Collider>();


    }

    private void Update()
    {
        inputManager.HandleAllInputs();
        playerStats.RegenerateStamina();

        //Checking
        isInteracting = animator.GetBool("isInteracting");
        isInvurnelable = animator.GetBool("isInvurnelable");
        playerLocomotion.isJumping = animator.GetBool("isJumping");
        animator.SetBool("isGrounded", playerLocomotion.isGrounded);
        canDoCombo = animator.GetBool("canDoCombo");
        isDead = animator.GetBool("isDead");
        animator.SetBool("isBlocking", isBlocking);
    }

    private void FixedUpdate()
    {
        HandleDeath();

        playerLocomotion.HandleAllMovement(); //Used here because of RB
    }

    private void LateUpdate()
    {
        cameraManager.HandleAllCameraMovement();
    }

    private void HandleDeath()
    {
        if (isDead)
        {
            playerCollider.enabled = false;
            lockOnTransform.gameObject.SetActive(false);
        }
    }
}
