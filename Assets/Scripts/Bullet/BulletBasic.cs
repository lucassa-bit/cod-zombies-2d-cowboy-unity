using UnityEngine;

public class BulletBasic : MonoBehaviour {
    private Vector3 shootDirection;
    private float bulletDamage;
    private float bulletSpeed;
    private float raycastHitSize = 0.2f;
    private float lifeTime;

    public void Setup(Vector3 shootDirection, float bulletDamage, float bulletSpeed, float lifeTime) {
        this.shootDirection = shootDirection;
        this.lifeTime = lifeTime + Time.time;
        this.bulletSpeed = bulletSpeed;
        this.bulletDamage = bulletDamage;

        float direction = Vector3.Angle(transform.position, shootDirection);
        if (shootDirection.y < 0) direction *= -1;
        transform.rotation = Quaternion.Euler(0,0, direction);
    }

    private void Update() {
        transform.position += shootDirection * Time.deltaTime * bulletSpeed;
        checkCollision();

        if (Time.time > lifeTime) {
            Destroy(gameObject);
        }
    }

    private void checkCollision() {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, shootDirection, raycastHitSize);
        
        if( hit.collider != null ) {
            if(hit.transform.gameObject.layer == 8) {
                hit.transform.SendMessage("DoDamageOnEnemy", bulletDamage);
            }
            Destroy(gameObject);
        }
    }
}
