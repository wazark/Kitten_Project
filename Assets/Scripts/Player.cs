using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D playerRB;
    private Animator playerAnimator;

    public Transform[] groundChecks;
    public LayerMask whatIsGround;

    [Header("Player Attributes")]
    public float speedMove;
    //public float buffSpeed;
    public float jumpHeight;
    public float gravityFlying;
    public float addHeightFlight;
    public float attackSpeed;    
    /*public float currentHealth;
    public float maxHealth;
    public float currentDamage;
    public float baseDamage;
    public float maxDamage;
     */
    [Header("Player Animations")]
    public float delayToChangeIdleState;
    public float idleReturnToDefaul;

    [Header("Player Shoot Ball")]
    public GameObject prefabBall;
    public Transform spawnBallLocation;
    public float speedBall;

    [Header("Colliders")]
    public Collider2D hammerHit;
    public Collider2D colliderDefault;
    public Collider2D colliderFlying;
    //public Collider2D colliderswimming;

    private bool isIdleAnimChanged;
    private bool isLookLeft;
    private bool isGrounded;
    private bool isIdle;
    private bool isAttacking;
    private bool isFlying;
    private float directionMovement;
    private float gravityBase;
    

    void Start()
    {
     //Colliders
       hammerHit.enabled = false;
       colliderDefault.enabled = true;
       colliderFlying.enabled = false;

     // Get Components
       playerRB = GetComponent<Rigidbody2D>();
       playerAnimator = GetComponent<Animator>();

     //Gravity
       gravityBase = playerRB.gravityScale;
    }

    void Update()
    {
        playerMovement();

        updateAnimator();

        playerAttacks();

        StartCoroutine(changeIdleAnimation());

        updateColliders();
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapArea(groundChecks[0].position, groundChecks[1].position, whatIsGround);
        if(isGrounded == true && isFlying== true)
        {
            isFlying = false;
            playerRB.gravityScale = gravityBase;
        }
    }

    void updateColliders()
    {
     if( isFlying== true && colliderFlying.enabled== false ) 
        {
            colliderDefault.enabled = false;
            colliderFlying.enabled = true;
        }
     else if( isFlying == false && colliderDefault.enabled== false ) 
        {
            colliderDefault.enabled = true;
            colliderFlying.enabled = false;
        }
    }

    void playerMovement()
    {
        directionMovement = Input.GetAxisRaw("Horizontal");
        
        if(directionMovement < 0 && isLookLeft == false) 
        {
            flipCharacter();
            isIdle= false;            
        }
        else if(directionMovement > 0 && isLookLeft == true) 
        {
            flipCharacter();
            isIdle = false;           
        }
        else if( directionMovement == 0 )
        {
            isIdle = true;
        }

        playerJump();

        playerRB.velocity = new Vector2 ( directionMovement * speedMove, playerRB.velocity.y );
    }
    
    void playerJump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded == true)
        {
            playerRB.AddForce(new Vector2(0, jumpHeight));            
        }

        if( Input.GetButtonDown("Jump") && isGrounded == false && isFlying== false && isAttacking == false)
        {
            isFlying = true;            
            playerRB.velocity = new Vector2(playerRB.velocity.x, addHeightFlight);
            playerRB.gravityScale = gravityFlying;
        }
        if (Input.GetButtonUp("Jump"))
        {
            isFlying = false;            
            playerRB.gravityScale = gravityBase;
        }
    }

    void playerAttacks()
    {
        if(Input.GetButtonDown("Fire1") && isAttacking == false )
        {
            playerAnimator.SetTrigger("isAttackA");
            isAttacking= true;
            isFlying = false;
            playerRB.gravityScale = gravityBase;
        }
        else if( Input.GetButtonDown("Fire2") && isAttacking == false )
        {
            playerAnimator.SetTrigger("isAttackB");
            isAttacking = true;
            isFlying = false;
            playerRB.gravityScale = gravityBase;
        }
    }

    void onAttackComplete()
    {
        StartCoroutine(activeAttack());
        StartCoroutine(returnDefaultIdleAnimation());
    }

    public void shootBall()
    {
        GameObject temp = Instantiate(prefabBall, spawnBallLocation.position, transform.localRotation);        
        temp.GetComponent<Rigidbody2D>().velocity = new Vector2(speedBall, 0);           
    }
        
    void updateAnimator()
    {
        playerAnimator.SetInteger("direction", (int)directionMovement);
        playerAnimator.SetBool("isGrounded", isGrounded);
        playerAnimator.SetFloat("vectical", playerRB.velocity.y);
        playerAnimator.SetBool("isFlying", isFlying);
        playerAnimator.SetBool("isAttacking", isAttacking);
    }

    void flipCharacter()
    {
        isLookLeft = !isLookLeft;
        float x = transform.localScale.x;
        x *= -1;
        speedBall *= -1;
        transform.localScale = new Vector3(x, transform.localScale.y, transform.localScale.z);
    }
    IEnumerator activeAttack()
    {
        yield return new WaitForSecondsRealtime(attackSpeed);
        isAttacking = false;
    }
        
    IEnumerator changeIdleAnimation()
    {        
        if(isIdle == true && isIdleAnimChanged == false && isAttacking == false)
        {
            isIdleAnimChanged = true;
            yield return new WaitForSecondsRealtime(delayToChangeIdleState);            
            playerAnimator.SetBool("ChangeIdleAnim", true);            
        }
        else if( isIdle == false || isAttacking == true)
        {
            StartCoroutine(returnDefaultIdleAnimation());
        }
    }

    IEnumerator returnDefaultIdleAnimation()
    {
        playerAnimator.SetBool("ChangeIdleAnim", false);
        yield return new WaitForSecondsRealtime(idleReturnToDefaul);
        isIdleAnimChanged = false;
    }
        
}
