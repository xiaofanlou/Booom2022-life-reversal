using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{

    public void restartGameScene()
    {
        Time.timeScale = 1;
        Debug.Log(SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        
    }

    public void BackToMenu() {
        SceneManager.LoadScene(0);
    }


    public void QuitGame()
    {
        Application.Quit();
    }

    public void ContinueGame()
    {
        Time.timeScale = 1;
        UIManager.Instance.ChangeScreenState(UIManager.ScreenState.game);
    }

    public void PauseGame(InputAction.CallbackContext callback) {
        Debug.Log("game paused");


        switch (callback.phase)
        {
            case InputActionPhase.Started:
                UIManager.Instance.ChangeScreenState(UIManager.ScreenState.Pause);
                break;
           
        }
        
    }

}
