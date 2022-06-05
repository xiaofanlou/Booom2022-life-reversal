using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTrap : MonoBehaviour
{
    public enum LaserDirection { Vertical, Horizontal }

    private LineRenderer lineRenderer;
    public Transform startPoint;
    public Transform endPoint;

    public LaserDirection directionType;
    private PlayerHealth playerHealth;

    

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponentInChildren<LineRenderer>();
        playerHealth = GameObject.FindGameObjectWithTag("PlayerHealth").GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {

        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, Vector2.Distance(startPoint.position, endPoint.position));
       
        lineRenderer.useWorldSpace = true;
        lineRenderer.SetPosition(0, startPoint.position);

        if (hit && (hit.collider.tag.Equals("Player") || hit.collider.tag.Equals("Box")))
        {
            Vector3 targetVec = startPoint.position;
                if (directionType == LaserDirection.Vertical)
                {
                    targetVec.y = hit.collider.transform.position.y;
                }
                if (directionType == LaserDirection.Horizontal)
                {
                    targetVec.x = hit.collider.transform.position.x;
                }
                

            lineRenderer.SetPosition(1, targetVec);
                if (hit.collider.tag.Equals("Player"))
                {
                    playerHealth.TakeDamaged(playerHealth.curHealth);
                }
        } else
        {
            lineRenderer.SetPosition(1, endPoint.position);

        }
    }
}
