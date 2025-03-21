using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private int _width;
    [SerializeField] private int _height;
    [SerializeField] private Tile _tileprefab;

    public int Width => _width;   // Added for boundary access
    public int Height => _height; // Added for boundary access

    void GenerateGrid()
    {
        float xOffset = -_width / 2f + 0.5f;
        float yOffset = -_height / 2f + 0.5f;

        for (int i = 0; i < _width; i++)
        {
            for (int j = 0; j < _height; j++)
            {
                Vector3 tilePosition = new Vector3(i + xOffset, j + yOffset, 0);
                Tile spawnTile = Instantiate(_tileprefab, tilePosition, Quaternion.identity);
                spawnTile.name = $"Tile {i} {j}";
            }
        }
    }

    void Start()
    {
        GenerateGrid();
    }
}
