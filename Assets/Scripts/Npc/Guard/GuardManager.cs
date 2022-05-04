using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GuardManager : CharacterManager
{
    GuardLocomotion guardLocomotion;
    GuardAnimator guardAnimator;
    NpcWeaponSlotManager npcWeaponSlotManager;
    NpcInventory npcInventory;
    Animator animator;

    public Rigidbody guardRB;
    public NavMeshAgent guardNavmeshAnget;

    public GuardState currentState;
    public DollManager currentDollTarget;

    public float stoppingDistance = 3;
    public float rotationSpeed = 15;

    public bool isPerformingTraining;
    public bool isPerformingAction;
    public bool isTrainingFinish;
    public bool isInteracting;
    public bool isArmed;

    public float currentTrainingTime;

    private void Awake()
    {
        guardLocomotion = GetComponent<GuardLocomotion>();
        guardAnimator = GetComponentInChildren<GuardAnimator>();
        npcWeaponSlotManager = GetComponentInChildren<NpcWeaponSlotManager>();
        npcInventory = GetComponentInChildren<NpcInventory>();
        animator = GetComponentInChildren<Animator>();

        guardRB = GetComponent<Rigidbody>();
        guardNavmeshAnget = GetComponentInChildren<NavMeshAgent>();

        guardRB.isKinematic = false;
        guardNavmeshAnget.enabled = false;
    }

    private void Update()
    {
        isArmed = animator.GetBool("isArmed");
        isPerformingAction = animator.GetBool("isPerformingAction");
        HandleTrainingTime();
    }

    private void FixedUpdate()
    {
        HandleStateMachine();
    }

    private void HandleStateMachine()
    {
        if (currentState != null)
        {
            GuardState nextState = currentState.Tick(this, guardLocomotion, guardAnimator, npcWeaponSlotManager, npcInventory);

            if (nextState != null)
            {
                SwitchToNextState(nextState);
            }
        }
    }

    private void SwitchToNextState(GuardState state)
    {
        currentState = state;
    }

    private void HandleTrainingTime()
    {
        if (currentTrainingTime > 0)
        {
            currentTrainingTime -= Time.deltaTime;
        }

        if (isPerformingTraining)
        {
            if (currentTrainingTime <= 0)
            {
                isPerformingTraining = false;
                isTrainingFinish = true;
            }
        }
    }
}
