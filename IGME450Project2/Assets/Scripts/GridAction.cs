using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

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

    [SerializeField] private AttackPatterns _currentPattern;

    private Dictionary<Difficulty , List<AttackPatterns>> PatternsRegistry = new Dictionary<Difficulty, List<AttackPatterns>>();

    private List<AttackPatterns> selectedPatternList;

    [Header("Timers")]

    [SerializeField] private float globalTimer = 0f;

    [Header("Next Pattern Delay")]
    [SerializeField] private float newPatterndelay;

    [SerializeField] private float patternCooldown;

    [Header("Pattern Duration Values")]
    [SerializeField] private float patternLifeTimeTimer = 1.0f;

    [SerializeField] private float patternLifeTimeDuration = 1.0f;

    [SerializeField] private bool isApplied;

    [Header("Flashing Effect Values")]
    [SerializeField] private float flashDuration = 0.1f;

    [SerializeField] private int flashCount = 5;

    [SerializeField] private bool isFlashing = false;

    [SerializeField] private bool isDoneFlashing;

    [Header("Effect Colors")]

    [SerializeField] Color flashColor;

    [SerializeField] Color damageColor;

    private void Awake()
    {
        DefineDictionary();
    }

    private void Update()
    {
        globalTimer += Time.deltaTime;

        //Step 0 - determine the current difficulty based on the time
        DetermineDiffuclty();

        //Step 1 - Get a pattern
        //Step 2 - Apply the patterns as flashing
        //Step 3 - Apply the pattern to do damage
        //Step 4 - Clear Pattern
        //Step 5 - wait a few seconds start all over

        if (isApplied)
        {
            //Step 2 - 4
            GridChangeWithDelay();
        }
        else
        {
            if (patternCooldown > 0)
            {
                patternCooldown -= Time.deltaTime;
            }
            

            //Step 1 - Get a pattern
            if (patternCooldown == 0 && _currentDifficulty != Difficulty.Starting)
            {
                GetRandomPattern();
            }
            
            
            if (patternCooldown <= 0 && !isApplied)
            {
                patternCooldown = 0;
            }   
        }
    }

    private void GridChangeWithDelay()
    {
        //Step 2 - Apply the patterns as flashing
        if (!isDoneFlashing)
        {
            if (!isFlashing)
            {
                StartCoroutine(ShowFlash());
            }
        }
        
        if (isDoneFlashing)
        {
            patternLifeTimeTimer += Time.deltaTime;

            //Step 3 - Apply the pattern to do damage
            if (patternLifeTimeTimer < patternLifeTimeDuration)
            {
                ApplyPattern("Dangerous", damageColor);
            }
            //Step 4 - Clear Pattern
            else if (patternLifeTimeTimer >= patternLifeTimeDuration)
            {
                ClearPattern();
                patternLifeTimeTimer = 0;
                isDoneFlashing = false;
                isApplied = false;
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    private void GetRandomPattern()
    {
        selectedPatternList = PatternsRegistry[_currentDifficulty];

        //Debug.Log(selectedPatternList.Count);

        //if there is no current pattern it gets a new random pattern
        if (_currentPattern == null)
        {
            if (selectedPatternList.Count > 0)
                _currentPattern = selectedPatternList[Random.Range(0, selectedPatternList.Count)];
        }
        //if the attack pattern has a connected pattern instead of getting a random pattern it gets the next connected pattern
        else if (_currentPattern != null && _currentPattern.connectedPatterns == null)
        {
            if (selectedPatternList.Count > 0)
                _currentPattern = selectedPatternList[Random.Range(0, selectedPatternList.Count)];
        }
        else if (_currentPattern != null && _currentPattern.connectedPatterns != null)
        {
            _currentPattern = _currentPattern.connectedPatterns;
        }

        patternCooldown = newPatterndelay;
        isApplied = true;
    }

    /// <summary>
    /// At the start of the game it will define 4 different list that hold the patterns of that set difficulty
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
            if (PatternsRegistry[Difficulty.Beginner].Count > 0)
                _currentDifficulty = Difficulty.Beginner;

        }
        else if (globalTimer >= 2.0f && globalTimer < 3.0f)
        {
            //Debug.Log("novice");
            if (PatternsRegistry[Difficulty.Novice].Count > 0)
                _currentDifficulty = Difficulty.Novice;
        }
        else if (globalTimer >= 3.0f && globalTimer < 4.0f)
        {
            //Debug.Log("advance");
            if (PatternsRegistry[Difficulty.Advance].Count > 0)
                _currentDifficulty = Difficulty.Advance;
        }
        else if (globalTimer >= 7.0f)
        {
            //Debug.Log("expert");
            if (PatternsRegistry[Difficulty.Expert].Count > 0)
                _currentDifficulty = Difficulty.Expert;
        }
        
    }

    private void ChangeTileState(string tileTag, Color tileColor, GameObject tile)
    {
        tile.GetComponent<SpriteRenderer>().color = tileColor;
        tile.GetComponent<SpriteRenderer>().tag = tileTag;
    }

    private void ClearPattern()
    {
        for (int x = 0; x < _currentPattern.cols; x++)
        {
            for (int y = 0; y < _currentPattern.rows; y++)
            {
                ChangeTileState("Safe", Color.white, GridManager.Instance.TileList[y][x]);
            } 
        }
    }
   
    private void ApplyPattern(string tileTag, Color tileColor)
    {
        for (int x = 0; x < _currentPattern.cols; x++)
        {
            //string tempString = "";
            for (int y = 0; y < _currentPattern.rows; y++)
            {
                //tempString += $" {_currentPattern.tileGrid[x].row[y].isDangerous}";
                
                if (_currentPattern.tileGrid[x].row[y].isDangerous)
                {
                    ChangeTileState(tileTag, tileColor, GridManager.Instance.TileList[y][x]);
                }

            }

        }
    }

    private IEnumerator ShowFlash()
    {
        isFlashing = true;

        for (int x = 0; x < flashCount; x++)
        {
            ApplyPattern("Safe", flashColor);
            yield return new WaitForSeconds(flashDuration);

            ApplyPattern("Safe", Color.white);
            yield return new WaitForSeconds(flashDuration);

            if (x == flashCount - 1)
            {
                isDoneFlashing = true;
            }
        }

        isFlashing = false;
    }

    
}
