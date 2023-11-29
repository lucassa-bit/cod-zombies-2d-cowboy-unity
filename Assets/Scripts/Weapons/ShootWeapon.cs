using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ShootWeapon : MonoBehaviour {
    public WeaponScriptableObject WeaponStats;
    protected PlayerStats PlayerStats;
    protected PlayerAim PlayerAim;
    protected SfxPlaying SoundController;
    protected LineRenderer[] line;
    protected float Cooldown;
    protected float ReloadCooldown;
    protected int shells;
    [HideInInspector]
    public bool CantShoot;

    public class SendMessageArgs
    {
        public float BulletDamage;
        public Vector3 DamageDirection;
        public GameObject PlayerGO;
    }

    private void Awake()
    {
        PlayerStats = GetComponent<PlayerStats>();
        PlayerAim = GetComponent<PlayerAim>();
        SoundController = GetComponent<PlayerBase>().SoundController;

        line = new LineRenderer[GetShells()];
        line[0] = GetComponentInChildren<LineRenderer>();

        for (int index = 1; index < GetShells(); index++)
        {
            GameObject go = new("Shoot - " + (index + 1));
            go.transform.parent = gameObject.transform;
            line[index] = go.AddComponent<LineRenderer>();
            line[index].widthMultiplier = 0.05f;
            line[index].material = PlayerAim.ShootMaterial;
        }

        line = GetComponentsInChildren<LineRenderer>();
    }

    private void Start()
    {
        WeaponStats.Munition = WeaponStats.MaxMunition;
        CantShoot = false;
    }

    private void Update()
    {
        if(Time.time > ReloadCooldown && CantShoot)
        {
            WeaponStats.Munition = WeaponStats.MaxMunition;
            CantShoot = false;
        }
    }

    private void OnEnable()
    {
        PlayerAim.onShoot += OnShoot;
    }

    private void OnDisable()
    {
        PlayerAim.onShoot -= OnShoot;
    }

    private IEnumerator ShootVisual(int index, Vector3 initialPos, Vector3 finalPos)
    {
        line[index].enabled = true;
        line[index].SetPosition(0, initialPos);
        line[index].SetPosition(1, initialPos + finalPos);
        SoundController.PlayShoot(WeaponStats.WeaponAudioClip);
        yield return new WaitForSeconds(0.1f);
        line[index].enabled = false;
    }

    protected IEnumerator KnifeEffect(Vector3 direction)
    {
        SpriteRenderer sr = PlayerAim.BulletPosition.GetComponent<SpriteRenderer>();
        sr.enabled = true;
        sr.flipX = direction == Vector3.left;
        yield return new WaitForSeconds(0.1f);
        sr.enabled = false;
    }

    private Quaternion GetRandom(float angle)
    {
        float randomAngle = UnityEngine.Random.Range(-angle, angle);
        return Quaternion.AngleAxis(randomAngle, Vector3.forward);
    }

    protected void SingleShoot(Vector3 position, Vector3 shootDirection)
    {
        if(WeaponStats.Munition <= 0) return;

        SingleShoot(0, position, shootDirection, 5);
        WeaponStats.Munition--;
        ResetTimer();
    }

    protected void SingleShoot(int index, Vector3 position, Vector3 shootDirection, float maxAngle)
    {
        Vector3 finalPos = GetRandom(maxAngle) * shootDirection;
        RaycastHit2D hit = Physics2D.Raycast(position, finalPos, WeaponStats.Range);
        StartCoroutine(ShootVisual(index, position, !hit ? finalPos * WeaponStats.Range : (Vector3)hit.point - transform.position));
            
        if (hit && hit.collider.gameObject.layer == 8)
            hit.collider.GetComponent<EnemyStats>().DoDamageOnEnemy(new SendMessageArgs
            {
                BulletDamage = DamageWeapon(),
                DamageDirection = shootDirection,
                PlayerGO = gameObject
            });
    }

    public abstract void OnShoot(object sender, PlayerAim.OnShootEventArgs e);

    public void ResetTimer()
    {
        if (WeaponStats.Munition <= 0)
        {
            ReloadCooldown = Time.time + ReloadTimeWeapon();
            CantShoot = true;
        }
        Cooldown = Time.time + FireRateWeapon(2f);
    }

    public float ReloadTimeWeapon()
    {
        return WeaponStats.ReloadTime - (PlayerStats.ReloadSpeed * 0.5f);
    }

    public float FireRateWeapon(float weight)
    {
        return weight / WeaponStats.FireRateBase;
    }

    public float DamageWeapon()
    {
        return WeaponStats.Damage + (PlayerStats.Damage * 0.5f);
    }

    public virtual int GetShells()
    {
        return 1;
    }

    public virtual bool IsKnife()
    {
        return false;
    }


    public override bool Equals(object obj)
    {
        return obj is ShootWeapon weapon &&
               base.Equals(obj) &&
               EqualityComparer<WeaponScriptableObject>.Default.Equals(WeaponStats, weapon.WeaponStats) &&
               EqualityComparer<PlayerStats>.Default.Equals(PlayerStats, weapon.PlayerStats) &&
               EqualityComparer<PlayerAim>.Default.Equals(PlayerAim, weapon.PlayerAim) &&
               Cooldown == weapon.Cooldown;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(base.GetHashCode(), WeaponStats, PlayerStats, PlayerAim, Cooldown);
    }
}
