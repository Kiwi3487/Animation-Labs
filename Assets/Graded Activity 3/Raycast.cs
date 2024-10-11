using UnityEngine;
using UnityEngine.AI;

public class ClickToMove : MonoBehaviour
{
    public NavMeshAgent agent;
    public LayerMask groundLayer;  // Layer mask to define where the agent can move (e.g., ground layer)

    void Start()
    {
        if (agent == null)
        {
            agent = GetComponent<NavMeshAgent>();
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Left-click
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Cast ray on the ground layer
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundLayer))
            {
                // Move the agent to the clicked position
                agent.SetDestination(hit.point);
            }
        }
    }
}
