using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilGhost : AggresiveEnemy
{
    private PolygonCollider2D attackCollid;

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        attackCollid = GetComponentInChildren<PolygonCollider2D>();
        attackCollid.enabled = false;
    }


    public void AttackStart() {

        attackCollid.enabled = true;
    }


    public void AttackEnd()
    {

        attackCollid.enabled = false;
    }
}
