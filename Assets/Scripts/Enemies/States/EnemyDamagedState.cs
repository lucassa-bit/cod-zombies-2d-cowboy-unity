using UnityEngine;

public class EnemyDamagedState : BaseState<EnemyStateMachine.EnemyState>
{
    protected EnemyStateMachine Context;

    private readonly float Cooldown;
    private float Timer;

    public EnemyDamagedState(EnemyStateMachine.EnemyState key, EnemyStateMachine enemyStateMachine, float cooldown) : base(key) {
        Context = enemyStateMachine;
        Cooldown = cooldown;
    }

    public override void EnterState() {
        Timer = Time.time + Cooldown;
    }

    public override void ExitState() {
    }

    public override void UpdateState() {
        Context.AnimationEnemy();
    }

    public override EnemyStateMachine.EnemyState GetNextState() {
        if (Time.time > Timer)
        {
            if(Context.CanAttack())
                return EnemyStateMachine.EnemyState.Attack;
            return EnemyStateMachine.EnemyState.Walk;
        }
        return EnemyStateMachine.EnemyState.Damaged;
    }

    public override void OnTriggerEnter(Collider2D other) {
    }

    public override void OnTriggerExit(Collider2D other) {
    }

    public override void OnTriggerStay(Collider2D other) {
    }
}
