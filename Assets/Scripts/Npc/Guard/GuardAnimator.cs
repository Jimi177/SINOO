using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardAnimator : AnimatorManager
{
    GuardManager guardManager;

    private void Awake()
    {
        guardManager = GetComponentInParent<GuardManager>();
    }

    private void OnAnimatorMove()
    {
        float delta = Time.deltaTime;
        guardManager.guardRB.drag = 0;
        Vector3 deltaPosition = animator.deltaPosition;
        deltaPosition.y = 0;
        Vector3 velocity = deltaPosition / delta;
        guardManager.guardRB.velocity = velocity;
    }
}
