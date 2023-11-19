using UnityEngine;

public class PlayerShootProjectiles : MonoBehaviour
{
    [SerializeField]
    private Transform pfBullet;
    private PlayerStats stats;
    private float coolDown;
    private float bulletLifeTime = 5f;

    private void Awake() {
        stats = GetComponent<PlayerStats>();
        GetComponent<PlayerAim>().onShoot += BulletShootProjection_OnShoot;
    }

    private void Start() {
        coolDown = Time.time + 1/stats.fireRate;
    }

    private void BulletShootProjection_OnShoot(object sender, PlayerAim.OnShootEventArgs e) {
        if(Time.time > coolDown) {
            Transform instantiateGO = Instantiate(pfBullet, e.BulletEndPointPosition, Quaternion.identity);

            instantiateGO.GetComponent<BulletBasic>().Setup(e.BulletShootDirection.normalized, stats.damage, stats.bulletSpeed, bulletLifeTime);
            coolDown = Time.time + 1/stats.fireRate;
        }

    }
}
