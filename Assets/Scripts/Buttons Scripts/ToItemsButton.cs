using UnityEngine;
using UnityEngine.UI;

public class ToItemsToggle : MonoBehaviour
{
    public Toggle sweetsToggle;  // Toggle для раздела "Сладости"
    public Toggle itemsToggle;   // Toggle для раздела "Товары"
    public GameObject sweets;
    public GameObject items;

    public AudioClip buttonSound;
    private AudioSource audioSource;

    public void Awake()
    {
        audioSource = Camera.main.GetComponentInChildren<AudioSource>();
        
        sweetsToggle.onValueChanged.AddListener(OnSweetsToggleChanged);
        itemsToggle.onValueChanged.AddListener(OnItemsToggleChanged);

        sweets.SetActive(true);
        items.SetActive(false);
    }

    private void OnSweetsToggleChanged(bool isOn)
    {
        if (isOn)
        {
            sweets.SetActive(true);
            items.SetActive(false);
            
            if (itemsToggle.isOn) itemsToggle.isOn = false;
            
            audioSource.PlayOneShot(buttonSound);
        }
    }

    private void OnItemsToggleChanged(bool isOn)
    {
        if (isOn)
        {
            items.SetActive(true);
            sweets.SetActive(false);
            
            if (sweetsToggle.isOn) sweetsToggle.isOn = false;
            
            audioSource.PlayOneShot(buttonSound);
        }
    }
}