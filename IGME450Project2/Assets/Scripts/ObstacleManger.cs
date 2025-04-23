using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ObstacleManger : MonoBehaviour
{
    [SerializeField] private GameObject obstaclePrefab;

    private GridManager gridManager;
    private Player player;
    private CoinPickup coin;
    private Points points;

    private List<Obstacle> obstacles = new List<Obstacle>();
    private int obstaclesSpawned = 3; // Start with 3
    private const int maxObstacles = 7;

    void Start()
    {
        gridManager = FindFirstObjectByType<GridManager>();
        player = FindFirstObjectByType<Player>();
        coin = FindFirstObjectByType<CoinPickup>();
        points = FindFirstObjectByType<Points>();

        if (gridManager == null || player == null || coin == null || obstaclePrefab == null) return;

        // Spawn initial 3 obstacles
        for (int i = 0; i < obstaclesSpawned; i++)
        {
            SpawnObstacle();
        }

        // Let CoinPickup access this manager if needed
        coin.SetObstacleManager(this);
    }

    void Update()
    {
        if (points == null) return;

        int currentPoints = points.GetCurrentPoints;
        int targetObstacleCount = Mathf.Min(3 + (currentPoints / 3), maxObstacles);

        if (targetObstacleCount > obstaclesSpawned)
        {
            int toSpawn = targetObstacleCount - obstaclesSpawned;

            for (int i = 0; i < toSpawn; i++)
            {
                SpawnObstacle();
            }

            obstaclesSpawned = targetObstacleCount;
        }
    }

    private void SpawnObstacle()
    {
        GameObject obj = Instantiate(obstaclePrefab);
        Obstacle obstacle = obj.GetComponent<Obstacle>();
        obstacle.Setup(gridManager, player, coin);
        obstacles.Add(obstacle);
    }

    public void RespawnAllObstacles()
    {
        foreach (Obstacle obstacle in obstacles)
        {
            obstacle.Respawn();
        }
    }
}
