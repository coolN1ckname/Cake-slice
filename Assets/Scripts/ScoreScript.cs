using TMPro;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class ScoreScript : MonoBehaviour
{
    public int Oilscore = 0; // общий счёт масла
    public int chocoScore = 0; // общий счёт шоколада
    public TMP_Text oilScoreText;
    public TMP_Text chocoScoreText;
    public GameObject glow;

    public static ScoreScript Instance;

    private bool isFirstHint = true;


    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        oilScoreText.text = Oilscore.ToString();
        chocoScoreText.text = chocoScore.ToString();

        if (Oilscore >= 10 && isFirstHint)
        {
            ShopHint();
        }
    }

    public void AddScore(int value)
    {
        Oilscore += value;
    }

    public void TakeScore(int value)
    {
        Oilscore -= value;
    }

    public void AddChocolate(int value)
    {
        chocoScore += value;
    }

    public void TakeChocolate(int value)
    {
        chocoScore -= value;
    }

    public void ShopHint()
    {
        Time.timeScale = 0;
        isFirstHint = false;
    }
}
