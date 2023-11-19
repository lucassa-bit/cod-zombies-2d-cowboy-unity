using UnityEngine;

public class PlayerStats : MonoBehaviour {
    public float healthMax;
    public float health {  get; private set; }
    public float speed;
    public float damage;
    public float fireRate;
    public float bulletSpeed;

    private float cooldownToHealUp;
    private float timerToHealUp;

    private void Start() {
        cooldownToHealUp = 5f;
    }

    private void Update() {
        if(health < healthMax) {
            if(Time.time > timerToHealUp) {
                health = healthMax;
            }
        }
    }

    public void DoDamageOnPlayer(float damageQuantity) {
        health -= damageQuantity;
        timerToHealUp = Time.time + cooldownToHealUp;

        if(health <= 0) Destroy(gameObject);
    }
}
