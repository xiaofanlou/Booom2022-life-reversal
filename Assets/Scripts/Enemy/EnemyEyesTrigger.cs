using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEyesTrigger : MonoBehaviour
{
    public AggresiveEnemy aggresiveEnemy;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") //if that object tap is equal to Player
        {
            if (GameManager.Instance.isInGame()) //is game status is not equal to gameover
                aggresiveEnemy.Follow(collision.transform); //start follow object
        }
    }
}
