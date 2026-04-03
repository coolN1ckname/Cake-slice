using UnityEngine;
using UnityEngine.UI;

public class DamageFlash : MonoBehaviour
{
    public static DamageFlash Instance;

    private Image image;

    public float flashDuration = 0.3f;
    private float timer;

    private void Awake()
    {
        Instance = this;
        image = GetComponent<Image>();

        SetAlpha(0); 
    }

    private void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            SetAlpha(timer / flashDuration); 
        }
    }

    public void Flash()
    {
        timer = flashDuration;
        SetAlpha(1f);
    }

    private void SetAlpha(float value)
    {
        Color color = image.color;
        color.a = Mathf.Clamp01(value);
        image.color = color;
    }
}