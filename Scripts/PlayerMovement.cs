using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 movementInput;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        if (Mathf.Abs(horizontal) > 0)
        {
            movementInput = new Vector2(horizontal, 0).normalized;
        }
        else if (Mathf.Abs(vertical) > 0)
        {
            movementInput = new Vector2(0, vertical).normalized;
        }
        else
        {
            movementInput = Vector2.zero;
        }
    }

    void FixedUpdate()
    {
        rb.velocity = movementInput * moveSpeed;
    }
}
