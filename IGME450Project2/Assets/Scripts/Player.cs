using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    //Get the current grid x and y 
    private int currentGridX;
    private int currentGridY;

    //Add the offset for the x and y value
    private float xOffset;
    private float yOffset;

    private GridManager gridManager;


    void Start()
    {
        //Get the gridManager
        gridManager = FindFirstObjectByType<GridManager>();
        if (gridManager == null) return;

        // Offset to align grid tiles correctly
        xOffset = -gridManager.Width / 2f + 0.5f;
        yOffset = -gridManager.Height / 2f + 0.5f;

        // Start in the center of the grid
        currentGridX = Mathf.FloorToInt(gridManager.Width / 2f);
        currentGridY = Mathf.FloorToInt(gridManager.Height / 2f);

        UpdatePlayerPosition();

    }

    public void Move(InputAction.CallbackContext context)
    {
        //So it doesn't update every frame when pressed
        if (!context.performed) return;

        Vector2 input = context.ReadValue<Vector2>();

        //boundary check with Mathf.Clamp
        currentGridX = Mathf.Clamp(currentGridX + (int)input.x, 0, gridManager.Width - 1);
        currentGridY = Mathf.Clamp(currentGridY + (int)input.y, 0, gridManager.Height - 1);

        UpdatePlayerPosition(); 
    }

    //Update the player position 
    void UpdatePlayerPosition()
    {
        transform.position = new Vector3(currentGridX + xOffset, currentGridY + yOffset, transform.position.z);
    }
}
