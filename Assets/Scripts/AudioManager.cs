using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioClip backgroundMusic;
    private AudioSource music;
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        music = GetComponent<AudioSource>();
        music.clip = backgroundMusic;
        music.Play();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Pause()
    {
        if ( gameManager.isPaused )
        {
            music.Pause();
        } else
        {
            music.UnPause();
        }
        
    }

    public void StopPlaying()
    {
        music.Stop();
    }

    public void OnChangeVolume(float value)
    {
        music.volume = value;
    }
}
