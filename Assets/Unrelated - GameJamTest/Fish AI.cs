using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishAI : MonoBehaviour
{
    public float speed;
    public bool canMove;
    public Transform[] waypoints;
    private int currentWaypoint;
    private int lastWaypoint = -1;
    void Start()
    {
        canMove = true;
        GetNextWaypoint();
    }
    void Update()
    {
        if (canMove && waypoints.Length > 0)
        {
            Transform targetWaypoint = waypoints[currentWaypoint];
            transform.position = Vector3.MoveTowards(transform.position, targetWaypoint.position, speed * Time.deltaTime);
            if (Vector3.Distance(transform.position, targetWaypoint.position) < 0.1f)
            {
                GetNextWaypoint();
            }
        }
    }
    void GetNextWaypoint()
    {
        if (waypoints.Length < 2) return;
        int newWaypoint;
        do
        {
            newWaypoint = Random.Range(0, waypoints.Length);
        } while (newWaypoint == lastWaypoint);
        
        lastWaypoint = currentWaypoint;
        currentWaypoint = newWaypoint;
    }
}
