using UnityEngine;
using UnityEngine.UIElements;

public class MeleeWeapon : ShootWeapon {
    private Vector3 position;
    private Vector3 size;

    public override void OnShoot(object sender, PlayerAim.OnShootEventArgs e) {
        if (Time.time > Cooldown) {
            position = e.BulletEndPointPosition;
            size = new Vector3(WeaponStats.Range / 10, WeaponStats.Range / 10, 0);
            RaycastHit2D[] hit = Physics2D.BoxCastAll(e.BulletEndPointPosition, size, 0, e.BulletShootDirection, WeaponStats.Range / 100);
            StartCoroutine(KnifeEffect(e.BulletShootDirection));

            for (int index = 0; index < hit.Length; index++)
            {
                if (hit[index].collider.gameObject.layer == 8)
                {
                    hit[index].collider.GetComponent<EnemyStats>().DoDamageOnEnemy(new SendMessageArgs
                    {
                        BulletDamage = WeaponStats.Damage,
                        DamageDirection = e.BulletShootDirection,
                        PlayerGO = gameObject
                    });
                }
            }

            SoundController.PlayShoot(WeaponStats.WeaponAudioClip);
            ResetTimer();
        }
    }

    public override bool IsKnife()
    {
        return true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.matrix = Matrix4x4.TRS(position, Quaternion.identity, Vector3.one);
        Gizmos.DrawWireCube(Vector3.zero, size);
    }
}
