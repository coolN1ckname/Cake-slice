#nullable enable
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemButton : MonoBehaviour
{
    public int price;
    public int currency; // 1 - Масло, 2 - Шоколад
    public string itemName = null!;
    public ShopScript shop = null!;
    public TMP_Text? oilPriceTag;
    public TMP_Text? chocoPriceTag;

    private Button button = null!;
    public GameObject? itemPrefab;
    public Spawner spawner = null!;
    public EventManagerScript? eventManager; //ТОлько для шоколадопада
    private bool isBought = false;
    public bool isSpawnable;


    public AudioSource audioSource;
    public AudioClip buySound;

    private void Awake()
    {
        button = GetComponent<Button>();
    }

    public void Update()
    {
        int oilScore = ScoreScript.Instance.Oilscore;
        int chocoScore = ScoreScript.Instance.chocoScore;
        if (!isBought && oilPriceTag != null) 
        {
            oilPriceTag.text = oilScore + " / " + price;
        }
        else if (!isBought && chocoPriceTag != null)
        {
            chocoPriceTag.text = chocoScore + " / " + price;
        }
        else if (isBought && oilPriceTag != null)
        {
            oilPriceTag.text = "КУПЛЕНО!";
        }
        else if (isBought && chocoPriceTag != null)
        {
            chocoPriceTag.text = "КУПЛЕНО!";
        }
    }
    public void Buy()
    {
        if(shop.TryBuy(price, itemName, currency))
        {
            isBought = true;
            button.interactable = false;
            if (isSpawnable)
            {
                if (itemName != "Шоколад")
                spawner.UnlockItem(itemPrefab);
                else
                {
                    spawner.isCholocateBought = true;
                }
            }
            else if (itemName == "Шоколадопад" && eventManager != null)
            {
                eventManager.UnlockChocolateRain();
            }
            else if (itemName == "Улучшенный спавнер")
            {
                Debug.Log("Спавнер улучшен");
                spawner.SpawnerUpgrade();
            }
            audioSource.PlayOneShot(buySound);
        }
        
    }
}
