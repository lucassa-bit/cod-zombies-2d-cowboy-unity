using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    private Animator anim;
    private string currentState;
    private SpriteRenderer childTransform;

    private static readonly string[] ENEMY_MOVE = {
        "Enemy_moving_1",
        "Enemy_moving_2",
        "Enemy_moving_3",
        "Enemy_moving_4",
        "Enemy_moving_5",
    };

    private void Awake()
    {
        anim = GetComponent<Animator>();
        childTransform = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        currentState = ENEMY_MOVE[0];
    }

    private void ChangeAnimationState(string state)
    {
        if (currentState == state) { return; }

        anim.Play(state);
        currentState = state;
    }

    private void ChooseAnimationDirection(string[] anims, Vector3 comparator)
    {
        childTransform.flipX = false;

        // diagonal top (right and left)
        // diagonal bottom (right and left)

        // For not creating more than 6 animations, verify if is a specific animator than mirror the X direction of the sprint
        // BOTTOM
        if (comparator.x >= -0.45f && comparator.x <= 0.45f && comparator.y <= -0.9f)
            ChangeAnimationState(anims[0]);
        // TOP
        else if (comparator.x >= -0.45f && comparator.x <= 0.45f && comparator.y >= 0.9f)
            ChangeAnimationState(anims[1]);
        // RIGHT
        else if (comparator.y >= -0.45f && comparator.y <= 0.45f && comparator.x >= 0.9f)
            ChangeAnimationState(anims[2]);
        // LEFT
        else if (comparator.y >= -0.45f && comparator.y <= 0.45f && comparator.x <= -0.9f) {
            ChangeAnimationState(anims[2]);
            childTransform.flipX = true;
        }
        // DIAGONAL TOP RIGHT
        else if (comparator.x > 0.45f && comparator.x < 0.9f && comparator.y < 0.9f && comparator.y > 0.45f)
            ChangeAnimationState(anims[3]);
        // DIAGONAL TOP LEFT
        else if (comparator.x < -0.45f && comparator.x > -0.9f && comparator.y < 0.9f && comparator.y > 0.45f) {
            ChangeAnimationState(anims[3]);
            childTransform.flipX = true;
        }
        // DIAGONAL BOTTOM RIGHT
        else if (comparator.x > 0.45f && comparator.x < 0.9f && comparator.y > -0.9f && comparator.y < -0.45f)
            ChangeAnimationState(anims[4]);
        // DIAGONAL BOTTOM LEFT
        else if (comparator.x < -0.45f && comparator.x > -0.9f && comparator.y > -0.9f && comparator.y < -0.45f)
        {
            ChangeAnimationState(anims[4]);
            childTransform.flipX = true;
        }
    }

    public void ChangeEnemyMoveAnimation(Vector3 direction)
    {
        ChooseAnimationDirection(ENEMY_MOVE, direction);
    }
}
