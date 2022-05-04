using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorManager : MonoBehaviour
{
    public Animator animator;
   
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    public void PlayTargetAnimation(string targetAnimation, bool isInteracting)
    {
        animator.applyRootMotion = isInteracting;
        animator.SetBool("isInteracting", isInteracting);
        animator.CrossFade(targetAnimation, 0.2f);
    }

    public void PerfomringAttack()
    {
        animator.SetBool("isPerformingAttack", true);
    }

    public void FinishedAttack()
    {
        animator.SetBool("isPerformingAttack", false);
    }

    public void PerfomringSpecialAttack()
    {
        animator.SetBool("isPerformingSpecialAttack", true);
    }

    public void FinishedSpecialAttack()
    {
        animator.SetBool("isPerformingSpecialAttack", false);
    }

    //public void BlockIsActive()
    //{
    //    animator.SetBool("isBlocking", true);
    //}

    //public void BlockIsNotActive()
    //{
    //    animator.SetBool("isBlocking", false);
    //}

    public void EnableCombo()
    {
        animator.SetBool("canDoCombo", true);
    }

    public void DisableCombo()
    {
        animator.SetBool("canDoCombo", false);
    }

    public void EnableisInvurnelable()
    {
        animator.SetBool("isInvurnelable", true);
    }

    public void DisableisInvurnelable()
    {
        animator.SetBool("isInvurnelable", false);
    }

    public void isDead()
    {
        animator.SetBool("isDead", true);
    }

    public virtual void TakeCriticalDamageAnimationEvent()
    {

    }

    
}
