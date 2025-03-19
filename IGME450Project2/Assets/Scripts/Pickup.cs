using UnityEngine;

public class Pickup : MonoBehaviour
{
    private GridManager gridManager;
    private Vector2Int currentGridPosition;

    void Start()
    {
        gridManager = FindFirstObjectByType<GridManager>();
        Respawn(); // Initial random position
    }

    private void OnTriggerEnter2D(Collider2D player)
    {
        if (player.CompareTag("Player"))
        {
            Respawn();
        }
    }

    void Respawn()
    {
        if (gridManager == null) return;

        int gridWidth = gridManager.Width;
        int gridHeight = gridManager.Height;

        // Randomize a new position
        Vector2Int newGridPosition;

        do
        {
            int randomX = Random.Range(0, gridWidth);
            int randomY = Random.Range(0, gridHeight);
            newGridPosition = new Vector2Int(randomX, randomY); 
        }
        while (newGridPosition == currentGridPosition); // Avoid respawning on the same tile

        // Calculate new position with offset
        float xOffset = -gridManager.Width / 2f + 0.5f;
        float yOffset = -gridManager.Height / 2f + 0.5f;

        transform.position = new Vector3(newGridPosition.x + xOffset, newGridPosition.y + yOffset, 0);
        currentGridPosition = newGridPosition;
    }
}
