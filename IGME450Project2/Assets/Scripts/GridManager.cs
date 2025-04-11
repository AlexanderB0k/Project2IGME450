using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance;

    [SerializeField] private int _width;
    [SerializeField] private int _height;
    [SerializeField] private GameObject _tileprefab;

    private List<List<GameObject>> _tileList = new List<List< GameObject>>();

    public List<List<GameObject>> TileList { get { return _tileList; } }

    public int MapWidth { get { return _width; } }

    public int MapHeight { get { return _height; } }
    //[SerializeField] private Tile _tileprefab;

    public int Width => _width;   // Added for boundary access
    public int Height => _height; // Added for boundary access

    public void Awake()
    {
        if (Instance != null) {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        float xOffset = -_width / 2f + 0.5f;
        float yOffset = -_height / 2f + 0.5f;

        /*
        for (int i = 0; i < _width; i++)
        {
            Destroy(gameObject);
        }
        */
    }

    void Start()
    {
        GenerateGrid();
    }

    void GenerateGrid()
    {
        float xOffset = -_width / 2f + 0.5f;
        float yOffset = -_height / 2f + 0.5f;

        for (int i = 0; i < _width; i++)
        {
            List<GameObject> tempList = new List<GameObject>();
            for (int j = 0; j < _height; j++)
            {
                Vector3 tilePosition = new Vector3(i + xOffset, j + yOffset, 0);
                GameObject spawnTile = Instantiate(_tileprefab, tilePosition, Quaternion.identity);
                spawnTile.name = $"Tile {i} {j}";
                tempList.Add(spawnTile);
            }
            _tileList.Add(tempList);
        }
    }
}
