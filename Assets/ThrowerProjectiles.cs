using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowerProjectiles : MonoBehaviour
{
    Rigidbody thingRB;
    PlayerManager playerManager;

    [Header("Position")]
    public float positionX;
    public float positionY;
    public float positionZ;
    public float smooth;

    [Header("Force")]
    public float throwForce = 500;
    public float upForce = 1;

    [Header("Rotation")]
    public float rotationX;
    public float rotationY;
    public float rotationZ;

    private void Awake()
    {
        thingRB = GetComponent<Rigidbody>();
        playerManager = FindObjectOfType<PlayerManager>();
    }

    private void Start()
    {
        Vector3 dir = playerManager.lockOnTransform.transform.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = lookRotation.eulerAngles;
        transform.eulerAngles = rotation;


        thingRB.AddForce(transform.forward * throwForce + transform.up * upForce);
        thingRB.AddTorque(rotationX, rotationY, rotationZ);
    }
}
