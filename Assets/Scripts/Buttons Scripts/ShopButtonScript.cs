using UnityEngine;

public class ShopButtonScript : MonoBehaviour
{
    public GameObject shop;
    public GameObject toTheShopButton;
    public GameObject toTheGameButton;
    public GameObject knife;
    public AudioClip buttonSound;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = Camera.main.GetComponentInChildren<AudioSource>();
    }
    private void Update()
    {
           
    }

    public void ToTheShopButtonClick()
    {
        Debug.Log("кнопка нажата");
        shop.SetActive(true);
        toTheShopButton.SetActive(false);
        toTheGameButton.SetActive(true);
        knife.SetActive(false);
        Time.timeScale = 0;
        audioSource.PlayOneShot(buttonSound);
    }

    public void ToTheGameButtonClick()
    {
        shop.SetActive(false);
        toTheGameButton.SetActive(false);
        toTheShopButton.SetActive(true);
        knife.SetActive(true);
        Time.timeScale = 1;
        audioSource.PlayOneShot(buttonSound);
    }
}
