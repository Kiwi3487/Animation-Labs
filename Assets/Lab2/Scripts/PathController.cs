using System.Collections.Generic;
using UnityEngine;

public class PathController : MonoBehaviour
{
    [SerializeField] private PathManager pathManager;

    private List<Waypoint> _thePath;
    private Waypoint _target;

    public float moveSpeed;
    public float rotateSpeed;
    public Animator animator;
    private bool _isSprinting;
    private bool _canMove = true;
    
    void Start()
    {
        animator.SetBool("isSprinting", false);
        
        _thePath = pathManager.GetPath();
        if (_thePath != null && _thePath.Count > 0)
        {
            _target = _thePath[0];
        }
    }

    private void RotateTowardsTarget()
    {
        float stepSize = rotateSpeed * Time.deltaTime;

        Vector3 targetDir = _target.pos - transform.position;
        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, stepSize, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDir);
    }

    private void MoveToward()
    {
        float stepSize = Time.deltaTime * moveSpeed;
        float distanceToTarget = Vector3.Distance(transform.position, _target.pos);
        if (distanceToTarget < stepSize)
        {
            return;
        }
        Vector3 moveDir = Vector3.forward;
        transform.Translate(moveDir * stepSize);
    }

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            _isSprinting = !_isSprinting;
            animator.SetBool("isSprinting", _isSprinting);
        }

        if (_isSprinting && _canMove)
        {
            RotateTowardsTarget();
            MoveToward();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        _target = pathManager.GetNextTarget();

        if (other.gameObject.CompareTag("Player"))
        {
            animator.SetBool("isSprinting", false);
            _canMove = false;
        }
    }
}