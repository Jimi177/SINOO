using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : CharacterManager
{
    CameraManager cameraManager;
    EnemyLocomotion enemyLocomotion;
    EnemyAnimator enemyAnimator;
    EnemyStats enemyStats;
    public Collider enemyCollider;
    Collider enemyBlocker;
    

    public Rigidbody enemyRB;
    public NavMeshAgent navmeshAgent;
    public State currentState;
    public CharacterStats currentTarget;

    public bool isPerformingAction;
    public bool isInteracting;
    public float viewableAngle;

    public bool canRotate;
    public bool fightModeActive;
    

    public int enemyClass;

    [Header("A.I Settings")]

    [Header("Pursue State")]

    public float stoppingDistance = 3;
    public float rotationSpeed = 45;

    [Header("Combat State")]
    public bool canDoCombo;
    public bool allowAIToPerformCombos;
    public float comboLikelyHood;



    public float currentRecoveryTime = 0;
    private void Awake()
    {
        cameraManager = FindObjectOfType<CameraManager>();

        enemyLocomotion = GetComponent<EnemyLocomotion>();
        enemyAnimator = GetComponentInChildren<EnemyAnimator>();
        enemyStats = GetComponent<EnemyStats>();
        enemyCollider = GetComponent<Collider>();
        enemyBlocker = GetComponentInChildren<Collider>();

        navmeshAgent = GetComponentInChildren<NavMeshAgent>();
        enemyRB = GetComponent<Rigidbody>();

        //navmeshAgent.enabled = false;
        enemyRB.isKinematic = false;
        enemyAnimator.animator.SetInteger("enemyClass", enemyClass);
    }

    private void Update()
    {
        HandleRecoveryTime();
        
        if (isDead)
        {
            return;
        }

        HandleStateMachine();

        isInteracting = enemyAnimator.animator.GetBool("isInteracting");
        isInvurnelable = enemyAnimator.animator.GetBool("isInvurnelable");
        canRotate = enemyAnimator.animator.GetBool("canRotate");
        canDoCombo = enemyAnimator.animator.GetBool("canDoCombo");
        isDead = enemyAnimator.animator.GetBool("isDead");
        
    }

    private void FixedUpdate()
    {
        navmeshAgent.transform.localPosition = Vector3.zero;
        navmeshAgent.transform.localRotation = Quaternion.identity;
    }

    private void HandleStateMachine()
    {
        

        if (currentState != null)
        {
            State nextState = currentState.Tick(this, enemyStats, enemyAnimator);

            if(nextState != null)
            {
                SwitchToNextState(nextState);
            }
        }
    }

    private void SwitchToNextState(State state)
    {
        currentState = state;
    }

    private void HandleRecoveryTime()
    {
        if (currentRecoveryTime > 0)
        {
            currentRecoveryTime -= Time.deltaTime;
        }

        if (isPerformingAction)
        {
            if (currentRecoveryTime <= 0)
            {
                isPerformingAction = false;
            }
        }
    }

    
}
