using UnityEngine;

public class Pickup : MonoBehaviour
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

            // Initial Random Position
            Respawn();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Player") 
        {
            Debug.Log("Coin collected!");
            Respawn();
        }
    }

    void Respawn()
    {
        GridManager gridManager = FindFirstObjectByType<GridManager>();
        if (gridManager == null) return;

        int gridWidth = gridManager.Width;
        int gridHeight = gridManager.Height;

        Vector2Int newGridPosition = new Vector2Int(currentGridX, currentGridY); // Start with current position

        // Keep generating new positions until it finds a different one
        while (newGridPosition.x == currentGridX && newGridPosition.y == currentGridY)
        {
            int randomX = Random.Range(0, gridWidth);
            int randomY = Random.Range(0, gridHeight);
            newGridPosition = new Vector2Int(randomX, randomY);
        }

        // Set the coin's new grid position
        currentGridX = newGridPosition.x;
        currentGridY = newGridPosition.y;

        // Move the coin to the correct tile position
        UpdateCoinPosition();
    }

    void UpdateCoinPosition()
    {
        transform.position = new Vector3(currentGridX + xOffset, currentGridY + yOffset, transform.position.z);
    }
}
