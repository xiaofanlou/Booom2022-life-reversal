using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth;
    public int curHealth;
    private Animator animator;
    public int flipForce;
    public GameObject player;
    public float flipSecond;
    public int blinkCount;
    private bool isPlayerProtected;

    private bool isDead;
    private float deadTriggerSeconds;
    // Start is called before the first frame update
    void Start()
    {
        HealthBar.maxHealth = maxHealth;

        HealthBar.currentHealth = curHealth;
        animator = GetComponentInParent<Animator>();
        isPlayerProtected = false;
        isDead = false;
        deadTriggerSeconds = 0.4f;
    }

    // Update is called once per frame
    void Update()
    {
        if (curHealth <= 0 && !isDead)
        {
            StartCoroutine(PlayerDead());
        }

    }

    public void TakeDamaged(int damage)
    {
        curHealth -= damage;
        HealthBar.currentHealth = curHealth;
        tryCheckDead();
        isPlayerProtected = true;
    }

    public void GetHealed()
    {
        curHealth = maxHealth;
        HealthBar.currentHealth = curHealth;
    }

    public void tryCheckDead() {
        if (curHealth <= 0) {
            animator.SetTrigger("isDead");
        }
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy") && !isPlayerProtected)
        {
            int damage = other.gameObject.GetComponent<HasDamage>().damage;
            int ratio;
            if (other.transform.position.x > transform.position.x)
            {
                ratio = -1;
            }
            else {
                ratio = 1;
            }
            GetComponentInParent<Rigidbody2D>().AddForce(new Vector2(ratio * flipForce, 0));
            TakeDamaged(damage);
            ClipManager.Instance.Hitted();
            StartCoroutine(PlayerHerted(blinkCount, flipSecond));
        }
    }

    private IEnumerator PlayerHerted(int blinkNum, float seconds)
    {
        Renderer renderer = player.GetComponent<Renderer>();
        for(int i = 0; i < blinkNum ; i++)
        {
            renderer.enabled = !renderer.enabled;
            yield return new WaitForSeconds(seconds);
        }

        renderer.enabled = true;
        isPlayerProtected = false;
    }



    private IEnumerator PlayerDead()
    {
        isDead = true;
        yield return new WaitForSeconds(deadTriggerSeconds);
        
        player.GetComponent<PlayerController>().setDead();
    }

}

