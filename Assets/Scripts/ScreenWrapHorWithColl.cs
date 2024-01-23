using UnityEngine;

public class ScreenWrapColl : MonoBehaviour
{
    private Rigidbody2D myRigidBody;
    private float objectWidth;

    public float wrapOffset = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        // Get the Rigidbody2D component attached to this GameObject
        myRigidBody = GetComponent<Rigidbody2D>();

        // Get the width of the object (assuming it's a 2D object)
        objectWidth = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // OnTriggerEnter2D is called when the Collider2D other enters the trigger
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the other collider is a trigger wall
        if (other.CompareTag("ScreenWrapTrigger"))
        {
            // Move the object to the corresponding position on the opposite side
            Vector2 newPosition = new Vector2(-transform.position.x + Mathf.Sign(transform.position.x) * wrapOffset, transform.position.y);
            transform.position = newPosition;
        }
    }
}
