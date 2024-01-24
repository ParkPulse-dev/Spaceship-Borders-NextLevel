using UnityEngine;

public class ScreenWrapColl : MonoBehaviour
{
    private Rigidbody2D myRigidBody;
    private float objectWidth;

    public float wrapOffset = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        // Get the Rigidbody2D component attached to this object
        myRigidBody = GetComponent<Rigidbody2D>();

        // Get the width of the object
        objectWidth = GetComponent<SpriteRenderer>().bounds.size.x;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the other collider is a trigger wall
        if (other.CompareTag("ScreenWrapTrigger"))
        {
            WrapObject();
        }
    }
    private void WrapObject()
    {
        // Move the object to the corresponding position on the opposite side
        float newX = -transform.position.x + Mathf.Sign(transform.position.x) * wrapOffset;
        transform.position = new Vector2(newX, transform.position.y);
    }
}
