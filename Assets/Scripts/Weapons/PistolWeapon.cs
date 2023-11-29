using UnityEngine;

public class PistolWeapon : ShootWeapon {
    public override void OnShoot(object sender, PlayerAim.OnShootEventArgs e) {
        if (Time.time > Cooldown) {
            SingleShoot(e.BulletEndPointPosition, e.BulletShootDirection);
        }
    }
}
