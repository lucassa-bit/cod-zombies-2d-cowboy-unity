using UnityEngine;

public class ShotgunWeapon : ShootWeapon {
    private readonly float RotationRange = 15;

    public override void OnShoot(object sender, PlayerAim.OnShootEventArgs e) {
        if (Time.time > Cooldown && !CantShoot) {
            for (int index = 0; index < GetShells(); index++) {
                SingleShoot(index, e.BulletEndPointPosition, e.BulletShootDirection, RotationRange);
            }
            WeaponStats.Munition--;
            ResetTimer();
        }
    }

    public override int GetShells() {
        return 6;
    }
}
