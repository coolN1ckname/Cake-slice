using System.Runtime.CompilerServices;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public AudioClip bgMusic;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = Camera.main.GetComponentInChildren<AudioSource>();
    }
    void Start()
    {
        audioSource.clip = bgMusic;
        audioSource.loop = true;
        audioSource.Play();
    }

}
