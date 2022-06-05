using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandCup : MonoBehaviour
{
    public Transform leftTopCornor;

    public Transform rightBottomCornor;

    public float speed;

    private Vector2 nextPosition;

    // Start is called before the first frame update
    void Start()
    {
        nextPosition = getRandomPosition();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, nextPosition) < 0.1)
        {
            nextPosition = getRandomPosition();
            return;  
        }

        transform.position = Vector3.MoveTowards(transform.position, nextPosition, speed * Time.deltaTime);
        
    }

    private Vector2 getRandomPosition()
    {
        return new Vector2(Random.Range(leftTopCornor.position.x, rightBottomCornor.position.x),
            Random.Range(rightBottomCornor.position.y, leftTopCornor.position.y));

    }


    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("PlayerHealth"))
        {
            EventManager.PlayerGained(gameObject);
        }
    }
}
