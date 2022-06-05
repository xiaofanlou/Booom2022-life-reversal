using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushedBox : MonoBehaviour
{

    private BoxCollider2D collider2D;
    private Rigidbody2D rigidbody2D;
    public int velocity;
    // Start is called before the first frame update
    void Start()
    {
        collider2D = GetComponent<BoxCollider2D>();
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        collision.otherRigidbody.velocity = new Vector2(0, 0);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !(collision.collider is BoxCollider2D))
        {
            Vector2 playerPosition = collision.collider.transform.position;
            Vector2 boxPosition = collision.otherCollider.transform.position;

            Debug.Log(collision.gameObject.GetComponent<PlayerController>().IsOnGround());
            if (!collision.gameObject.GetComponent<PlayerController>().IsOnGround())
            {
                Debug.Log("can not push");
                return;
            }

            int direction;
            if (playerPosition.x < boxPosition.x)
            {
                direction = 1;
            }
            else {
                direction = -1;
            }

            collision.otherRigidbody.velocity = new Vector2(direction * velocity, 0);
        }
    }
}
