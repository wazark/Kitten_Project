using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Components
    private Rigidbody2D playerRB;
    private Animator playerAnimator;
    private GameController _gameController;
    
    [Header("Player Attributes")]
    public float speedMove;
    public float speedSwimming;    
    public float jumpHeight;    
    public float addHeightFlight;
    public float swimmingImpulse;
    public float attackSpeed;
    public float speedBall; 

    // Privates Variables
    private bool isLookLeft;
    private bool isGrounded;    
    private bool isAttacking;
    private bool isFlying;
    private bool isSwimming;
    private bool isWaterSurface;
    private bool isStartingFly;
    private float directionMovement;
    private float gravityBase;
    
    // Inicialização
    void Start()
    {
     // Inicializando GameController
       _gameController = FindObjectOfType(typeof(GameController)) as GameController;

     // Desabilitando Colliders Iniciais
       _gameController.hammerHit.enabled = false;
       

     // Get Componnets
       playerRB = GetComponent<Rigidbody2D>();
       playerAnimator = GetComponent<Animator>();

     // Definindo a gravidade base
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
        isGrounded = Physics2D.OverlapArea(_gameController.groundChecks[0].position, _gameController.groundChecks[1].position, _gameController.whatIsGround);
        if(isGrounded == true && isFlying== true)
        {
            isFlying = false;
            isStartingFly= false;
            playerRB.gravityScale = gravityBase;
        }
    }

    
    // Verificação de colisão
    void OnTriggerEnter2D(Collider2D col) 
    {
        switch(col.gameObject.tag)
        {            
            case "Water":
                isFlying = false;
                isStartingFly = false;
                isWaterSurface = false;
                if(isSwimming == false)
                {
                    playerRB.velocity = new Vector2(0, 0);
                }
            break;   
                
            case "UnderWater":                                    
                    isSwimming = true;
            break;

            case "WaterSurface":                
                isWaterSurface = true;
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

            case "WaterSurface":
                isWaterSurface = false;                
            break;
        } 
    }
    
    // Controle do Jogador
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

    // Atualização de colliders
    void updateColliders()
    {
    if( isSwimming == true) 
        {
            _gameController.capColliderDefault.direction = CapsuleDirection2D.Horizontal;
            _gameController.capColliderDefault.offset = _gameController.swimmingOffset;
            _gameController.capColliderDefault.size = _gameController.swimmingSize;
            playerRB.gravityScale = _gameController.gravitySwimming;            
        }     
    else if( isFlying == true) 
        {
            _gameController.capColliderDefault.direction = CapsuleDirection2D.Horizontal;
            _gameController.capColliderDefault.offset = _gameController.flyingOffset;
            _gameController.capColliderDefault.size = _gameController.flyingSize;
        }
    else if(isFlying == false && isSwimming == false)
        {
            _gameController.capColliderDefault.direction = CapsuleDirection2D.Vertical;
            _gameController.capColliderDefault.offset = _gameController.defaultOffset;
            _gameController.capColliderDefault.size = _gameController.defaultSize;
        }     
     
    }

    // Atualização de animação
    void updateAnimator()
    {
        playerAnimator.SetInteger("direction", (int)directionMovement);
        playerAnimator.SetBool("isGrounded", isGrounded);
        playerAnimator.SetFloat("vectical", playerRB.velocity.y);
        playerAnimator.SetBool("isFlying", isFlying);
        playerAnimator.SetBool("isAttacking", isAttacking);
        playerAnimator.SetBool("isSwimming", isSwimming);
    }

    // Controle de posição da Câmera
    void cameraPositionControll()
    {
        // Controla o limite máximo que a camera vai chegar nas laterias do game, movimenta a camera de acordo com a direção do player de forma mais suáve.
        if (_gameController.mainCamera.transform.position.x > _gameController.leftCameraLimit.position.x && _gameController.mainCamera.transform.position.x < _gameController.rightCameraLimit.position.x)
        {
            Vector3 newCameraPosition = new Vector3(this.transform.position.x, isSwimming ? _gameController.cameraMaxYPosition : _gameController.cameraDefaultYPosition, -10f);
            _gameController.mainCamera.transform.position = Vector3.Lerp(_gameController.mainCamera.transform.position, newCameraPosition, _gameController.cameraSpeedMove * Time.deltaTime);
        }
        else if (_gameController.mainCamera.transform.position.x <= _gameController.leftCameraLimit.position.x && transform.position.x > _gameController.leftCameraLimit.position.x)
        {
            Vector3 newCameraPosition = new Vector3(this.transform.position.x, isSwimming ? _gameController.cameraMaxYPosition : _gameController.cameraDefaultYPosition, -10f);
            _gameController.mainCamera.transform.position = Vector3.Lerp(_gameController.mainCamera.transform.position, newCameraPosition, _gameController.cameraSpeedMove * Time.deltaTime);
        }
        else if (_gameController.mainCamera.transform.position.x >= _gameController.rightCameraLimit.position.x && transform.position.x < _gameController.rightCameraLimit.position.x)
        {
            Vector3 newCameraPosition = new Vector3(this.transform.position.x, isSwimming ? _gameController.cameraMaxYPosition : _gameController.cameraDefaultYPosition, -10f);
            _gameController.mainCamera.transform.position = Vector3.Lerp(_gameController.mainCamera.transform.position, newCameraPosition, _gameController.cameraSpeedMove * Time.deltaTime);
        }

        // Controla o FOV da câmera aproximando ou distanciando dependendo do status do player.
        if (isSwimming == true)
        {
            _gameController.mainCamera.fieldOfView = Mathf.Lerp(_gameController.mainCamera.fieldOfView, _gameController.cameraSizeSwimming, Time.deltaTime * _gameController.cameraSmooth);
            _gameController.shadowEnvironment.SetActive(false);
        }
        else if (isFlying == true)
        {
            _gameController.mainCamera.fieldOfView = Mathf.Lerp(_gameController.mainCamera.fieldOfView, _gameController.cameraSizeFlying, Time.deltaTime * _gameController.cameraSmooth);
            _gameController.shadowEnvironment.SetActive(false);
        }
        else
        {
            _gameController.mainCamera.fieldOfView = Mathf.Lerp(_gameController.mainCamera.fieldOfView, _gameController.cameraSizeDefault, Time.deltaTime * _gameController.cameraSmooth);
            _gameController.shadowEnvironment.SetActive(true);
        }
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
            if (isStartingFly == false)
            {
                playerRB.velocity = new Vector2(playerRB.velocity.x, addHeightFlight);
                isStartingFly= true;
            }
            playerRB.gravityScale = _gameController.gravityFlying;
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
        }
        else if(Input.GetButtonDown("Jump") && isWaterSurface == true)
        {
           playerRB.AddForce(new Vector2(0, jumpHeight));            
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
        GameObject temp = Instantiate(_gameController.prefabBall, _gameController.spawnBallLocation.position, transform.localRotation);        
        temp.GetComponent<Rigidbody2D>().velocity = new Vector2(speedBall, 0);           
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