using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class PlayerandBobber : MonoBehaviour
{
    public Transform player; 
    public Transform follower;   
    public float maxRopeDistance = 3.0f;  
    public float maxFollowSpeed = 5.0f; 
    public float driftSpeed = 2.0f;  
    public float driftStopDistance = 0.5f; 
    public float playerStopThreshold = 0.0001f;

    private Vector3 lastPlayerPosition;
    private LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
        lastPlayerPosition = player.position;
    }

    void Update()
    {
        BobberMovement();
        FollowBobber();
        LineRenderer();
    }

    void BobberMovement()
    {
        //Move Hook/player
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        player.Translate(movement * (Time.deltaTime * 5.0f));
    }

    void FollowBobber()
    {
        Vector3 playerZXPos = new Vector3(player.position.x, follower.position.y, player.position.z);
        float distance = Vector3.Distance(follower.position, playerZXPos);
        if (distance > maxRopeDistance)
        {
            //simulate a bobber getting pulled by a rope
            var position = follower.position;
            Vector3 direction = (playerZXPos - position).normalized;
            float followSpeed = Mathf.Min(maxFollowSpeed, distance - maxRopeDistance); 
            position += direction * (followSpeed * Time.deltaTime); follower.position = position;
        }
        else
        {
            if (Vector3.Distance(player.position, lastPlayerPosition) < playerStopThreshold)
            {
                //simulate drifting after when it stops getting pulled
                var position = follower.position;
                Vector3 direction = (playerZXPos - position).normalized; position += direction * (driftSpeed * Time.deltaTime); follower.position = position;
                float driftDistance = Vector3.Distance(position, playerZXPos);
                if (driftDistance <= driftStopDistance)
                {
                    follower.position = Vector3.MoveTowards(follower.position, playerZXPos, driftSpeed * Time.deltaTime);
                }
            }
        }
        lastPlayerPosition = player.position;
    }
    void LineRenderer()
    {
        lineRenderer.SetPosition(0, player.position);
        lineRenderer.SetPosition(1, follower.position);
    }
}
