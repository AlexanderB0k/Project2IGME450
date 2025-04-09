using UnityEngine;

public class Pickup : MonoBehaviour
{
    private int currentGridX;
    private int currentGridY;

    private float xOffset;
    private float yOffset;

    private GridManager gridManager;
    private TimerController timerController; 

    void Start()
    {
        gridManager = FindFirstObjectByType<GridManager>();
        timerController = FindFirstObjectByType<TimerController>(); 
        if (gridManager == null) return;

        // Offset values for correct tile alignment
        xOffset = -gridManager.Width / 2f + 0.5f;
        yOffset = -gridManager.Height / 2f + 0.5f;

        // Initial Random Position
        Respawn();
    }

    // TriggerEnter
    private void OnTriggerEnter2D(Collider2D player)
    {
        if (player.gameObject.name == "Player")
        {
            Debug.Log("Coin collected!");
            Respawn();

            // Reset timer to 15 seconds
            if (timerController != null)
            {
                timerController.ResetTimer(15f);
            }
        }
    }

    void Respawn()
    {
        if (gridManager == null) return;

        int gridWidth = gridManager.Width;
        int gridHeight = gridManager.Height;

        Vector2Int newGridPosition = new Vector2Int(currentGridX, currentGridY);

        // Generate a unique position that differs from the current one
        while (newGridPosition == new Vector2Int(currentGridX, currentGridY))
        {
            int randomX = Random.Range(0, gridWidth);
            int randomY = Random.Range(0, gridHeight);
            newGridPosition = new Vector2Int(randomX, randomY);
        }

        // Update new grid position
        currentGridX = newGridPosition.x;
        currentGridY = newGridPosition.y;

        // Move the pickup to the new position
        UpdatePickupPosition();
    }

    void UpdatePickupPosition()
    {
        transform.position = new Vector3(currentGridX + xOffset, currentGridY + yOffset, transform.position.z);
    }
}
