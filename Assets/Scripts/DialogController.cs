using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogController : MonoBehaviour
{
    public GameObject dialog;
    public string targetText;

    public Text dialogText;
    
    // Start is called before the first frame update
    void Start()
    {
        dialog.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.tag);
        if (collision.CompareTag("PlayerHealth"))
        {
            dialogText.text = targetText;
            dialog.SetActive(true);

        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.CompareTag("PlayerHealth"))
        {
            dialog.SetActive(false);
        }
    }
}
