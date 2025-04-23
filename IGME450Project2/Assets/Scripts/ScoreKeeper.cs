using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    private ScoreKeeper Instance;

    [SerializeField] private Points points;
    [SerializeField] private int totalPoints;

    [SerializeField] private GridAction action;
    [SerializeField] private float totalTime;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

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
