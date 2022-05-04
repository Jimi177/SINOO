using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    InputManager inputManager;
    PlayerManager playerManager;

    public Transform targetTransform; //Object Camera follows
    public Transform cameraPivot;
    public Transform cameraTransform;
    private Vector3 cameraFollowVelocity = Vector3.zero;
    private Vector3 cameraVectorPostion;
    public LayerMask collisionLayers;
    public LayerMask enviromentLayer;
    private float defaultPosition;

    public float cameraFollowSpeed = 0.2f;
    public float cameraLookSpeed = 2;
    public float cameraPivotSpeed = 2;
    public float lookAngle;
    public float pivotAngle;
    public float minimumPivotAngle = -35;
    public float maximumPivotAngle = 35;
    public float maximumPivotXAngle = 40;
    public float miniumPivotXAngle = -25;
    public float cameraCollisionRadius = 0.3f;
    public float cameraCollisionOffSet = 0.2f;
    public float minimumCollisionOffSet = 0.2f;
    public float maximumDistanceFromTarget = 25;
    

    
    public CharacterManager nearestLockOnTransform;
    public CharacterManager currentLockOnTransform;
    public CharacterManager leftLockOnTarget;
    public CharacterManager rightLockOnTarget;
    List<CharacterManager> availableTargets = new List<CharacterManager>();
   


    public void Awake()
    {
        inputManager = FindObjectOfType<InputManager>();
        playerManager = FindObjectOfType<PlayerManager>();
        targetTransform = FindObjectOfType<PlayerManager>().transform;
        cameraTransform = Camera.main.transform;
        defaultPosition = cameraTransform.localPosition.z;
    }

    private void Start()
    {
        enviromentLayer = LayerMask.NameToLayer("Enviroment");
    }

    public void HandleAllCameraMovement()
    {
        FollowTarget();
        RotateCamera();
        HandleCameraCollision();

        if(inputManager.lockOnActive)
        {
            LockCameraPivot();
        }
        
    }

    private void FollowTarget()
    {
        Vector3 targetPosition = Vector3.SmoothDamp(transform.position, targetTransform.position, ref cameraFollowVelocity, cameraFollowSpeed);
        transform.position = targetPosition;
    }

    private  void RotateCamera()
    {
        if(inputManager.lockOnActive == false && currentLockOnTransform == null)
        {
            Vector3 rotation;
            Quaternion targetRotation;

            lookAngle += inputManager.cameraYInput * cameraLookSpeed;
            pivotAngle -= inputManager.cameraXInput * cameraPivotSpeed;
            pivotAngle = Mathf.Clamp(pivotAngle, minimumPivotAngle, maximumPivotAngle);

            rotation = Vector3.zero;
            rotation.y = lookAngle;
            targetRotation = Quaternion.Euler(rotation);
            transform.rotation = targetRotation;

            rotation = Vector3.zero;
            rotation.x = pivotAngle;
            targetRotation = Quaternion.Euler(rotation);
            cameraPivot.localRotation = targetRotation;
        }
        else
        {
            Vector3 direction = currentLockOnTransform.transform.position - transform.position;
            direction.Normalize();
            direction.y = 0;

            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = targetRotation;

            direction = currentLockOnTransform.transform.position - cameraPivot.position;
            direction.Normalize();

            targetRotation = Quaternion.LookRotation(direction);
            Vector3 eularAngle = targetRotation.eulerAngles;
            eularAngle.y = 0;
            cameraPivot.localEulerAngles = eularAngle;
        }
        
    }

    private void HandleCameraCollision()
    {
        float targetPosition = defaultPosition;
        RaycastHit hit;
        Vector3 direction = cameraTransform.position - cameraPivot.position;
        direction.Normalize();

        if(Physics.SphereCast(cameraPivot.transform.position, cameraCollisionRadius, direction, out hit, Mathf.Abs(targetPosition), collisionLayers))
        {
            float distance = Vector3.Distance(cameraPivot.position, hit.point);
            targetPosition =- (distance - cameraCollisionOffSet);
        }

        if(Mathf.Abs(targetPosition) < minimumCollisionOffSet)
        {
            targetPosition -= minimumCollisionOffSet;
        }

        cameraVectorPostion.z = Mathf.Lerp(cameraTransform.localPosition.z, targetPosition, 0.2f);
        cameraTransform.localPosition = cameraVectorPostion;
    }

    public void HandleLockOn()
    {
        float shortestDistance = Mathf.Infinity;
        float shortestDistanceFromLeftTarget = -Mathf.Infinity;
        float shortestDistanceFromRightTarget = Mathf.Infinity;

        Collider[] colliders = Physics.OverlapSphere(targetTransform.position, 26);

        for (int i = 0; i < colliders.Length; i++)
        {
            CharacterManager character = colliders[i].GetComponent<CharacterManager>();

            if(character != null && character.tag == "Enemy")
            {
                Vector3 lockTargetDirection = character.transform.position - targetTransform.position;
                float distanceFromTarget = Vector3.Distance(targetTransform.position,character.transform.position);
                float viewableAngle = Vector3.Angle(lockTargetDirection, cameraTransform.forward);
                RaycastHit hit;

                if(character.transform.root != targetTransform.transform.root &&
                    viewableAngle > -50 && viewableAngle < 50 &&
                    distanceFromTarget <= maximumDistanceFromTarget)
                {
                    if(Physics.Linecast(playerManager.lockOnTransform.position, character.lockOnTransform.position, out hit))
                    {
                        Debug.DrawLine(playerManager.lockOnTransform.position, character.lockOnTransform.position);

                        if (hit.transform.gameObject.layer == enviromentLayer)
                        {
                            //cant add target if hit smth
                        }
                        else
                        {
                            availableTargets.Add(character);
                        }
                    }
                    
                }
            }
        }

        for (int k = 0; k < availableTargets.Count; k++)
        {
            float distanceFromTarget = Vector3.Distance(targetTransform.position, availableTargets[k].transform.position);

            if(distanceFromTarget < shortestDistance)
            {
                shortestDistance = distanceFromTarget;
                nearestLockOnTransform = availableTargets[k];
                
            }

            if(inputManager.lockOnActive)
            {
                //Vector3 relativeEnemyPosition = currentLockOnTransform.transform.InverseTransformPoint(availableTargets[k].transform.position);
                //var distanceFromLeftTarget = currentLockOnTransform.transform.position.x - availableTargets[k].transform.position.x;
                //var distanceFromRightTarget = currentLockOnTransform.transform.position.x + availableTargets[k].transform.position.x;
                Vector3 relativeEnemyPosition = inputManager.transform.InverseTransformPoint(availableTargets[k].transform.position);
                var distanceFromLeftTarget = relativeEnemyPosition.x;
                var distanceFromRightTarget = relativeEnemyPosition.x;


                if (relativeEnemyPosition.x <= 0.00 && distanceFromLeftTarget > shortestDistanceFromLeftTarget && availableTargets[k] != currentLockOnTransform)
                {
                    shortestDistanceFromLeftTarget = distanceFromLeftTarget;
                    leftLockOnTarget = availableTargets[k];
                }
                else if(relativeEnemyPosition.x >= 0.00 && distanceFromRightTarget < shortestDistanceFromRightTarget && availableTargets[k] != currentLockOnTransform)
                {
                    shortestDistanceFromRightTarget = distanceFromRightTarget;
                    rightLockOnTarget = availableTargets[k];
                }
            }
        }
    }

    public void ClearLockOn()
    {
        availableTargets.Clear();
        nearestLockOnTransform = null;
        currentLockOnTransform = null;
    }

    public void LockCameraPivot()
    {
        
        Vector3 rotation;
        Quaternion targetRotation;

        pivotAngle -= inputManager.cameraXInput * cameraPivotSpeed;

        pivotAngle = Mathf.Clamp(pivotAngle, miniumPivotXAngle, maximumPivotXAngle);

        rotation = Vector3.zero;
        rotation.x = pivotAngle;
        targetRotation = Quaternion.Euler(rotation);
        cameraPivot.localRotation = targetRotation;
    }

}
