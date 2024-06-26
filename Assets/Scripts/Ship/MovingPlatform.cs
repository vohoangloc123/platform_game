
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float speed = 2f;
    public Transform pointA;
    public Transform pointB;
    private bool movingToPointB = true;

    private Quaternion startRotation;
    private Quaternion endRotation;

    private Transform playerTransform;
    private Vector3 lastPlatformPosition;

    void Start()
    {
        startRotation = transform.rotation;
        endRotation = Quaternion.Euler(0f, 180f, 0f); // Rotate 180 degrees on the Y axis
        lastPlatformPosition = transform.position;
    }

    void Update()
    {
        Vector3 targetPosition = movingToPointB ? pointB.position : pointA.position;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // Update player's position based on platform movement
        if (playerTransform != null)
        {
            Vector3 platformMovement = transform.position - lastPlatformPosition;
            playerTransform.position += platformMovement;
        }

        // Check if reached the target point
        if (transform.position == targetPosition)
        {
            if (movingToPointB)
            {
                movingToPointB = false;
                RotateBoat(endRotation);
            }
            else
            {
                movingToPointB = true;
                RotateBoat(startRotation);
            }
        }

        lastPlatformPosition = transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerTransform = collision.transform;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerTransform = null;
        }
    }

    // Method to rotate the boat
    private void RotateBoat(Quaternion targetRotation)
    {
        transform.rotation = targetRotation;
    }
}
