using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    
    public void LoadSceneOne()
    {

        SceneManager.LoadScene(1);
    }

    public void LoadStageOne()
    {

        SceneManager.LoadScene(2);
    }


    public void LoadStageTwo()
    {

        SceneManager.LoadScene(4);
    }


    public void Quit()
    {

        Application.Quit();
    }
}
