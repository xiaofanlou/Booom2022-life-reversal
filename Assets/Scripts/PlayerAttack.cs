using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public int damage;
    private Animator anim;
    private PolygonCollider2D collider2D;

    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponentInParent<Animator>();
        collider2D = GetComponent<PolygonCollider2D>();
    }
   

    void Attack() {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                ClipManager.Instance.Attack();
                enemy.TakeDamage(damage);
            }
            if (enemy.IsDead())
            {
                return;
            }

            Animator[] hitAnimators = other.GetComponentsInChildren<Animator>();
            foreach (Animator anim in hitAnimators)
            {
                anim.SetTrigger("isHitted");
            }
        }
    }

}
