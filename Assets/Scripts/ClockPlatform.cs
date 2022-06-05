using System.Collections.Generic;
using UnityEngine;

public class ClockPlatform : MonoBehaviour
{
    private Transform playerParent;
    private Dictionary<string, Transform> parentMap;
    private bool isRising;

    private Vector3 lastPosition;

    public int addForceRising;

    // Start is called before the first frame update
    void Start()
    {
        parentMap = new Dictionary<string, Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        lastPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }

    private void LateUpdate()
    {
        if (transform.position.y > lastPosition.y)
        {   isRising = true;
        }
        else {
            isRising = false;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!enableCollision(collision))
        { return;
        }

        if (!parentMap.ContainsKey(collision.collider.name))
        {
            parentMap.Add(collision.collider.name, collision.gameObject.transform.parent);
        }
        collision.gameObject.transform.parent = transform;
        


    }


    private bool enableCollision(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && collision.collider.name.Equals("Player"))
        {

            return true;
        }

        if (collision.gameObject.CompareTag("Box"))
        {
            return true;
        }

        return false;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {

        if (!enableCollision(collision))
        {
            return;
        }

        if (parentMap.ContainsKey(collision.collider.name))
        {
            collision.gameObject.transform.parent = parentMap[collision.gameObject.name];
            parentMap.Remove(collision.gameObject.name);
        }

        if (collision.gameObject.CompareTag("Player") && collision.collider.name.Equals("Player"))
        {
            //collision.gameObject.transform.parent = playerParent;
            Debug.Log("isRising " + isRising);


            if (isRising)
            {
                collision.rigidbody.AddRelativeForce(new Vector2(0, addForceRising));
            }
        }
    }
}
