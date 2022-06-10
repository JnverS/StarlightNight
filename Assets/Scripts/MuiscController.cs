using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MuiscController : MonoBehaviour
{
    public static MuiscController instance;
    private AudioSource music = null;
    [SerializeField] private AudioClip gameMusic;
    [SerializeField] private AudioClip menuMusic;

    private void Awake()
    {
        if (music == null)
            music = GetComponent<AudioSource>();
        if (instance != null)
            Destroy(gameObject);
        else
        {
            instance = this;
            DontDestroyOnLoad(transform.gameObject);
        }
        PlayMenuMusic();
    }

    public void PlayGameMusic()
    {
        if (music.clip == gameMusic)
            return;
        music.clip = gameMusic;
        music.Play();
    }    
    public void PlayMenuMusic()
    {
        if (music.clip == menuMusic)
            return;
        music.clip = menuMusic;
        music.Play();
    }
}
