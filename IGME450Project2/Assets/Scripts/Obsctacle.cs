using UnityEngine;

public class Obsctacle : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private int currentGridX;
    private int currentGridY;

    private float xOffset;
    private float yOffset;

    private GridManager gridManager;


    void Start()
    {
        gridManager = FindFirstObjectByType<GridManager>();

        // Offset values for correct tile alignment
        xOffset = -gridManager.Width / 2f + 0.5f;
        yOffset = -gridManager.Height / 2f + 0.5f;

        // Initial Random Position
        Respawn();
    }

    // Update is called once per frame
    void Update()
    {
        
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
        UpdatePosition();

    }

    void UpdatePosition()
    {
        transform.position = new Vector3(currentGridX + xOffset, currentGridY + yOffset, transform.position.z);
    }
}
