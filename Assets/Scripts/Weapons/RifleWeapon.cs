using UnityEngine;

public class RifleWeapon : ShootWeapon {
    public override void OnShoot(object sender, PlayerAim.OnShootEventArgs e)
    {
        if (Time.time > Cooldown)
        {
            SingleShoot(e.BulletEndPointPosition, e.BulletShootDirection);
        }
    }
}
