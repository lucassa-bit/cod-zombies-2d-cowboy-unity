using System.Collections;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    private SpriteRenderer sprite;

    public float MaxHealth;
    public float Health {  get; private set; }

    public float Speed;
    public float AttackDelay;
    public long KillGain;

    [HideInInspector]
    public GameController Controller;

    public void Start() {
        sprite = GetComponentInChildren<SpriteRenderer>();
        Health = MaxHealth;
    }

    public void DoDamageOnEnemy(ShootWeapon.SendMessageArgs args) {
        Health -= args.BulletDamage;

        StartCoroutine(DamageEnemy());

        // Adicionar anima��o de morte
        if(Health <= 0) {
            args.PlayerGO.SendMessage("AddAmount", KillGain);
            Destroy(gameObject);
        }
    }

    private IEnumerator DamageEnemy()
    {
        sprite.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        sprite.color = Color.white;
    }

    private void OnDestroy()
    {
        Controller.EnemyKilled();
    }
}
