using UnityEngine;
using TMPro;
using System.Runtime.CompilerServices;

public class TimerController : MonoBehaviour
{
    [SerializeField] private float timerCounter;
    [SerializeField] private int minutes;
    [SerializeField] private int seconds;
    [SerializeField] private TextMeshProUGUI text;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*
        timerCounter = Time.deltaTime;  
        minutes = Mathf.FloorToInt(timerCounter/60f);
        seconds = Mathf.FloorToInt(timerCounter - minutes * 60);
        text.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        */
    }
}
