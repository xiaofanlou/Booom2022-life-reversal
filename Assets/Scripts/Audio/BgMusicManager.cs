using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgMusicManager : MonoBehaviour
{
    public static BgMusicManager Instance;

    public AudioSource bgSource;
    public AudioClip preMusic;
    public AudioClip bgMusic;

    // Start is called before the first frame update
    void Awake()
    {
        SingletonInit();
    }

    private void Start()
    {   if (preMusic == null)
        {

            PlayMusic();
        }
        else
        {
            PlayPreMusic();
        }
    }

    private void Update()
    {
        if (bgSource.isPlaying) {
            return;
        }
        PlayMusic();
        
    }


    void SingletonInit()
    {
        if (Instance != null)
            Destroy(gameObject);
        else
            Instance = this;
    }


    public void PlayMusic()
    {
        bgSource.clip = bgMusic;
        bgSource.loop = true;
        bgSource.Play();
    }



    public void PlayPreMusic()
    {
        bgSource.clip = preMusic;
        bgSource.loop = false;
        bgSource.Play();

    }
}
