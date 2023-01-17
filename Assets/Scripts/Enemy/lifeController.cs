using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lifeController : MonoBehaviour
{
    private GameController _gameController;
    private bool isHitted;

    void Start()
    {
        _gameController = FindObjectOfType(typeof(GameController)) as GameController;
    }

    
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D coll)
    {
        switch (coll.gameObject.tag)
        {
            case "HammerHit":
                if (isHitted == false)
                {
                    isHitted = true;
                    StartCoroutine(hitVFXDelay());
                    GameObject temp = Instantiate(_gameController.hitPrefab, transform.position, transform.localRotation);
                    print("efeito");
                }
            break;
        }
    }
    IEnumerator hitVFXDelay()
    {
        yield return new WaitForSeconds(0.5f);
        isHitted = false;
    }
}
