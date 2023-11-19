using UnityEngine;

public class EnemyMoveState : BaseState<EnemyStateMachine.EnemyState>
{
    protected EnemyStateMachine ctx;
    protected float minDistance;

    public EnemyMoveState(EnemyStateMachine.EnemyState key, EnemyStateMachine enemyStateMachine, float minDistance) : base(key) {
        this.minDistance = minDistance;
        ctx = enemyStateMachine;
    }

    public override void EnterState() {
        ctx.CanWalk(true);
    }

    public override void ExitState() {
        ctx.CanWalk(false);
    }

    public override void UpdateState() {
        ctx.animationEnemy();
    }

    public override EnemyStateMachine.EnemyState GetNextState() {
        if (ctx.DistanceOfTarget() < minDistance)
            return EnemyStateMachine.EnemyState.Attack;
        return EnemyStateMachine.EnemyState.Walk;
    }

    public override void OnTriggerEnter(Collider2D other) {
    }

    public override void OnTriggerExit(Collider2D other) {
    }

    public override void OnTriggerStay(Collider2D other) {
    }
}
