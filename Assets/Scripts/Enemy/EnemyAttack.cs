using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public bool isAttacking;

    public float attackCoolDownTime;

    private float currentCoolDownTime;

    // Start is called before the first frame update
    void Start()
    {
        isAttacking = false;
        currentCoolDownTime = 0;
    }

    private void Update()
    {
        if (isAttacking && currentCoolDownTime > 0)
        {
            currentCoolDownTime -= Time.deltaTime;
        }

        if (currentCoolDownTime <= 0)
        {
            isAttacking = false;
        }

        if (!isAttacking)
        {
            GetComponent<PolygonCollider2D>().enabled = false;
        }
    }

    public bool Attack() {
        if (!CanAttack())
        {
            return false;
        }

        isAttacking = true;
        currentCoolDownTime = attackCoolDownTime;
        return true;
    }
    

    private bool CanAttack() {
        return !isAttacking && currentCoolDownTime <= 0;
    }
}
