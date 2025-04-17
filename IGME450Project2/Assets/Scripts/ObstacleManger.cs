using System.Collections.Generic;
using UnityEngine;

public class ObstacleManger : MonoBehaviour
{
    [SerializeField] private GameObject obstaclePrefab;
    [SerializeField] private GridManager gridManager;
    [SerializeField] private Player player;
    [SerializeField] private CoinPickup coin;

    private List<Obstacle> obstacles = new List<Obstacle>();

    void Start()
    {
        gridManager = FindFirstObjectByType<GridManager>();
        player = FindFirstObjectByType<Player>();
        coin = FindFirstObjectByType<CoinPickup>();

        if (gridManager == null || player == null || obstaclePrefab == null) return;

        for (int i = 0; i < 3; i++)
        {
            GameObject obj = Instantiate(obstaclePrefab);
            Obstacle obstacle = obj.GetComponent<Obstacle>();
            obstacle.Setup(gridManager, player, coin);
            obstacles.Add(obstacle);
        }

        // Give CoinPickup access to this manager
        coin.SetObstacleManager(this);
    }

    public void RespawnAllObstacles()
    {
        foreach (Obstacle obstacle in obstacles)
        {
            obstacle.Respawn();
        }
    }

}
