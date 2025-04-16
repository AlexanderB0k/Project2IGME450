using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private int currentGridX;
    private int currentGridY;

    private float xOffset;
    private float yOffset;

    private GridManager gridManager;
    private Player player;

    public void Setup(GridManager gm, Player p)
    {
        gridManager = gm;
        player = p;

        xOffset = -gridManager.Width / 2f + 0.5f;
        yOffset = -gridManager.Height / 2f + 0.5f;

        Respawn(); // Now safe to call
    }

    public void Respawn()
    {
        if (gridManager == null || player == null) return;

        Vector2Int playerPos = player.GetGridPosition();
        Vector2Int newGridPosition;

        do
        {
            int randomX = Random.Range(0, gridManager.Width);
            int randomY = Random.Range(0, gridManager.Height);
            newGridPosition = new Vector2Int(randomX, randomY);
        } while (newGridPosition == playerPos);

        currentGridX = newGridPosition.x;
        currentGridY = newGridPosition.y;

        UpdateObstaclePosition();
    }

    void UpdateObstaclePosition()
    {
        transform.position = new Vector3(currentGridX + xOffset, currentGridY + yOffset, transform.position.z - 1);
    }

    public Vector2Int GetGridPosition()
    {
        return new Vector2Int(currentGridX, currentGridY);
    }
}
