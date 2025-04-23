using UnityEngine;
using TMPro;
using System.Runtime.CompilerServices;

public class TimerController : MonoBehaviour
{
    [SerializeField] private float timerCounter = 15f;
    [SerializeField] private int minutes;
    [SerializeField] public int seconds;
    [SerializeField] private TextMeshProUGUI text;

    private bool timerActive = false;

    public float TimerCounter => timerCounter;

    void Start()
    {
        minutes = Mathf.FloorToInt(timerCounter / 60f);
        seconds = Mathf.FloorToInt(timerCounter % 60f);
        text.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void Update()
    {
        if (timerActive && timerCounter > 0f)
        {
            timerCounter -= Time.deltaTime;
            timerCounter = Mathf.Max(timerCounter, 0f);

            minutes = Mathf.FloorToInt(timerCounter / 60f);
            seconds = Mathf.FloorToInt(timerCounter % 60f);

            text.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }

    public void ResetTimer(float seconds)
    {
        timerCounter = seconds;
    }

    public void AddTime(float seconds)
    {
        timerCounter += seconds;
    }

    public void StartTimer()
    {
        timerActive = true;
    }
}
