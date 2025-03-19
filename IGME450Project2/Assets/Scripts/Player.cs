using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private int currentGridX;
    private int currentGridY;

    // Grid boundaries
    private float minX, maxX, minY, maxY;
    private float xOffset;
    private float yOffset;

    void Start()
    {
        // Find the GridManager and set boundaries
        GridManager gridManager = FindFirstObjectByType<GridManager>();
        if (gridManager != null)
        {
            // Offset values for correct tile alignment
            xOffset = -gridManager.Width / 2f + 0.5f;
            yOffset = -gridManager.Height / 2f + 0.5f;

            // Correct Boundary Calculation with Offset
            minX = xOffset;
            maxX = xOffset + gridManager.Width - 1;
            minY = yOffset;
            maxY = yOffset + gridManager.Height - 1;

            // Correct Center Calculation
            currentGridX = Mathf.FloorToInt(gridManager.Width / 2f);
            currentGridY = Mathf.FloorToInt(gridManager.Height / 2f);
        }

        // Position the player at the correct center tile
        UpdatePlayerPosition();
    }

    public void Move(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Vector2 input = context.ReadValue<Vector2>();

            // Update grid coordinates for movement
            int newGridX = currentGridX + (int)input.x;
            int newGridY = currentGridY + (int)input.y;

            // Clamp new position within grid boundaries
            if (newGridX + xOffset >= minX && newGridX + xOffset <= maxX)
                currentGridX = newGridX;

            if (newGridY + yOffset >= minY && newGridY + yOffset <= maxY)
                currentGridY = newGridY;

            // Move the player to the new tile position
            UpdatePlayerPosition();
        }
    }

    void UpdatePlayerPosition()
    {
        transform.position = new Vector3(currentGridX + xOffset, currentGridY + yOffset, transform.position.z);
    }
}
