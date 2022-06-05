using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    private BoxCollider2D boxCollider2D;
    public Transform[] points;
    public int speed;
    public float delayTime;
    private int currentPosition;
    private int increment;
    public float waitTime;
    private float currentWaitTime;
    private Transform nextPoint;

    private float currentDelayTime;
    // Start is called before the first frame update
    void Start()
    {
        currentPosition = 0;
        boxCollider2D = GetComponent<BoxCollider2D>();
        currentWaitTime = waitTime;
        nextPoint = points[1];
        currentDelayTime = delayTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentDelayTime > 0)
        {
            currentDelayTime -= Time.deltaTime;
            return;
        }

        if (currentPosition == 0)
        {   increment = 1;
            if (currentWaitTime > Mathf.Epsilon)
            {
                currentWaitTime -= Time.deltaTime;
                return;
            }
        }
        if (currentPosition == points.Length - 1){
            increment = -1;
            if (currentWaitTime > Mathf.Epsilon)
            {
                currentWaitTime -= Time.deltaTime;
                return;
            }
        }
        
        nextPoint = points[currentPosition + increment];

       
        Vector2 positionAfterMove = Vector2.MoveTowards(transform.position, nextPoint.position, Time.deltaTime * speed);

        transform.position = positionAfterMove;
        if (Vector2.Distance(positionAfterMove, nextPoint.position) < Mathf.Epsilon) {
            currentPosition += increment;
            if (currentPosition == 0 || currentPosition == points.Length - 1) {
                currentWaitTime = waitTime;
            }
        }

    }
    
}
