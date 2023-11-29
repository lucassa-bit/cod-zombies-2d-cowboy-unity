using UnityEngine;

public class EnemyAttackState : BaseState<EnemyStateMachine.EnemyState> {
    protected EnemyStateMachine Context;
    protected float AttackCooldown;
    protected float Cooldown;

    public EnemyAttackState(EnemyStateMachine.EnemyState key, EnemyStateMachine enemyStateMachine, float AttackCooldown) : base(key) {
        this.AttackCooldown = AttackCooldown;
        Context = enemyStateMachine;
    }

    public override void EnterState() {
    }

    public override void ExitState() {
    }

    public override void UpdateState() {
        if (Time.time > Cooldown) {
            Context.CanAttack().transform.SendMessage("DamageOnPlayer", 1f);
            Cooldown = AttackCooldown + Time.time;
        }
    }

    public override EnemyStateMachine.EnemyState GetNextState() {
        if (!Context.CanAttack())
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