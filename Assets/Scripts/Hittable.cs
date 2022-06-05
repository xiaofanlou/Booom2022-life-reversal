using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hittable : MonoBehaviour
{
    private Animator[] hitAnimators;
    // Start is called before the first frame update
    void Start()
    {
        hitAnimators = GetComponentsInChildren<Animator>();

    }
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.CompareTag("PlayerAttack"))
        {
            foreach (Animator anim in hitAnimators)
            {
                anim.SetTrigger("isHitted");
            }
        }
    }
}
