using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockCharacterCollision : MonoBehaviour
{
    
    [SerializeField] public CapsuleCollider characterCollider;
    [SerializeField] public CapsuleCollider characterBlockerCollider;
    void Start()
    {
        Physics.IgnoreCollision(characterCollider, characterBlockerCollider, true);
    }

    
}
