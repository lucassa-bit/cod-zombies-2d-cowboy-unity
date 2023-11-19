using Pathfinding;
using System;
using UnityEngine;

public class EnemyStateMachine : StateManager<EnemyStateMachine.EnemyState>
{
    private AIPath aiPath;
    private float circleRadius = 1f;
    private EnemyAnimation anim;
    private EnemyStats stats;

    public enum EnemyState {
        Walk,
        Attack
    }

    private void Awake() {
        aiPath = GetComponent<AIPath>();
        stats = GetComponent<EnemyStats>();
        anim = GetComponentInChildren<EnemyAnimation>();

        float minDistance = 0.5f;
        aiPath.maxSpeed = stats.speed;

        EnemyAttackState enemyAttackState = new(EnemyState.Attack, this, stats.attackDelay);
        EnemyMoveState enemyMoveState = new(EnemyState.Walk, this, minDistance);

        States.Add(EnemyState.Attack, enemyAttackState);
        States.Add(EnemyState.Walk, enemyMoveState);

        CurrentState = States[EnemyState.Walk];
    }

    public void CanWalk(bool isWalking) {
        aiPath.canMove = isWalking;
    }

    public float DistanceOfTarget() {
        return aiPath.remainingDistance;
    }

    public RaycastHit2D CanAttack() {
        return Physics2D.CircleCast(transform.position, circleRadius, Vector2.zero, 0, LayerMask.GetMask("Player"));
    }

    public void animationEnemy() {
        anim.ChangeEnemyMoveAnimation(aiPath.desiredVelocity.normalized);
    }
}
