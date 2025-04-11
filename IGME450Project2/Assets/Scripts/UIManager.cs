using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private Sprite[] _lives;
    public int livesRemaining;
    [SerializeField] private Background background;
    private TimerController timer;
    [SerializeField] private GameObject gameOverScreen;

    void Start()
    {
        //Initalize the timer for the game
        timer = FindFirstObjectByType<TimerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ShowGameOverScreen()
    {
        SceneManager.LoadScene("GameOverScene");
    }

    public void updateLives(int currentLives)
    {
        livesRemaining = currentLives;

        if (livesRemaining == 0 || timer.seconds <= 0f)
        {
            ShowGameOverScreen();
        }
        //Create the interaction between the attack and the player can update this
        //Keep this for now 

    }
}
