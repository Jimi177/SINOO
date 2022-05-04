using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocomotion : MonoBehaviour
{
    CameraManager cameraManager;
    PlayerManager playerManager;
    PlayerAnimatorManager playerAnimatorManager;

    InputManager inputManager;
    Transform cameraObject;
    Vector3 moveDirection;

    public Rigidbody playerRigidBody;

    [Header("Movement Speeds")]
    public float walkSpeed;
    public float sprintSpeed;
    public float berserkerSpeed;
    public float rotationSpeed;

    [Header("Falling")]
    public float inAirTimer;
    public float leapingVelocity;
    public float fallingVelocity;
    public LayerMask groundLayer;
    public float rayCastHeightOffSet = 0.5f;

    [Header("Movement Flags")]
    public bool isSprinting;
    public bool isGrounded;
    public bool isJumping;
    public bool isBerserkerActive;

    public bool isArmed;
    public float weaponMovmentSpeed;

    [Header("Jump Speeds")]
    public float jumpHeight = 3;
    public float gravityIntensity = -15;

    private void Awake()
    {
        cameraManager = FindObjectOfType<CameraManager>();

        playerManager = GetComponent<PlayerManager>();
        playerAnimatorManager = GetComponentInChildren<PlayerAnimatorManager>();

        inputManager = GetComponent<InputManager>();
        playerRigidBody = GetComponent<Rigidbody>();
        cameraObject = Camera.main.transform;
    }

    public void HandleAllMovement()
    {
        HandleFallingAndLanding();

        if (playerManager.isInteracting && playerManager.isBlocking== false)
        {
            return;
        }

        if (isJumping)
        {
            return;
        }

        HandleMovement();
        HandleRotation();
    }

    private void HandleMovement()
    {
        moveDirection = cameraObject.forward * inputManager.verticalInput;
        moveDirection += cameraObject.right * inputManager.horizontalInput;
        moveDirection.Normalize(); //Keeps it 0-1
        moveDirection.y = 0;

        
        if (isSprinting)
        {
            if (isBerserkerActive)
            {
                moveDirection *= berserkerSpeed;
            }
            else
            {
                moveDirection *= sprintSpeed;
            }
        }
        else if(isArmed)
        {
            moveDirection *= weaponMovmentSpeed;
        }
        else
        {
            moveDirection *= walkSpeed;
        }


        Vector3 movementVelocity = moveDirection;
        playerRigidBody.velocity = movementVelocity;
    }

    private void HandleRotation()
    {
        if (inputManager.lockOnActive)
        {
            if(isSprinting)
            {
                Vector3 targetDirection = Vector3.zero;
                targetDirection = cameraManager.cameraTransform.forward * inputManager.verticalInput;
                targetDirection += cameraManager.cameraTransform.right * inputManager.horizontalInput;
                targetDirection.Normalize();
                targetDirection.y = 0;

                if (targetDirection == Vector3.zero)
                {
                    targetDirection = transform.forward;
                }

                Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
                Quaternion playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

                transform.rotation = playerRotation;
            }
            else
            {
                Vector3 rotationDirection = moveDirection;
                rotationDirection = cameraManager.currentLockOnTransform.transform.position - transform.position;
                rotationDirection.y = 0;
                rotationDirection.Normalize();
                Quaternion tr = Quaternion.LookRotation(rotationDirection);
                Quaternion targetRotation = Quaternion.Slerp(transform.rotation, tr, rotationSpeed * Time.deltaTime);
                transform.rotation = targetRotation;
            }
        }
        else
        {
            Vector3 targetDirection = Vector3.zero;
            targetDirection = cameraObject.forward * inputManager.verticalInput;
            targetDirection += cameraObject.right * inputManager.horizontalInput;
            targetDirection.Normalize();
            targetDirection.y = 0;

            if (targetDirection == Vector3.zero)
            {
                targetDirection = transform.forward;
            }

            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
            Quaternion playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            transform.rotation = playerRotation;
        }
        
        
    }

    private void HandleFallingAndLanding()
    {
        RaycastHit hit;
        Vector3 rayCastOrigin = transform.position;
        Vector3 targetPosition;
        rayCastOrigin.y += rayCastHeightOffSet;
        targetPosition = transform.position;

        if (!isGrounded && !isJumping)
        {
            if (!playerManager.isInteracting)
            {
                playerAnimatorManager.PlayTargetAnimation("Falling", true);
            }

            inAirTimer += Time.deltaTime;
            playerRigidBody.AddForce(transform.forward * leapingVelocity);
            playerRigidBody.AddForce(Vector3.down * fallingVelocity * inAirTimer);
        }

        if (Physics.SphereCast(rayCastOrigin, 0.2f, Vector3.down, out hit, groundLayer))
        {
            if (!isGrounded & !playerManager.isInteracting)
            {
                playerAnimatorManager.PlayTargetAnimation("Land", true);
            }

            Vector3 rayCastHitPoint = hit.point;
            targetPosition.y = rayCastHitPoint.y;
            inAirTimer = 0;
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

        if (isGrounded && !isJumping)
        {
            if (playerManager.isInteracting || inputManager.moveAmount > 0)
            {
                transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime / 0.1f);
            }
            else
            {
                transform.position = targetPosition;
            }
        }
    }

    public void HandleJumping()
    {
        if (isGrounded)
        {
            playerAnimatorManager.animator.SetBool("isJumping", true);
            playerAnimatorManager.PlayTargetAnimation("Jump", false);

            float jumpingVelocity = Mathf.Sqrt(-2 * gravityIntensity * jumpHeight);
            Vector3 playerVelocity = moveDirection;
            playerVelocity.y = jumpingVelocity;
            playerRigidBody.velocity = playerVelocity;
        }
    }

    public void HandleRolling()
    {
        if (playerManager.isInteracting)
        {
            return;
        }

        if (isGrounded && inputManager.moveAmount > 0.5)
        {
            moveDirection = cameraObject.forward * inputManager.verticalInput;
            moveDirection += cameraObject.right * inputManager.horizontalInput;
            moveDirection.Normalize();
            moveDirection.y = 0;

            playerAnimatorManager.PlayTargetAnimation("Roll", true);

            Quaternion rollRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = rollRotation;

        }
    }
}
