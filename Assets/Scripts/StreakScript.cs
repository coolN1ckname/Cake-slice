using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StreakScript : MonoBehaviour
{
    public static double streak = 0; // текущий стрик
    public static float streakTime = 0; // продолжительность текущего стрика
    public float streakTimer = 1f; // Таймер стрика
    public int streakForChocolate = 5; // Чтоб получить шоколадку

    public TMP_Text currentStreak;
    public TMP_Text streakTimeText;

    public GameObject streakBoxesParent; // Родительский объект "Streak Boxes"
    public Sprite activeBoxSprite;       // Спрайт для активной ячейки
    public Sprite inactiveBoxSprite;     // Спрайт для неактивной ячейки
    
    private Image[] streakBoxes;          // Массив всех Image компонентов у детей
    private int lastStreakValue = -1;     // Для отслеживания изменений

    public static StreakScript Instance;


    public void Awake()
    {
        Instance = this;
    }

    public void Start()
    {
        InitializeStreakBoxes();
    }
    
    public void Update()
    {
        currentStreak.text = "Серия: " + streak.ToString() + "/" + streakForChocolate;
        streakTimeText.text = streakTime.ToString();

        // Обновляем визуал Streak Boxes только если значение изменилось
        if ((int)streak != lastStreakValue)
        {
            UpdateStreakBoxes();
            lastStreakValue = (int)streak;
        }

        if (streak >= 1)
        {
            streakTime += Time.deltaTime;

            if (streakTime > streakTimer)
            {
                int chocolateAmount = (int)Math.Floor(streak / streakForChocolate);
                ScoreScript.Instance.AddChocolate(chocolateAmount);

                streakTime = 0;
                streak = 0;
                UpdateStreakBoxes(); // Обновляем при сбросе
                lastStreakValue = 0;
            }
        }
    }

    private void InitializeStreakBoxes()
    {
        if (streakBoxesParent == null)
        {
            Debug.LogWarning("Streak Boxes Parent не назначен в инспекторе!");
            return;
        }

        // Получаем все компоненты Image у дочерних объектов
        streakBoxes = streakBoxesParent.GetComponentsInChildren<Image>();
        
        // Исключаем самого родителя, если у него тоже есть Image
        if (streakBoxes.Length > 0 && streakBoxes[0].gameObject == streakBoxesParent)
        {
            // Убираем первый элемент, если это сам родитель
            Image[] temp = new Image[streakBoxes.Length - 1];
            System.Array.Copy(streakBoxes, 1, temp, 0, streakBoxes.Length - 1);
            streakBoxes = temp;
        }
        
        UpdateStreakBoxes();
    }

    private void UpdateStreakBoxes()
    {
        if (streakBoxes == null) return;
        
        int currentStreakInt = (int)streak;
        
        for (int i = 0; i < streakBoxes.Length; i++)
        {
            if (streakBoxes[i] == null) continue;
            
            // Если индекс меньше текущего стрика — показываем активный спрайт
            if (i < currentStreakInt)
            {
                streakBoxes[i].sprite = activeBoxSprite;
            }
            else
            {
                streakBoxes[i].sprite = inactiveBoxSprite;
            }
        }
    }

    public static void AddStreak()
    {
        streak += 1;
        
        // Обнуляем время при сборе первой сладости
        if (streak <= 1)
        {
            streakTime = 0;
        }
    }
    
    public static void BreakStreak()
    {
        streak = 0;
        if (Instance != null)
        {
            Instance.UpdateStreakBoxes();
            Instance.lastStreakValue = 0;
        }
    }

    public void AddStreakTimer()
    {
        streakTimer += 0.2f;
    }
}