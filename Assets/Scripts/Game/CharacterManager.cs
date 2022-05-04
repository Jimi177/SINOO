using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public Collider characterCollider;
    public Transform lockOnTransform;
    public bool isInvurnelable;
    public bool isPlayer;

    [Header("Combat Colliders")]
    public bool isPossibleToBackstab;
    public CriticalDamageCollider backStabCollider;
    public CriticalDamageCollider riposteCollider;

    [Header("Combat Flags")]
    public bool canBeRiposted;
    public bool canBeparried;
    public bool canBeBerserkerCritical;
    public bool isParrying;
    public bool isBlocking;
    public bool isKicking;
    public bool isDead;

    public int pendingCriticalDamage;
}
