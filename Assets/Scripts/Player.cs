using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D playerRB;
    private Animator playerAnimator;

    public Transform groundCheck;
    public LayerMask whatIsGround;

    [Header("Player Attributes")]
    public float movementSpeed;
    //public float buffSpeed;
    public float jumpHeight;
    /*public float maxAmountJump;
    public float currentHealth;
    public float maxHealth;
    public float currentDamage;
    public float baseDamage;
    public float maxDamage;
     */
    private bool isLookLeft;
    private bool isGrounded;
    private float directionMovement;

    void Start()
    {
       playerRB = GetComponent<Rigidbody2D>();
       playerAnimator = GetComponent<Animator>();
    }

    
    void Update()
    {
        playerMovement();
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

        playerRB.velocity = new Vector2 ( directionMovement * movementSpeed, playerRB.velocity.y );
    }
    
    void playerJump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded == true)
        {
            playerRB.AddForce(new Vector2(0, jumpHeight));
        }
    }

    void updateAnimator()
    {
        playerAnimator.SetInteger("direction", (int)directionMovement);
        playerAnimator.SetBool("isGrounded", isGrounded);
        playerAnimator.SetFloat("vectical", playerRB.velocity.y);
    }

    void flipCharacter()
    {
        isLookLeft = !isLookLeft;
        float x = transform.localScale.x;
        x *= -1;
        transform.localScale = new Vector3(x, transform.localScale.y, transform.localScale.z);
    }

}
