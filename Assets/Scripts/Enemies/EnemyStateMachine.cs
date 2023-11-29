using Pathfinding;
using UnityEngine;

public class EnemyStateMachine : StateManager<EnemyStateMachine.EnemyState>
{
    private AIPath AiPath;

    private EnemyAnimation Animation;
    private EnemyStats Stats;

    private float CircleRadius = 0.5f;

    public enum EnemyState {
        Walk,
        Attack,
        Damaged
    }

    private void Awake() {
        AiPath = GetComponent<AIPath>();
        Stats = GetComponent<EnemyStats>();
        
        Animation = GetComponentInChildren<EnemyAnimation>();

        AiPath.maxSpeed = Stats.Speed;

        EnemyAttackState enemyAttackState = new(EnemyState.Attack, this, Stats.AttackDelay);
        EnemyMoveState enemyMoveState = new(EnemyState.Walk, this, CircleRadius);

        States.Add(EnemyState.Attack, enemyAttackState);
        States.Add(EnemyState.Walk, enemyMoveState);

        CurrentState = States[EnemyState.Walk];
    }

    public void CanWalk(bool isWalking) {
        AiPath.canMove = isWalking;
    }

    public float DistanceOfTarget() {
        return AiPath.remainingDistance;
    }

    public RaycastHit2D CanAttack() {
        return Physics2D.CircleCast(transform.position, CircleRadius, Vector2.zero, 0, LayerMask.GetMask("Player"));
    }

    public void AnimationEnemy() {
        Animation.ChangeEnemyMoveAnimation(AiPath.desiredVelocity.normalized);
    }
}
