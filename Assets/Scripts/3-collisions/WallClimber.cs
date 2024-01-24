using System.Collections;
using UnityEngine;

public class WallClimber : MonoBehaviour
{
    [Tooltip("Direction and speed of movement, in units per second")]
    [SerializeField] Vector3 speed;

    private bool isColliding = false;

    void Update()
    {
        // Check if the object is not currently colliding
        if (!isColliding)
        {
            // Update the object's position based on the speed vector
            transform.position += speed * Time.deltaTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Set the flag to indicate that the object is currently colliding
        isColliding = true;

        // Get the first contact point from the collision
        Vector2 collisionPoint = collision.contacts[0].point;

        // Call the HandleCollision method with the obtained collision point
        HandleCollision(collisionPoint);
    }


    private void OnCollisionExit2D(Collision2D collision)
    {
        // Reset the collision flag when the object is no longer colliding
        isColliding = false;
    }

    private void HandleCollision(Vector2 collisionPoint)
    {
        // Determine the direction vector from the object's position to the collision point
        Vector2 collisionDirection = collisionPoint - (Vector2)transform.position;

        // Calculate the dot product between the normalized collision direction and the normalized speed vector
        float dotProduct = Vector2.Dot(collisionDirection.normalized, speed.normalized);

        // If the dot product is positive, it means the collision is happening from the front
        if (dotProduct > 0)
        {
            // Stop the movement by setting the speed vector to zero
            speed = Vector3.zero;
        }
    }
}
