using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [Header("Visual Effects")]
    public GameObject hitPrefab;
    public GameObject shadowEnvironment;

    [Header("Camera FOV")]
    public float cameraSizeSwimming;
    public float cameraSizeFlying;
    public float cameraSizeDefault;
    public float cameraSmooth;
    public float cameraMaxYPosition;
    public float cameraDefaultYPosition;
    public float cameraPositionSmooth;

    [Header("Gravity Settings")]
    public float gravityFlying;
    public float gravitySwimming;

    [Header("Ground Checks")]
    public Transform[] groundChecks;
    public LayerMask whatIsGround;

    [Header("Camera Settings")]
    public Camera mainCamera;
    public Transform leftCameraLimit;
    public Transform rightCameraLimit;
    public float cameraSpeedMove;

    [Header("Colliders")]
    public Collider2D hammerHit;
    public CapsuleCollider2D capColliderDefault;
    public Vector2 defaultOffset;
    public Vector2 defaultSize;
    public Vector2 flyingOffset;
    public Vector2 flyingSize;
    public Vector2 swimmingOffset;
    public Vector2 swimmingSize;

    [Header("Player Shoot Ball")]
    public GameObject prefabBall;
    public Transform spawnBallLocation;

    void Start()
    {
        
    }

    
    void Update()
    {
        
    }
}
