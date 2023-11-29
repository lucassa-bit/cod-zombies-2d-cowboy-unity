using System;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator Animator;
    private string CurrentState;
    private SpriteRenderer ChildTransform;

    // DIRECTIONS
    // 1 - bottom, 2 - top, 3 - horizontal, 4 - diagonal top, 5 diagonal bottom 

    private static readonly Vector3[] DIRECTIONS = {
        // vertical
        new Vector3(0, -1),
        new Vector3(0, 1),
        // horizontal (Right and left)
        new Vector3(1, 0),
        new Vector3(-1, 0),
        // diagonal top (right and left)
        new Vector3(1, 1).normalized,
        new Vector3(-1, 1).normalized,
        // diagonal bottom (right and left)
        new Vector3(1, -1).normalized,
        new Vector3(-1, -1).normalized,
    };

    // Animation states
    private static readonly string[] PLAYER_IDLE = {
        "Player_idle_1",
        "Player_idle_2",
        "Player_idle_3",
        "Player_idle_3",
        "Player_idle_4",
        "Player_idle_4",
        "Player_idle_5",
        "Player_idle_5",
    };

    private static readonly string[] PLAYER_MOVE = {
        "Player_moving_1",
        "Player_moving_2",
        "Player_moving_3",
        "Player_moving_3",
        "Player_moving_4",
        "Player_moving_4",
        "Player_moving_5",
        "Player_moving_5",
    };

    private static readonly string[] PLAYER_SHOOT = {
        "Player_attacking_1",
        "Player_attacking_2",
        "Player_attacking_3",
        "Player_attacking_3",
        "Player_attacking_4",
        "Player_attacking_4",
        "Player_attacking_5",
        "Player_attacking_5",
    };

    private void Awake() {
        Animator = GetComponent<Animator>();
        ChildTransform = GetComponentInChildren<SpriteRenderer>();
    }

    private void Start() {
        CurrentState = PLAYER_IDLE[0];
    }

    private void ChangeAnimationState(string state) {
        if(CurrentState == state) { return; } 
        
        Animator.Play(state);
        CurrentState = state;
    }

    private void ChooseAnimationDirection(string[] Animators, Vector3 comparator) {
        for (int index = 0; index < DIRECTIONS.Length; index++) {
            if (DIRECTIONS[index].Equals(comparator)) {
                // For not creating more than 6 Animatorations, verify if is a specific Animatorator than mirror the X direction of the sprint
                if (index == 3 || index == 5 || index == 7) {
                    ChildTransform.flipX = true;
                } else {
                    ChildTransform.flipX = false;
                }

                ChangeAnimationState(Animators[index]);
            }
        }
    }

    public void ChangePlayerMoveAnimation(Vector3 direction, Vector3 shootDirection, Vector3 lastFacedDirection, bool playerCanShoot) {
        if (playerCanShoot && shootDirection != Vector3.zero)
        {
            ChooseAnimationDirection(PLAYER_SHOOT, shootDirection);
        }
        else if (direction == Vector3.zero)
        {
            ChooseAnimationDirection(PLAYER_IDLE, lastFacedDirection);
        }
        else
        {
            ChooseAnimationDirection(PLAYER_MOVE, direction);
        }
    }
}
