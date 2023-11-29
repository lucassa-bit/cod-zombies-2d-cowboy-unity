using UnityEngine;

public class EnemyMoveState : BaseState<EnemyStateMachine.EnemyState>
{
    protected EnemyStateMachine Context;
    protected float MinDistance;

    public EnemyMoveState(EnemyStateMachine.EnemyState key, EnemyStateMachine enemyStateMachine, float MinDistance) : base(key) {
        this.MinDistance = MinDistance;
        Context = enemyStateMachine;
    }

    public override void EnterState() {
        Context.CanWalk(true);
    }

    public override void ExitState() {
        Context.CanWalk(false);
    }

    public override void UpdateState() {
        Context.AnimationEnemy();
    }

    public override EnemyStateMachine.EnemyState GetNextState() {
        if (Context.DistanceOfTarget() < MinDistance)
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
