using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lifeController : MonoBehaviour
{
    // Get Componnets and Scripts
    private GameController _gameController;
    private Player _player;
    private Animator enemyAnim;

    [Header("Enemy Settings")]
    public int enemyLifeMax;




    // Private Variables
    private bool isHitted;
    

    void Start()
    {
        _gameController = FindObjectOfType(typeof(GameController)) as GameController;
        _player = FindObjectOfType(typeof(Player)) as Player;
        enemyAnim = GetComponent<Animator>();
    }

    
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        switch (coll.gameObject.tag)
        {
            case "HammerHit":
                if (!isHitted)
                {
                    isHitted = true;
                    StartCoroutine(hitVFXDelay());
                    GameObject temp = Instantiate(_gameController.hitPrefab, transform.position, transform.localRotation);
                    damageController(_player.hammerDamage);
                    enemyAnim.SetTrigger("isHitted");

                }
            break;

            case "BallHit":

                Destroy(coll.gameObject,0.1f);
                if (!isHitted)
                {
                    isHitted = true;
                    StartCoroutine(hitVFXDelay());
                    GameObject temp = Instantiate(_gameController.hitPrefab, transform.position, transform.localRotation);
                    damageController(_player.ballDamage);
                    enemyAnim.SetTrigger("isHitted");
                }
                break;
        }
    }
    void updateAnimator()
    {
        enemyAnim.SetTrigger("isHitted");
    }
    void damageController(int dmg)
    {
        enemyLifeMax -= dmg;
        if(enemyLifeMax <= 0)
        {
            Destroy(this.gameObject,0.2f);
        }
    }

    IEnumerator hitVFXDelay()
    {
        yield return new WaitForSeconds(0.5f);
        isHitted = false;
    }
}
