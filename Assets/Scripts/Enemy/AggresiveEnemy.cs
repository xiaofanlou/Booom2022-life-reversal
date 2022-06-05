using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AggresiveEnemy : Enemy
{

    bool isFollow;
    [HideInInspector] public Transform followTarget;
    float returnTimer = 3;

    public float attackRadius;

    public float returnRadius = 2; //radius for start to return when player exit
    bool isReturning;
    Vector3 starterPos;

    public bool isPatrolling;
    public float patrollRadius;

    //Patrol variables
    float patrollSin;
    float patrollTimer = 0.02f;

    [Header("Variables")]
    public float moveSpeed; //move speed
    public float followSpeed;
    public bool isDefaultFaceToLeft;
    bool isMove;
    bool isGround;

    public EnemyAttack enemyAttack;

    private BoxCollider2D footCollid;
    private bool canMove;

    // Start is called before the first frame update
    protected void Start()
    {
        canMove = true;
        isFollow = false;
        isReturning = false;
        starterPos = transform.position;
        animator = GetComponent<Animator>();
        footCollid = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    public void FixedUpdate()
    {   if (base.IsDead())
        {
            return;
        }

        if (GameManager.Instance.isInGame()) //Check pause and gameover
        {
            if (!canMove)
            {
                return; //Block move
            }
            Move();
        }
    }

    private new void Update()
    {
        base.Update();
        if (base.IsDead()) {
            GetComponent<CapsuleCollider2D>().enabled = false;
            return;
        }
        if (GameManager.Instance.isInGame())
        {
            if (!canMove)
            {
                return;
            }
            Rotation();
            Animation();
            
        }
    }
    public void Move() {
        isGround = CheckGround();

        if (!enemyAttack.isAttacking)
        {
            isMove = true;
        }
        if (isPatrolling && !isFollow && !isReturning)
        {
            PatrollSin();
            Patroll();
        }
    }

    private bool CheckGround()
    {
        if (!footCollid.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            return false;
        }
        return true;
    }


    public void Rotation()
    {
        int patrollX = isDefaultFaceToLeft ? 1 : -1;
        if (isPatrolling)
        {
            if (patrollTimer > 0) //Side of patroll
            {
                transform.localScale = new Vector3(-patrollX, 1, 1);
            }
            else //reverse
            {
                transform.localScale = new Vector3(patrollX, 1, 1);
            }
        }

        if (isFollow) //get target and check the difference between them
        {
            if (transform.position.x > followTarget.position.x)
            {
                transform.localScale = new Vector3(patrollX, 1, 1);
            }
            else
            {
                transform.localScale = new Vector3(-patrollX, 1, 1);
            }
        }

        if (isReturning) //side of stater position
        {
            if (transform.position.x > starterPos.x)
            {
                transform.localScale = new Vector3(patrollX, 1, 1);
            }
            else
            {
                transform.localScale = new Vector3(-patrollX, 1, 1);
            }
        }
    }




    public void Follow(Transform target) {
        followTarget = target; //set target to follow

        if (!isFollow)
        {
            isFollow = true;
            StartCoroutine(IFollow());
        }
    }


    public void Attack() {
        bool canAttack = enemyAttack.Attack();
        if (canAttack)
        {
            animator.SetTrigger("isAttack");
        }
    }

    //The patrol is made using a sinusoid
    void PatrollSin()
    {
        patrollSin += patrollTimer * moveSpeed;
        
        if (patrollSin >= 1)
        {
            patrollTimer = -0.02f;
        }
        else if (patrollSin <= -1)
        {
            patrollTimer = 0.02f;
        }
    }


    void Patroll()
    {
        float sin = patrollSin; //local sin varible
        float x = patrollRadius * sin + starterPos.x; //set x of enemy position

        float y = transform.position.y;
        float z = transform.position.z;
        
        transform.position = new Vector3(x, y, z); //Change position
    }


    public void Animation()
    {
        if (isMove)
        {
            animator.SetBool("isMove", true);
        }
        else
        { animator.SetBool("isMove", false);
        }
    }


    IEnumerator IFollow()
    {
        float timer = returnTimer; //timer to return after player leave

        while (isFollow)
        {
            if (Vector2.Distance(transform.position, followTarget.position) > returnRadius && timer <= 0) //if target leave
            {
                isFollow = false;
                followTarget = null;

                isReturning = true;

                patrollSin = 0;

                StartCoroutine(IReturnToStartPos()); //start returning
            }
            /*else if (!isGround) //if the target jumps over the gap  
            {
                isFollow = false;
                followTarget = null;

                isReturning = true;

                patrollSin = 0;

                StartCoroutine(IReturnToStartPos());
            }*/
            else
            {
                if (Vector2.Distance(transform.position, followTarget.position) > attackRadius && !enemyAttack.isAttacking)
                {
                    transform.position = Vector2.MoveTowards(transform.position, new Vector2(followTarget.position.x, transform.position.y), followSpeed * Time.deltaTime); //follow target
                }
                else //if target in attack radius
                {
                    
                    Attack(); //attack
                }
                timer -= Time.deltaTime;
            }
            yield return null;
        }

        yield return null;
    }

    IEnumerator IReturnToStartPos()
    {
        while (transform.position.x != starterPos.x)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(starterPos.x, transform.position.y), followSpeed * Time.deltaTime); //move to start pos
            

        yield return null;
        }

        isReturning = false;
        yield return null;
    }
}
