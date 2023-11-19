using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour {
    private PlayerStats stats;
    public Vector3 velocityVector { get; private set; }
    public Vector3 lastFacedDirection {  get; private set; }

    private PlayerAnimation playerAnim;
    private Rigidbody2D rgbd;

    private void Awake() {
        stats = GetComponent<PlayerStats>();
        rgbd = GetComponent<Rigidbody2D>();
        playerAnim = GetComponentInChildren<PlayerAnimation>();
    }

    private void Start() {
        velocityVector = Vector3.zero;
        lastFacedDirection = Vector3.zero;
    }

    public void SetDirection(Vector3 direction) {
        velocityVector = direction;

        if(direction != Vector3.zero && direction != lastFacedDirection) {
            lastFacedDirection = direction;
        }
    }    

    private void FixedUpdate() {
        rgbd.velocity = velocityVector * stats.speed;
        playerAnim.ChangePlayerMoveAnimation(velocityVector, lastFacedDirection);
    }
}
