using System.Collections;
using UnityEngine;

public class WallClimber : MonoBehaviour
{
    [Tooltip("Direction and speed of movement, in units per second")]
    [SerializeField] Vector3 speed;

    private bool isColliding = false;

    void Update()
    {
        if (!isColliding)
        {
            transform.position += speed * Time.deltaTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isColliding = true;
        HandleCollision(collision.contacts[0].point);
    }


    private void OnCollisionExit2D(Collision2D collision)
    {
        isColliding = false;
    }

    private void HandleCollision(Vector2 collisionPoint)
    {
        // Determine the direction of the collision
        Vector2 collisionDirection = collisionPoint - (Vector2)transform.position;

        // Calculate the dot product to check if the collision is happening from the front
        float dotProduct = Vector2.Dot(collisionDirection.normalized, speed.normalized);

        // If the collision is happening from the front, stop movement in that direction
        if (dotProduct > 0)
        {
            speed = Vector3.zero;
        }
    }
}
