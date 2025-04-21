using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    private Points points;
    [SerializeField] private int totalPoints;

    private GridAction action;
    [SerializeField] private float totalTime;

    private void Awake()
    {
        points = FindFirstObjectByType<Points>();
        action = FindFirstObjectByType<GridAction>();
    }

    private void Update()
    {
        if (points != null)
        {
            totalPoints = points.points;
        }

        if (action != null)
        {
            totalTime = action.GlobalTimer;
        }
    }
}
