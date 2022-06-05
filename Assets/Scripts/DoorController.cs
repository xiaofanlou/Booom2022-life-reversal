using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public Transform targetTransform;
  

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("playerInteraction"))
        {
            Debug.Log("door trigger " + collision.gameObject.name);
            GameObject.FindGameObjectWithTag("Player").transform.position = targetTransform.position;
        }
    }
}
