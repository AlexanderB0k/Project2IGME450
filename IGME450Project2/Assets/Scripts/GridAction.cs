using System.Collections.Generic;
using UnityEngine;

public enum Difficulty
{
    Starting,
    Beginner,
    Novice,
    Advance,
    Expert
}

public class GridAction : MonoBehaviour
{ 

    [SerializeField] private List<AttackPatterns> fullPatternList;

    [SerializeField] private Difficulty _currentDifficulty;

    [SerializeField] private AttackPatterns currentPattern;

    private Dictionary<Difficulty , List<AttackPatterns>> PatternsRegistry = new Dictionary<Difficulty, List<AttackPatterns>>();

    private List<AttackPatterns> selectedPatternList;

    [Header("Timers")]

    [SerializeField] private float globalTimer = 0f;

    private float newPatterndelay = 1.0f;

    private float patternLifeTimeTImer = 1.0f;

    private void Start()
    {
        DefineDictionary();

        
    }

    private void Update()
    {
        globalTimer += Time.deltaTime;

        //Step 1 - determine the current difficulty based on the time
        DetermineDiffuclty();

        //Step 2 - get a random pattern based on the currenty difficulty
        GetRandomPattern();

        //Step 3 - changes the tile
        ChangeGridPattern();
    }

    /// <summary>
    /// 
    /// </summary>
    private void ChangeGridPattern()
    {
        for (int x = 0; x < currentPattern.cols; x++)
        {
            string tempString = "";
            for (int y = 0; y < currentPattern.rows; y++)
            {
                tempString += $" {currentPattern.tileGrid[x].row[y].isDangerous}";

                if (currentPattern.tileGrid[x].row[y].isDangerous)
                {
                    GridManager.Instance.TileList[y][x].GetComponent<SpriteRenderer>().color = Color.red;
                    GridManager.Instance.TileList[y][x].GetComponent<SpriteRenderer>().tag = "Damage";
                }

            }

            //Debug.Log(tempString);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    private void DetermineDiffuclty()
    {
        //When under a certain time the difficulty will be starting
        if (globalTimer < 1.0f)
        {
            //Debug.Log("starting");
            _currentDifficulty = Difficulty.Starting;
        }
        else if (globalTimer >= 1.0f && globalTimer < 2.0f)
        {
            //Debug.Log("beginner");
            _currentDifficulty = Difficulty.Beginner;
        }
        else if (globalTimer >= 2.0f && globalTimer < 3.0f)
        {
            //Debug.Log("novice");
            _currentDifficulty = Difficulty.Novice;
        }
        else if (globalTimer >= 3.0f && globalTimer < 4.0f)
        {
            //Debug.Log("advance");
            _currentDifficulty = Difficulty.Advance;
        }
        else if (globalTimer >= 7.0f)
        {
            //Debug.Log("expert");
            _currentDifficulty = Difficulty.Expert;
        }
        
    }

    /// <summary>
    /// 
    /// </summary>
    private void GetRandomPattern()
    {
        selectedPatternList = PatternsRegistry[_currentDifficulty];

        //Debug.Log(selectedPatternList.Count);

        //if the attack pattern has a connected pattern instead of getting a random pattern it gets the next connected pattern
        if (currentPattern.connectedPatterns != null)
        {
            currentPattern = currentPattern.connectedPatterns;
        }
        //if the current pattern doesn't have another conncted pattern it gets a new random pattern
        else
        {
            currentPattern = selectedPatternList[Random.Range(0, selectedPatternList.Count - 1)];
        }
        
    }

    /// <summary>
    /// At the start of the 
    /// </summary>
    private void DefineDictionary()
    {
        List<AttackPatterns> starting = new List<AttackPatterns>();

        List<AttackPatterns> beginner = new List<AttackPatterns>();

        List<AttackPatterns> novice = new List<AttackPatterns>();

        List<AttackPatterns> advance = new List<AttackPatterns>();

        List<AttackPatterns> expert = new List<AttackPatterns>();

        //If we add another diffuclty change the the number 
        for (int x = 0; x < fullPatternList.Count; x++)
        {
            switch (fullPatternList[x].DifficultyRating)
            {
                case Difficulty.Starting:
                    starting.Add(fullPatternList[x]);
                    break;
                case Difficulty.Beginner:
                    beginner.Add(fullPatternList[x]);
                    break;
                case Difficulty.Novice:
                    novice.Add(fullPatternList[x]);
                    break;
                case Difficulty.Advance:
                    advance.Add(fullPatternList[x]);
                    break;
                case Difficulty.Expert:
                    expert.Add(fullPatternList[x]);
                    break;
            }
        }

        PatternsRegistry.Add(Difficulty.Starting, starting);
        PatternsRegistry.Add(Difficulty.Beginner, beginner);
        PatternsRegistry.Add(Difficulty.Novice, novice);
        PatternsRegistry.Add(Difficulty.Advance, advance);
        PatternsRegistry.Add(Difficulty.Expert, expert);
    }
}
