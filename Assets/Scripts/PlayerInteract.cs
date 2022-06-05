using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    private Animator anim;
    private CapsuleCollider2D collider2D;

    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponentInParent<Animator>();
        collider2D = GetComponent<CapsuleCollider2D>();
    }


    void StartInteract()
    {
        collider2D.enabled = true;

    }

    void EndInteract() {
        collider2D.enabled = false;
    }
    
}
