using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    private Animator Animator;
    private string CurrentState;
    private SpriteRenderer ChildTransform;

    private static readonly string[] ENEMY_MOVE = {
        "Enemy_moving_1",
        "Enemy_moving_2",
        "Enemy_moving_3",
        "Enemy_moving_4",
        "Enemy_moving_5",
    };

    private void Awake()
    {
        Animator = GetComponent<Animator>();
        ChildTransform = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        CurrentState = ENEMY_MOVE[0];
    }

    private void ChangeAnimationState(string state)
    {
        if (CurrentState == state) { return; }

        Animator.Play(state);
        CurrentState = state;
    }

    private void ChooseAnimationDirection(string[] Animators, Vector3 comparator)
    {
        ChildTransform.flipX = false;

        // diagonal top (right and left)
        // diagonal bottom (right and left)

        // For not creating more than 6 Animatorations, verify if is a specific Animatorator than mirror the X direction of the sprint
        // BOTTOM
        if (comparator.x >= -0.45f && comparator.x <= 0.45f && comparator.y <= -0.9f)
            ChangeAnimationState(Animators[0]);
        // TOP
        else if (comparator.x >= -0.45f && comparator.x <= 0.45f && comparator.y >= 0.9f)
            ChangeAnimationState(Animators[1]);
        // RIGHT
        else if (comparator.y >= -0.45f && comparator.y <= 0.45f && comparator.x >= 0.9f)
            ChangeAnimationState(Animators[2]);
        // LEFT
        else if (comparator.y >= -0.45f && comparator.y <= 0.45f && comparator.x <= -0.9f) {
            ChangeAnimationState(Animators[2]);
            ChildTransform.flipX = true;
        }
        // DIAGONAL TOP RIGHT
        else if (comparator.x > 0.45f && comparator.x < 0.9f && comparator.y < 0.9f && comparator.y > 0.45f)
            ChangeAnimationState(Animators[3]);
        // DIAGONAL TOP LEFT
        else if (comparator.x < -0.45f && comparator.x > -0.9f && comparator.y < 0.9f && comparator.y > 0.45f) {
            ChangeAnimationState(Animators[3]);
            ChildTransform.flipX = true;
        }
        // DIAGONAL BOTTOM RIGHT
        else if (comparator.x > 0.45f && comparator.x < 0.9f && comparator.y > -0.9f && comparator.y < -0.45f)
            ChangeAnimationState(Animators[4]);
        // DIAGONAL BOTTOM LEFT
        else if (comparator.x < -0.45f && comparator.x > -0.9f && comparator.y > -0.9f && comparator.y < -0.45f)
        {
            ChangeAnimationState(Animators[4]);
            ChildTransform.flipX = true;
        }
    }

    public void ChangeEnemyMoveAnimation(Vector3 direction)
    {
        ChooseAnimationDirection(ENEMY_MOVE, direction);
    }
}
