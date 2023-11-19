using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public float maxHealth;
    public float health {  get; private set; }

    public float speed;
    public float attackDelay;

    public void Start() {
        health = maxHealth;
    }

    public void DoDamageOnEnemy(int damage) {
        health -= damage;

        // Adicionar animação de morte
        if(health <= 0) Destroy(gameObject);
    }

    private void OnDestroy() {
        
    }
}
