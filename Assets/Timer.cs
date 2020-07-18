using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField]
    Text timerText;

    float timeRemaining;

    bool running;

    void Start()
    {
         timeRemaining = 60;

         timeRemaining -= Time.deltaTime;
         float minutes = Mathf.FloorToInt(timeRemaining / 60);
         float seconds = Mathf.FloorToInt(timeRemaining % 60);
         timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

         running = true;
    }

    void Update()
    {
        if (running)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                float minutes = Mathf.FloorToInt(timeRemaining / 60);
                float seconds = Mathf.FloorToInt(timeRemaining % 60);
                timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
            }
            else
            {
                running = false;
            }
        }

    }

    public void addToTime(float num)
    {
        timeRemaining += num;
    }

    public float getTimeLeft()
    {
        return timeRemaining;
    }
}