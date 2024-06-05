
using UnityEngine;

public class FishMovement : MonoBehaviour
{
    public float swimSpeed = 5f; // Speed of the fish

    private Transform player; // Reference to the player's transform
    private bool isSwimming = false; // Flag to determine if the fish is swimming in water
    public Collider2D swimAreaCollider; // Reference to the collider of the swim area
    private SpriteRenderer spriteRenderer; // Reference to the SpriteRenderer component
    private int currentDirection = 1; // Current direction of the fish (1 for right, -1 for left)
    private Vector3 pointA; // Point A
    private Vector3 pointB; // Point B
    private bool reachedPointA = false; // Flag to check if the fish has reached point A

    public void SetSwimAreaCollider(Collider2D collider)
    {
        swimAreaCollider = collider;
    }

    void Start()
    {
        // Find the player GameObject by tag "Player"
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // Find the swim area GameObject by tag "SwimArea"
        swimAreaCollider = GameObject.FindGameObjectWithTag("SwimArea").GetComponent<Collider2D>();

        // Get the SpriteRenderer component
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Debug: Check if swimAreaCollider is null
        if (swimAreaCollider == null)
        {
            Debug.LogError("Swim area collider not found!");
        }

        // Set initial positions for point A and point B
        pointA = transform.position;
        pointB = new Vector3(pointA.x + 10f, pointA.y, pointA.z); // Adjust the x-coordinate of point B as needed
    }

    void Update()
    {
        // Check if the player is found
        if (player != null)
        {
            // Check if the player is inside the swim area
            if (swimAreaCollider.OverlapPoint(player.position))
            {
                // Calculate direction towards the player
                Vector3 direction = player.position - transform.position;

                // Normalize the direction vector
                direction.Normalize();

                // Update currentDirection based on movement direction
                if (direction.x > 0)
                {
                    currentDirection = 1; // Right
                }
                else
                {
                    currentDirection = -1; // Left
                }

                // Flip the fish sprite based on direction
                spriteRenderer.flipX = (currentDirection == -1);

                // Move the fish towards the player
                transform.Translate(direction * swimSpeed * Time.deltaTime);
                isSwimming = true;
                reachedPointA = false; // Reset reachedPointA flag
            }
            else
            {
                // If the player is not inside the swim area
                if (!reachedPointA)
                {
                    // Move fish back to point A
                    Vector3 directionToA = pointA - transform.position;
                    directionToA.Normalize();
                    transform.Translate(directionToA * swimSpeed * Time.deltaTime);

                    // Flip the fish sprite based on movement direction
                    spriteRenderer.flipX = (directionToA.x < 0);

                    // Check if fish has reached point A
                    if (Vector3.Distance(transform.position, pointA) < 0.1f)
                    {
                        reachedPointA = true;
                    }
                }
                else
                {
                    // Move fish from point A to point B
                    Vector3 directionToB = pointB - transform.position;
                    directionToB.Normalize();
                    transform.Translate(directionToB * swimSpeed * Time.deltaTime);

                    // Flip the fish sprite based on movement direction
                    spriteRenderer.flipX = (directionToB.x < 0);

                    // Check if fish has reached point B
                    if (Vector3.Distance(transform.position, pointB) < 0.1f)
                    {
                        // Set new point A and point B
                        Vector3 temp = pointA;
                        pointA = pointB;
                        pointB = temp;
                        reachedPointA = false;
                    }
                }
                // Reset swimming flag
                isSwimming = false;
            }
        }
    }
}
