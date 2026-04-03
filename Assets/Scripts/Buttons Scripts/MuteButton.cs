using UnityEngine;

public class MuteButton : MonoBehaviour
{   
    public GameObject muteButton;
    public GameObject unmuteButton;
    public void Awake()
    {
        unmuteButton.SetActive(false);
    }
    public void MuteClick()
    {
        AudioListener.volume = 0;
        muteButton.SetActive(false);
        unmuteButton.SetActive(true);
        Debug.Log("Muted");
    }

    public void UnmuteClick()
    {
        AudioListener.volume = 1f;
        muteButton.SetActive(true);
        unmuteButton.SetActive(false);
        Debug.Log("Unmuted");
    }
}
