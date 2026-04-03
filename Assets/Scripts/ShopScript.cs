
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;


public class ShopScript : MonoBehaviour
{

    public bool TryBuy(int price, string name, int currency)
    {
        int oilAmount = ScoreScript.Instance.Oilscore;
        int chocoAmount = ScoreScript.Instance.chocoScore;

        if (oilAmount >= price && currency == 1)
        {
            PurchaseObject(price, name, currency);
            return true;
        }
        else if (chocoAmount >= price && currency == 2)
        {
            PurchaseObject(price, name, currency);
            return true;
        }
        else
        {
            Debug.Log("Не хватает средств!");
            return false;
        }
    }
    public void PurchaseObject(int price, string name, int currency)
    {
        if ( currency == 1)
            ScoreScript.Instance.TakeScore(price);
        else
            ScoreScript.Instance.TakeChocolate(price);
        Debug.Log(name + " куплено");
        
        if (name == "Увеличитель стрика")
        {
            StreakScript.Instance.AddStreakTimer();
        }
    }
}
