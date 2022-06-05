using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : HasDamage
{
    public int health;
    private float delayDestroySeconds;
    protected Animator animator;
    private bool isDead;
    private bool triggeredDead;
    // Start is called before the first frame update
    private void Awake()
    {
        delayDestroySeconds = 0.4f;
        triggeredDead = false;
    }

    void Start()
    {
       
        animator = GetComponent<Animator>();
        isDead = false;
    }

    // Update is called once per frame
    protected void Update()
    {
        if (health <= 0 && !isDead) {
            isDead = true;
        }
        if (isDead && !triggeredDead)
        {
            StartCoroutine(DelayDestroyed());
            triggeredDead = true;
        }   
    }

    public void TakeDamage(int damage) {
        if (health > 0) {
            health -= damage;
        }
        if (health <= 0)
        {
            animator.SetTrigger("isDead");
        }
    }

    public IEnumerator DelayDestroyed()
    {
        yield return new WaitForSeconds(delayDestroySeconds);
        if (gameObject != null) {
            DestroyMe();
        }
    }


    public void DestroyMe()
    {
        Destroy(gameObject);
    }

    public bool IsDead()
    {
        return isDead;
    }
}
