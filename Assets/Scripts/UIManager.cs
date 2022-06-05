using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public enum ScreenState {game, Pause, Lose }

    public static UIManager Instance;

    [Header("Components")]
    public GameObject pauseScreen, loseScreen;

    public ScreenState screenState;


    void SingletonInit()
    {
        if (Instance != null)
            Destroy(gameObject);
        else
            Instance = this;
    }

    void Awake()
    {
        SingletonInit();
    }

    public void ChangeScreenState(ScreenState state) {
        switch (state)
        {
            case ScreenState.Pause:
                pauseScreen.SetActive(true);
                loseScreen.SetActive(false);
                Time.timeScale = 0;
                break;
            case ScreenState.Lose:
                loseScreen.SetActive(true);
                pauseScreen.SetActive(false);
                break;
            case ScreenState.game:
                loseScreen.SetActive(false);
                pauseScreen.SetActive(false);
                break;

        }
    }

}
