using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class navControl : MonoBehaviour
{
    public GameObject Target;
    private NavMeshAgent AI;
    private bool isWalking = true;
    private Animator _animator;


    void Start()
    {
        AI = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
    }


    void Update()
    {
        if(isWalking)
            AI.destination = Target.transform.position;
        else
        {
            {
                AI.destination = transform.position;
            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Dragon"))
        {
            isWalking = false;
            _animator.SetTrigger("Attack");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Dragon"))
        {
            isWalking = true;
            _animator.SetTrigger("Walk");
        }
    }
}
