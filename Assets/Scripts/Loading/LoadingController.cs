using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingController : MonoBehaviour
{
    private Image image;
    public Text text;
    public Text continueText;

    public Sprite[] images;

    public string[] contents;

    public int contentSpeed;
    public float maxWaitTime;
    public int maxSize;
    public int nextScene;

    private int currentIndex;
    private int currentContentIndex;
    private float currentWaitTime;

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponentInChildren<Image>();
        continueText.enabled = false;
        currentIndex = 0;
        currentContentIndex = 0;
        currentWaitTime = maxWaitTime;
        image.sprite = images[0];
    }

    // Update is called once per frame
    void Update()
    {   if (currentIndex >= maxSize)
        {
            return;
        }

        bool filled = TryFillWithContent();

        if (filled) {
            Debug.Log("text filled");
            continueText.enabled = true;
            
        }
    }

    public void jumpToNext(InputAction.CallbackContext callback)
    {   if (!continueText.enabled)
        {
            return;
        }

        if (currentIndex >= maxSize)
        {
            SceneManager.LoadScene(nextScene);
            return;
        }
        currentIndex++;
        currentContentIndex = 0;
        currentWaitTime = maxWaitTime;
        image.sprite = images[currentIndex];
        text.text = "";
        continueText.enabled = false;
    }

    private bool TryFillWithContent()
    {
        int i;
        for (i = 0; i < contentSpeed; i++, currentContentIndex++)
        {
            if (currentContentIndex >= contents[currentIndex].Length)
            {
                return true;
            }
            text.text += contents[currentIndex][currentContentIndex];
        }
        return false;
    }
}
