using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClipManager : MonoBehaviour
{
    public static ClipManager Instance;


    [Header("Components")]
    public AudioSource clipSource;
    public AudioClip move;
    public AudioClip jump;
    public AudioClip interact;
    public AudioClip attack;
    public AudioClip hitted;
    public AudioClip death;
    public AudioClip reward;

    // Start is called before the first frame update
    void Awake()
    {
        SingletonInit();
    }

    private void Start()
    {
    }


    void SingletonInit()
    {
        if (Instance != null)
            Destroy(gameObject);
        else
            Instance = this;
    }

    public void PlayMove()
    {
        if (move != null)
        {
            clipSource.clip = move;
            clipSource.loop = true;
            clipSource.Play();
        }
    }

    public void Jump()
    {

        if (jump != null)
        {
            clipSource.clip = jump;
            clipSource.loop = false;
            clipSource.Play();
        }
    }

    public void Hitted()
    {
        if (hitted != null)
        {
            clipSource.clip = hitted;
            clipSource.loop = false;
            clipSource.Play();
        }

    }

    public void Reward()
    {
        if (reward != null)
        {
            clipSource.clip = reward;
            clipSource.Play();
        }
    }

    public void Attack()
    {
        if (attack != null)
        {
            clipSource.clip = attack;
            clipSource.loop = false;
            clipSource.Play();
        }
    }


    public void Interact()
    {
        if (interact != null)
        {
            clipSource.clip = interact;
            clipSource.loop = false;
            clipSource.Play();
        }

    }

    public void Stop()
    {
        clipSource.loop = false;
        clipSource.Stop();
    }

}
