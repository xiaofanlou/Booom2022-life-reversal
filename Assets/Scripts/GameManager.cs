using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private bool isGame;

    private bool isGravityInversed;

    // Start is called before the first frame update
    void Awake()
    {
        isGame = true;
        isGravityInversed = false;
        Time.timeScale = 1;
        SingletonInit();
    }

    void SingletonInit()
    {
        if (Instance != null)
            Destroy(gameObject);
        else
            Instance = this;
    }

    public bool isInGame()
    {
        return isGame;
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        isGame = false;
    }

    public bool IsGravityInversed()
    {
        return isGravityInversed;
    }

    public void inverseTheGravity()
    {
        isGravityInversed = !isGravityInversed;
    }
}
