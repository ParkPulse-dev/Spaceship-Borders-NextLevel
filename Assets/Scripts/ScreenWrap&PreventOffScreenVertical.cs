using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenWrap : MonoBehaviour
{
    private Rigidbody2D myRigidBody;
    private float objectWidth;
    private float objectHeight;

    // Start is called before the first frame update
    void Start()
    {
        // Get the Rigidbody2D component attached to this GameObject
        myRigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Convert the object's world position to screen coordinates
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(transform.position);

        // Get the world coordinates of the screen boundaries
        float rightSideOfScreenInWorld = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height)).x;
        float leftSideOfScreenInWorld = Camera.main.ScreenToWorldPoint(new Vector2(0f, 0f)).x;
        float topOfScreenInWorld = Camera.main.ScreenToWorldPoint(new Vector2(0f, Screen.height)).y;
        float bottomOfScreenInWorld = Camera.main.ScreenToWorldPoint(new Vector2(0f, 0f)).y;

        // Calculate the half-width and half-height of the object
        objectWidth = transform.GetComponent<SpriteRenderer>().bounds.size.x / 2;
        objectHeight = transform.GetComponent<SpriteRenderer>().bounds.size.y / 2;

        // Clamp the vertical position to stay within the screen boundaries
        // The offset (0.35f) ensures that the score value is also displayed and wont go off the screen
        float clampedY = Mathf.Clamp(transform.position.y, bottomOfScreenInWorld + objectHeight + 0.35f, topOfScreenInWorld - objectHeight);
        transform.position = new Vector2(transform.position.x, clampedY);

        // Wrap the object horizontally if it goes off one side of the screen
        if (screenPosition.x <= 0)
        {
            transform.position = new Vector2(rightSideOfScreenInWorld, transform.position.y);
        }
        else if (screenPosition.x >= Screen.width)
        {
            transform.position = new Vector2(leftSideOfScreenInWorld, transform.position.y);
        }
    }
}

