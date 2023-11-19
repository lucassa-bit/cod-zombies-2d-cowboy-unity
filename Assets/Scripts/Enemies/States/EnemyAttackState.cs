using UnityEngine;

public class EnemyAttackState : BaseState<EnemyStateMachine.EnemyState> {
    protected EnemyStateMachine ctx;
    protected float attackCooldown;
    protected float cooldown;

    public EnemyAttackState(EnemyStateMachine.EnemyState key, EnemyStateMachine enemyStateMachine, float attackCooldown) : base(key) {
        this.attackCooldown = attackCooldown;
        ctx = enemyStateMachine;
    }

    public override void EnterState() {
    }

    public override void ExitState() {
    }

    public override void UpdateState() {
        if (Time.time > cooldown) {
            ctx.CanAttack().transform.SendMessage("DoDamageOnPlayer", 1.0f);
            cooldown = attackCooldown + Time.time;
        }
    }

    public override EnemyStateMachine.EnemyState GetNextState() {
        if (!ctx.CanAttack())
            return EnemyStateMachine.EnemyState.Walk;
        return EnemyStateMachine.EnemyState.Attack;
    }

    public override void OnTriggerEnter(Collider2D other) {
    }

    public override void OnTriggerExit(Collider2D other) {
    }

    public override void OnTriggerStay(Collider2D other) {
    }
}