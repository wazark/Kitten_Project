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
    public float speedSwimming;
    //public float buffSpeed;
    public float jumpHeight;    
    public float addHeightFlight;
    public float swimmingImpulse;
    public float attackSpeed;
    /*public float currentHealth;
    public float maxHealth;
    public float currentDamage;
    public float baseDamage;
    public float maxDamage;
     */
    [Header("Gravity Settings")]
    public float gravityFlying;
    public float gravitySwimming;
    
    [Header("Player Shoot Ball")]
    public GameObject prefabBall;
    public Transform spawnBallLocation;
    public float speedBall;

    [Header("Colliders")]
    public Collider2D hammerHit;
    public Collider2D colliderDefault;
    public Collider2D colliderFlying;
    public Collider2D colliderSwimming;

    [Header("Camera Settings")]
    public Camera mainCamera;
    public Transform leftCameraLimit;
    public Transform rightCameraLimit;    
    public float cameraSpeedMove;
    

    private bool isLookLeft;
    private bool isGrounded;    
    private bool isAttacking;
    private bool isFlying;
    private bool isSwimming;
    private bool isWaterSurface;    
    private float directionMovement;
    private float gravityBase;

    void Start()
    {
     //Colliders
       hammerHit.enabled = false;
       colliderDefault.enabled = true;
       colliderFlying.enabled = false;
       colliderSwimming.enabled = false;

     // Get Components
       playerRB = GetComponent<Rigidbody2D>();
       playerAnimator = GetComponent<Animator>();

     //Gravity
       gravityBase = playerRB.gravityScale;
    }

    void Update()
    {        
        checkPlayerController();
        updateAnimator();
        updateColliders();
        cameraPositionControll();
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

    
    
    void OnTriggerEnter2D(Collider2D col) 
    {
        switch(col.gameObject.tag)
        {            
            case "Water":
            isSwimming = true;
            //playerRB.velocity = new Vector2 (0,0);
            playerRB.gravityScale = gravitySwimming;
            print("mudar animação para nadando");
            break;            
        }

    }
    void OnTriggerExit2D(Collider2D coll) 
    {
       switch(coll.gameObject.tag)
       {
        case "Water":
        isSwimming = false;
        playerRB.gravityScale = gravityBase;
        break;
       } 
    }
    

    void checkPlayerController()
    {
        if(isSwimming == false)
        {
        playerMovement();        
        playerAttacks();
        }
        else if (isSwimming == true)
        {
          playerMovementSwimming();
        }
    }

    void updateColliders()
    {
    if( isFlying == false && isSwimming == false && colliderDefault.enabled == false ) 
        {
            colliderDefault.enabled = true;
            colliderFlying.enabled = false;
            colliderSwimming.enabled = false;
        }     
    else if( isFlying == true && colliderFlying.enabled == false ) 
        {
            colliderFlying.enabled = true;
            colliderDefault.enabled = false;
            colliderSwimming.enabled = false;
        }
    else if(isSwimming == true && colliderSwimming.enabled == false)
        {
            colliderSwimming.enabled = true;
            colliderDefault.enabled = false;
            colliderFlying.enabled = false;
        }     
     
    }

    void cameraPositionControll()
    {
        if (mainCamera.transform.position.x > leftCameraLimit.position.x && mainCamera.transform.position.x < rightCameraLimit.position.x)
        {
            moveCamera();
        }
        else if (mainCamera.transform.position.x <= leftCameraLimit.position.x && transform.position.x > leftCameraLimit.position.x)
        {
            moveCamera();
        }
        else if (mainCamera.transform.position.x >= rightCameraLimit.position.x && transform.position.x < rightCameraLimit.position.x)
        {
            moveCamera();
        }
    }

    void moveCamera()
    {
        Vector3 newCameraPosition = new Vector3(this.transform.position.x, mainCamera.transform.position.y, -10f);
        mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, newCameraPosition, cameraSpeedMove * Time.deltaTime);
    }
    

    void playerMovement()
    {
        directionMovement = Input.GetAxisRaw("Horizontal");
        
        if(directionMovement < 0 && isLookLeft == false) 
        {
            flipCharacter();            
        }
        else if(directionMovement > 0 && isLookLeft == true) 
        {
            flipCharacter();          
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

    void playerMovementSwimming()
    {
        directionMovement = Input.GetAxisRaw("Horizontal");
        
        if(directionMovement < 0 && isLookLeft == false) 
        {
            flipCharacter();            
        }
        else if(directionMovement > 0 && isLookLeft == true) 
        {
            flipCharacter();          
        }

        playerJumpOnWater();

        playerRB.velocity = new Vector2 ( directionMovement * speedSwimming, playerRB.velocity.y );
    }
    void playerJumpOnWater()
    {
        if(Input.GetButtonDown("Jump") && isWaterSurface == false)
        {
            playerRB.AddForce(new Vector2(0,swimmingImpulse));
            //print("pulo no fundo");
        }
        else if(Input.GetButtonDown("Jump") && isWaterSurface == true)
        {
           playerRB.AddForce(new Vector2(0, jumpHeight)); 
           print("pulo na superficie");
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
        playerAnimator.SetBool("isSwimming",isSwimming);      
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

}