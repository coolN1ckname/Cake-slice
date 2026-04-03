using UnityEngine;
using UnityEngine.UI;


public class TutorialScript : MonoBehaviour
{
    public Button ToShopButton;
    public Button buyButton;
    public Button backToGameButton;
    public GameObject hintPanel;

    public int oilRequired = 10;

    public Color colorA = Color.white;
    public Color colorB = Color.yellow;

    public float speed = 2f;

    private bool isGlowing = false;
    private Button currentButton;

    private TutorialState currentState = TutorialState.None;
    private bool tutorialCompleted = false;

    public enum TutorialState
    {
        None,
        GoToShop,
        BuyItem,
        ReturnToGame
    }

    private void Start()
    {
        // Проверяем, проходил ли игрок обучение
        //tutorialCompleted = PlayerPrefs.GetInt("TutorialDone", 0) == 1; Перманентное выключение обучения после первого раза
    }

    void Update()
    {
        if (tutorialCompleted) return;

        int currentOil = ScoreScript.Instance.Oilscore;

        if (currentOil >= oilRequired && currentState == TutorialState.None)
        {
            hintPanel.SetActive(true);
            currentState = TutorialState.GoToShop;
            StartGlow(ToShopButton);
        }

        AnimateCurrentButton();
    }

    void StartGlow(Button button)
    {
        currentButton = button;
        isGlowing = true;
    }

    void StopGlow(Button button)
    {
        isGlowing = false;

        var colors = button.colors;
        colors.normalColor = Color.white;
        colors.highlightedColor = Color.white;
        colors.selectedColor = Color.white;
        button.colors = colors;

        button.transform.localScale = Vector3.one;
    }

    void AnimateCurrentButton()
    {
        if (!isGlowing || currentButton == null) return;

        float t = Mathf.PingPong(Time.unscaledTime * speed, 1f);
        Color lerpedColor = Color.Lerp(colorA, colorB, t);

        ColorBlock colors = currentButton.colors;
        colors.normalColor = lerpedColor;
        colors.highlightedColor = lerpedColor;
        colors.selectedColor = lerpedColor;
        currentButton.colors = colors;

        float scale = 1f + Mathf.PingPong(Time.unscaledTime * 0.2f, 0.1f);
        currentButton.transform.localScale = Vector3.one * scale;
    }

    public void OnShopClicked()
    {
        if (currentState != TutorialState.GoToShop) return;

        StopGlow(ToShopButton);
        hintPanel.SetActive(false);
        currentState = TutorialState.BuyItem;
        StartGlow(buyButton);
    }

    public void OnBuyClicked()
    {
        if (currentState != TutorialState.BuyItem) return;

        StopGlow(buyButton);

        currentState = TutorialState.ReturnToGame;
        StartGlow(backToGameButton);
    }

    public void OnBackClicked()
    {
        if (currentState != TutorialState.ReturnToGame) return;

        StopGlow(backToGameButton);

        currentState = TutorialState.None;

        // Отмечаем, что обучение пройдено
        tutorialCompleted = true;
        PlayerPrefs.SetInt("TutorialDone", 1);
        PlayerPrefs.Save();
    }
}