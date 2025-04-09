using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    private int currentGridX;
    private int currentGridY;

    private float xOffset;
    private float yOffset;

    private GridManager gridManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private Sprite[] _lives;
    public int livesRemaining;
    private TimerController timer;
    [SerializeField] private GameObject gameOverScreen;


    void Start()
    {
        timer = FindFirstObjectByType<TimerController>();

        // Get the GridManager
        gridManager = FindFirstObjectByType<GridManager>();
        if (gridManager == null) return;

        // Offset to align grid tiles correctly
        xOffset = -gridManager.Width / 2f + 0.5f;
        yOffset = -gridManager.Height / 2f + 0.5f;

        // Start in the center of the grid
        currentGridX = Mathf.FloorToInt(gridManager.Width / 2f);
        currentGridY = Mathf.FloorToInt(gridManager.Height / 2f);

        UpdatePlayerPosition();
    }

    void Update()
    {
        updateLives(3);
    }

    public void Move(InputAction.CallbackContext context)
    {
        // Only trigger on performed input
        if (!context.performed) return;

        Vector2 input = context.ReadValue<Vector2>();

        int targetX = Mathf.Clamp(currentGridX + (int)input.x, 0, gridManager.Width - 1);
        int targetY = Mathf.Clamp(currentGridY + (int)input.y, 0, gridManager.Height - 1);

        // Block movement if there's an obstacle at the target location
        if (IsBlockedByObstacle(targetX, targetY)) return;

        currentGridX = targetX;
        currentGridY = targetY;

        UpdatePlayerPosition();
    }

    private void UpdatePlayerPosition()
    {
        transform.position = new Vector3(currentGridX + xOffset, currentGridY + yOffset, transform.position.z);
    }

    private bool IsBlockedByObstacle(int targetX, int targetY)
    {

        Obsctacle[] obstacles = Object.FindObjectsByType<Obsctacle>(FindObjectsSortMode.None);
        
        foreach (Obsctacle obstacle in obstacles)
        {
            Vector2Int obstaclePos = obstacle.GetGridPosition();
            if (obstaclePos.x == targetX && obstaclePos.y == targetY)
                return true;
        }
        return false;
    }

    public Vector2Int GetGridPosition()
    {
        return new Vector2Int(currentGridX, currentGridY);
    }

    private void ShowGameOverScreen()
    {
        SceneManager.LoadScene(1);
        timer.seconds = 0; // Pauses the game
    }

    public void updateLives(int currentLives)
    {
        livesRemaining = currentLives;

        if (livesRemaining == 0 || timer.seconds <= 0f)
        {
            Debug.Log("Hella");
            ShowGameOverScreen();
        }
        //Create the interaction between the attack and the player can update this
        //Keep this for now 

    }
}
