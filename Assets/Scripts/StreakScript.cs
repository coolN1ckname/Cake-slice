using System;
using TMPro;
using UnityEngine;

public class StreakScript : MonoBehaviour
{
    public static double streak = 0; // текущий стрик
    public float streakTime = 0; // продолжительность текущего стрика
    public float streakTimer = 0.5f; // Таймер стрика

    public TMP_Text currentStreak;
    public TMP_Text streakTimeText;

    public static StreakScript Instance;


    public void Awake()
    {
        Instance = this;
    }
    
    public void Update()
    {
        currentStreak.text = streak.ToString();
        streakTimeText.text = streakTime.ToString();

        if (streak >= 1)
        {
            streakTime += Time.deltaTime;

            if (streakTime > streakTimer)
            {
                int chocolateAmount = (int)Math.Floor(streak / 3);
                ScoreScript.Instance.AddChocolate(chocolateAmount);

                streakTime = 0;
                streak = 0;
            }
        }
    }

    public static void AddStreak()
    {
        streak += 1;
    }
    public static void BreakStreak()
    {
        streak = 0;
    }

    public void AddStreakTimer()
    {
        streakTimer += 0.2f;
    }
}
