using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private Sprite[] _lives;
    public int livesRemaining;
    [SerializeField] private Background background;
    private TimerController timer;

    //Created the lives so the player class can access it 
    public int lives
    {
        get
        {
            return _lives.Length;
        }
    }
    void Start()
    {
        //Initalize the timer for the game
        timer = FindFirstObjectByType<TimerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateLives(int currentLives)
    {
        //If the livesRemaining is 0 you get sent to the game over Screen 
        if (livesRemaining == 0 || timer.seconds == 0f)
        { 
            //Go to the game over screen 

        }

        

        //Create the interaction between the attack and the player can update this
        //Keep this for now 

    }
}
