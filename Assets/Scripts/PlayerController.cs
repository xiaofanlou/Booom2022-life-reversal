using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class PlayerController : MonoBehaviour
{

    public int moveSpeed;
    public int jumpForce;
    public int highJumpForce;
    public int damage;
    public Transform xAxisStart;
    public int fallingSpeed;
    public float attackingComboMemoryTime;
    
    private Animator animator;
    private bool isMoveLeft;

    private bool isMoveRight;
    private Rigidbody2D rigid2D;
    private BoxCollider2D boxCollider2D;
    private bool isOnGround;
    public PolygonCollider2D attackCollider2D;
    public Transform deadPoint;
    private CapsuleCollider2D interactColli;

    private bool isHighJumped;

    private PlayerHealth playerHealth;

    private float gravityScale;


    private bool isAttacking;
    private int attackingCombo;
    private float curAttackingMemoryTime;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        rigid2D = GetComponent<Rigidbody2D>();
        isMoveLeft = false;
        isMoveRight = false;
        isHighJumped = false;
        attackCollider2D = GetComponentInChildren<PolygonCollider2D>();
        interactColli = GetComponentsInChildren<CapsuleCollider2D>()[1];
        playerHealth = GetComponentInChildren<PlayerHealth>();
        EventManager.playerGainLog += PlayerGained;
        gravityScale = rigid2D.gravityScale;
        attackingCombo = 0;
        isAttacking = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < deadPoint.position.y)
        {
            setDead();
        }

        TryGround();
        TryMove();

        if (GameManager.Instance.IsGravityInversed())
        {
            //Debug.Log(transform.rotation.x + "," + transform.rotation.y + "," + transform.rotation.z);
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 180);
            gravityScale = -1;
            if (rigid2D.gravityScale > 0)
            {
                rigid2D.gravityScale = 0 - rigid2D.gravityScale;
            }
        }
        else
        {
            gravityScale = 1;
        }

        if (curAttackingMemoryTime >= 0)
        {
            curAttackingMemoryTime -= Time.deltaTime;
        }
        else {
            attackingCombo = 0;
        }
    }

    public bool IsOnGround()
    {
        return isOnGround;
    }

    public void setDead() {
        
        GameManager.Instance.GameOver();
        UIManager.Instance.ChangeScreenState(UIManager.ScreenState.Lose);
    }

    private void TryGround() {
        if (boxCollider2D.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            animator.SetBool("isGround", true);
            isOnGround = true;

            if (rigid2D.velocity.y < Double.Epsilon)
            {
                animator.SetBool("isJump", false);
                isHighJumped = false;
            }
        }
        else {

            animator.SetBool("isGround", false);
            
            isOnGround = false;
        }
    }

    private void TryMove() {
        if (isGameOver())
        {
            return;
        }
        if (transform.position.x <= xAxisStart.position.x && isMoveLeft)
        {
            return;
        }
        bool isFalling = false;
        if (!isOnGround) {
            isFalling = rigid2D.velocity.y < 0;
        }

        if (isMoveRight)
        {

            if (!isOnGround)
            {
                transform.position = transform.position + new Vector3(gravityScale * moveSpeed * Time.deltaTime, 0);
                return;
            }
            else
            {
                transform.position = transform.position + new Vector3(gravityScale * moveSpeed * Time.deltaTime, 0);
            }
            isAttacking = false;
            return;
        }

        if (isMoveLeft)
        {


            if (!isOnGround)
            {
                transform.position = transform.position - new Vector3(gravityScale * moveSpeed * Time.deltaTime, 0);
                return;
            }
            else
            {
                transform.position = transform.position - new Vector3(gravityScale * moveSpeed * Time.deltaTime, 0);
            }

            isAttacking = false;
            return;
        }
        

    }

    public void OnAttack(InputAction.CallbackContext callback) {
        if (isGameOver())
        {
            return;
        }
        switch (callback.phase)
        {
            case InputActionPhase.Started:
                if (!isAttacking)
                {
                    animator.SetTrigger("isAttack");
                    animator.SetInteger("attackCombo", attackingCombo);
                    isAttacking = true;


                    if (attackingCombo == 0 || curAttackingMemoryTime >= 0)
                    {
                        attackingCombo = (attackingCombo + 1) % 3;
                    }
                }

                break;
        }
    }

    public void TryAttack() {
        attackCollider2D.enabled = true;
    }

    public void AttackEnded() {
        attackCollider2D.enabled = false;
        isAttacking = false;
        curAttackingMemoryTime = attackingComboMemoryTime;
    }

    public void OnLeftMove(InputAction.CallbackContext callback)
    {
        if (isGameOver())
        {
            return;
        }

        switch (callback.phase)
        {
            case InputActionPhase.Started:

                //transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, 180, transform.rotation.eulerAngles.z);

                transform.localScale = new Vector3(-1, 1, 1);
                animator.SetBool("isMove", true);
                isMoveLeft = true;
                ClipManager.Instance.PlayMove();
                break;
            case InputActionPhase.Canceled:
                isMoveLeft = false;
                if (!isMoveRight)
                {
                    animator.SetBool("isMove", false);
                }
                ClipManager.Instance.Stop();
                //transform.rotation = Quaternion.Euler(transform.rotation.x, 0, transform.rotation.z);
                break;
        }
    }

    public void OnRightMove(InputAction.CallbackContext callback)
    {
        if (isGameOver())
        {
            return;
        }

        switch (callback.phase)
        {
            case InputActionPhase.Started:
                animator.SetBool("isMove", true);
                transform.localScale = new Vector3(1, 1, 1);
                ClipManager.Instance.PlayMove();
                isMoveRight = true;
                break;
            case InputActionPhase.Canceled:
                isMoveRight = false;
                if (!isMoveLeft)
                {
                    animator.SetBool("isMove", false);

                }
                ClipManager.Instance.Stop();
                break;
        }
    }

    public void OnJump(InputAction.CallbackContext callback)
    {
        if (isGameOver())
        {
            return;
        }
        
        switch (callback.phase)
        {
            case InputActionPhase.Started:
                if (isOnGround && callback.interaction is HoldInteraction) {
                    rigid2D.AddForce(new Vector2(0, gravityScale * jumpForce));
                    animator.SetBool("isJump", true);
                    ClipManager.Instance.Jump();
                }
                break;
            case InputActionPhase.Performed:
                if (!isHighJumped && callback.interaction is HoldInteraction)
                {
                    rigid2D.AddForce(new Vector2(0, gravityScale * highJumpForce));
                    animator.SetBool("isJump", true);
                    isHighJumped = true;
                }
                break;
            case InputActionPhase.Canceled:
                
                break;
        }
    }

    public void OnInteract(InputAction.CallbackContext callback)
    {
        if (isGameOver())
        {
            return;
        }
        switch (callback.phase)
        {
            case InputActionPhase.Started:
                if (isOnGround)
                {
                    animator.SetTrigger("isInteract");
                }
                break;
            case InputActionPhase.Canceled:

                break;
        }
    }


    public void StartInteract()
    {
        Debug.Log(interactColli.name);
        interactColli.enabled = true;
        
    }

    public void EndInteract()
    {
        interactColli.enabled = false;
    }
    

    private bool isGameOver() {
        return !GameManager.Instance.isInGame();
    }


    // 吃到抓手之后的奖励
    private void PlayerGained(GameObject target)
    {
        Debug.Log("event triggered " + target.tag);
        if (target.CompareTag("Cup")) {
            //transform.localScale = transform.localScale * 2;

            ClipManager.Instance.Reward();
            Destroy(target);
        }

    }

}
