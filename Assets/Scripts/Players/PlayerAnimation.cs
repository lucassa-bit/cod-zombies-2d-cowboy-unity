using System;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator anim;
    private string currentState;
    private SpriteRenderer childTransform;

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
        "Player_shoot_1",
        "Player_shoot_2",
        "Player_shoot_3",
        "Player_shoot_3",
        "Player_shoot_4",
        "Player_shoot_4",
        "Player_shoot_5",
        "Player_shoot_5",
    };

    private void Awake() {
        anim = GetComponent<Animator>();
        childTransform = GetComponentInChildren<SpriteRenderer>();
    }

    private void Start() {
        currentState = PLAYER_IDLE[0];
    }

    private void ChangeAnimationState(string state) {
        if(currentState == state) { return; } 
        
        anim.Play(state);
        currentState = state;
    }

    private void ChooseAnimationDirection(string[] anims, Vector3 comparator) {
        for (int index = 0; index < DIRECTIONS.Length; index++) {
            if (DIRECTIONS[index].Equals(comparator)) {
                // For not creating more than 6 animations, verify if is a specific animator than mirror the X direction of the sprint
                if (index == 3 || index == 5 || index == 7) {
                    childTransform.flipX = true;
                } else {
                    childTransform.flipX = false;
                }

                ChangeAnimationState(anims[index]);
            }
        }
    }

    public void ChangePlayerMoveAnimation(Vector3 direction, Vector3 lastFacedDirection) {
        if (direction == Vector3.zero) { 
            ChooseAnimationDirection(PLAYER_IDLE, lastFacedDirection);
        } 
        else {
            ChooseAnimationDirection(PLAYER_MOVE, direction);
        }
    }
}
