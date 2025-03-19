using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 moveInput;

    // Grid boundaries
    private float minX, maxX, minY, maxY;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Find the GridManager and set boundaries
        GridManager gridManager = FindFirstObjectByType<GridManager>();
        if (gridManager != null)
        {
            minX = -gridManager.Width / 2f -0.6f;
            maxX = gridManager.Width / 2f - 0.6f;
            minY = -gridManager.Height / 2f ;
            maxY = gridManager.Height / 2f;
        }

        // Position the player at the center of the grid
        transform.position = new Vector3(0, 0, -1);
    }

    void Update()
    {
        rb.linearVelocity = moveInput * moveSpeed;

        // Clamp player position within grid boundaries
        float clampedX = Mathf.Clamp(transform.position.x, minX, maxX - 1);
        float clampedY = Mathf.Clamp(transform.position.y, minY, maxY - 1);

        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }

    public void Move(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }
}
