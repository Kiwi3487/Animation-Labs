using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.Mathematics;
using UnityEngine;

public class pathController : MonoBehaviour
{
    [SerializeField] public PathManager pathManager;

    private List<Waypoint> thePath;
    private Waypoint target;

    public float MoveSpeed;
    public float RotateSpeed;
    public Animator animator;

    void Start()
    {
        
        thePath = pathManager.GetPath();
        if (thePath != null && thePath.Count > 0)
        {
            target = thePath[0];
        }
    }

    void rotateTowardsTarget()
    {
        float stepSize = RotateSpeed * Time.deltaTime;

        Vector3 targetDir = target.pos - transform.position;
        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, stepSize, 0.0f);
        // transform.rotation = quaternion.LookRotation(newDir);
    }

    void moveTowards()
    {
        float stepSize = Time.deltaTime * MoveSpeed;
        float distanceToTarget = Vector3.Distance(transform.position, target.pos);
        if (distanceToTarget < stepSize)
        {
            return;
        }

        Vector3 moveDir = Vector3.forward;
        transform.Translate(moveDir* stepSize);
    }

    // void Update()
    // {
    //     if (Input.anyKeyDown)
    //     {
    //         isSprinting = !isSprinting;
    //         animator.SetBool("Sprinting", isSprinting);
    //     }
    //
    //     if (isSprinting)
    //     {
    //         rotateTowardsTarget();
    //         moveTowards();
    //     }
    // }
    
    private void OnTriggerEnter(Collider other)
    {
        target = pathManager.GetNextTarget();
    }
}
