using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour {
    private PlayerStats Stats;
    public Vector3 VelocityVector { get; private set; }

    private Rigidbody2D rgbd;

    private void Awake() {
        Stats = GetComponent<PlayerStats>();
        rgbd = GetComponent<Rigidbody2D>();
    }

    private void Start() {
        VelocityVector = Vector3.zero;
    }

    public void SetDirection(Vector3 direction) {
        VelocityVector = direction;
    }    

    private void FixedUpdate() {
        rgbd.velocity = VelocityVector * Stats.BaseSpeed;
    }
}
