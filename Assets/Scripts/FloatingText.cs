using TMPro;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    private float moveSpeed = 2f;
    private float lifeTime = 1f;
    private TMP_Text floatingText;
    private Camera mainCamera;

    private void Awake()
    {
        floatingText = GetComponentInChildren<TMP_Text>();
        mainCamera = Camera.main;
    }

    public void SetText(string value)
    {
        floatingText.text = value;
    }

    private void Update()
    {
        transform.position += Vector3.up * moveSpeed * Time.deltaTime;

        lifeTime -= Time.deltaTime;
        if(lifeTime <= 0)
        {
            Destroy(gameObject);
        }
    }
}
