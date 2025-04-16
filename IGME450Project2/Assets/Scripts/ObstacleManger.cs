using System.Collections.Generic;
using UnityEngine;

public class ObstacleManger : MonoBehaviour
{
    [SerializeField] private GameObject obstaclePrefab;
    [SerializeField] private GridManager gridManager;
    [SerializeField] private Player player;

    void Start()
    {
        gridManager = FindFirstObjectByType<GridManager>();
        player = FindFirstObjectByType<Player>();

        if (gridManager == null || player == null || obstaclePrefab == null) return;

        for (int i = 0; i < 3; i++)
        {
            GameObject obj = Instantiate(obstaclePrefab);
            Obstacle obstacle = obj.GetComponent<Obstacle>();
            obstacle.Setup(gridManager, player); 
        }
    }

}
