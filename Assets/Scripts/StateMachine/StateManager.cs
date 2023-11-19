using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateManager<EState> : MonoBehaviour where EState : Enum
{
    protected Dictionary<EState, BaseState<EState>> States = new Dictionary<EState, BaseState<EState>>(); 
    protected BaseState<EState> CurrentState;
    protected bool IsTransitioningState = false;

    void Start() {
        CurrentState.EnterState();
    }

    void Update() {
        EState nextStateKey = CurrentState.GetNextState();

        if (!IsTransitioningState) {
            if (nextStateKey.Equals(CurrentState.StateKey))
                CurrentState.UpdateState();
            else
                TransitionToState(nextStateKey);
        }
    }

    void TransitionToState(EState nextStateKey) {
        IsTransitioningState = true;
        CurrentState.ExitState();
        CurrentState = States[nextStateKey];
        CurrentState.EnterState();
        IsTransitioningState = false;
    }

    void OnTriggerEnter2D(Collider2D collision) {
        CurrentState.OnTriggerEnter(collision);
    }

    void OnTriggerExit2D(Collider2D collision) {
        CurrentState.OnTriggerExit(collision);
    }

    void OnTriggerStay2D(Collider2D collision) {
        CurrentState.OnTriggerStay(collision);

    }
}
