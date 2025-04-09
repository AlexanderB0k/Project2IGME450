using UnityEngine;

public class Obsctacle : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private int currentGridX;
    private int currentGridY;

    private float xOffset;
    private float yOffset;

    private GridManager gridManager;
    private Player player;
    private Pickup pickup;

    void Start()
    {
        gridManager = FindFirstObjectByType<GridManager>();
        player = FindFirstObjectByType<Player>();
        pickup = FindFirstObjectByType<Pickup>();

        if (gridManager == null || player == null) return;

        // Offset for tile alignment
        xOffset = -gridManager.Width / 2f + 0.5f;
        yOffset = -gridManager.Height / 2f + 0.5f;

        // Start in the center of the grid
        currentGridX = Mathf.FloorToInt(gridManager.Width / 2f);
        currentGridY = Mathf.FloorToInt(gridManager.Height / 2f);

        Respawn();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Respawn()
    {
        if (gridManager == null || player == null) return;

        Vector2Int playerPos = player.GetGridPosition();
        Vector2Int pickupPos = pickup.GetGridPosition();

        int randomX = Random.Range(0, gridManager.Width);
        int randomY = Random.Range(0, gridManager.Height);
        Vector2Int newGridPosition = new Vector2Int(randomX, randomY);

        while (newGridPosition == playerPos || newGridPosition == pickupPos)
        {
            randomX = Random.Range(0, gridManager.Width);
            randomY = Random.Range(0, gridManager.Height);
            newGridPosition = new Vector2Int(randomX, randomY);
        }

        currentGridX = newGridPosition.x;
        currentGridY = newGridPosition.y;

        UpdateObstaclePosition();
    }

    void UpdateObstaclePosition()
    {
        transform.position = new Vector3(currentGridX + xOffset, currentGridY + yOffset, transform.position.z);
    }

    public Vector2Int GetGridPosition()
    {
        return new Vector2Int(currentGridX, currentGridY);
    }
}
