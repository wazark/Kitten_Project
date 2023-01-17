using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class AIController : MonoBehaviour
{
    public float maxDistance = 10f;
    public float speed = 1f;
    public float chaseSpeed = 2f;
    public float respawnDelay = 5f;
    public bool respawnOnDeath = true;

    private Transform player;
    private bool chasing = false;
    private bool dead = false;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (!dead)
        {
            // Search for player
            if (!chasing)
            {
                transform.Translate(speed * Time.deltaTime, 0, 0);

                if (transform.position.x > maxDistance)
                {
                    speed = -speed;
                    transform.Translate(speed * Time.deltaTime, 0, 0);
                }
                else if (transform.position.x < -maxDistance)
                {
                    speed = -speed;
                    transform.Translate(speed * Time.deltaTime, 0, 0);
                }

                float distance = Vector3.Distance(transform.position, player.position);
                if (distance <= maxDistance)
                {
                    chasing = true;
                }
            }
            // Chase player
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, player.position, chaseSpeed * Time.deltaTime);
            }
        }
    }

    public void Die()
    {
        if (respawnOnDeath)
        {
            dead = true;
            Invoke("Respawn", respawnDelay);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Respawn()
    {
        dead = false;
    }
}
