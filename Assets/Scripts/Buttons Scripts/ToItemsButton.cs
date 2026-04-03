using UnityEngine;
using UnityEngine.UI;

public class ToItemsButton : MonoBehaviour
{
    public Button toItemsButton;
    public Button backButton;
    public GameObject sweets;
    public GameObject items;

    public AudioClip buttonSound;
    private AudioSource audioSource;


    public void Awake()
    {
        backButton.interactable = false;
        audioSource = Camera.main.GetComponentInChildren<AudioSource>();

    }
    public void ToItemsButtonClick()
    {   
        sweets.SetActive(false);
        items.SetActive(true);
        toItemsButton.interactable = false;
        backButton.interactable = true;
        
        audioSource.PlayOneShot(buttonSound);
    }

    public void ToSweetsButtonClick()
    {
        items.SetActive(false);
        sweets.SetActive(true);
        backButton.interactable = false;
        toItemsButton.interactable = true;

        audioSource.PlayOneShot(buttonSound);
    }
}
