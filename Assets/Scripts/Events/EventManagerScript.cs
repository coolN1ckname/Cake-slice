using UnityEngine;

public class EventManagerScript : MonoBehaviour
{
    public ChocoladeFall chocoladeFall;

    private void Awake()
    {
        chocoladeFall.enabled = false;
    }

    public void UnlockChocolateRain()
    {
        chocoladeFall.enabled = true;
    }
}
