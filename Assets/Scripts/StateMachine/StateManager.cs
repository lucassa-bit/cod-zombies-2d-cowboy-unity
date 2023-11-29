using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateManager<EState> : MonoBehaviour where EState : Enum
{
    protected Dictionary<EState, BaseState<EState>> States = new Dictionary<EState, BaseState<EState>>(); 
    protected BaseState<EState> CurrentState;
    protected bool IsTransitioningState = false;

    protected void Start() {
        CurrentState.EnterState();
    }

    protected void Update() {
        EState nextStateKey = CurrentState.GetNextState();

        if (!IsTransitioningState) {
            if (nextStateKey.Equals(CurrentState.StateKey))
                CurrentState.UpdateState();
            else
                TransitionToState(nextStateKey);
        }
    }

    protected void TransitionToState(EState nextStateKey) {
        IsTransitioningState = true;
        CurrentState.ExitState();
        CurrentState = States[nextStateKey];
        CurrentState.EnterState();
        IsTransitioningState = false;
    }

    protected void OnTriggerEnter2D(Collider2D collision) {
        CurrentState.OnTriggerEnter(collision);
    }

    protected void OnTriggerExit2D(Collider2D collision) {
        CurrentState.OnTriggerExit(collision);
    }

    protected void OnTriggerStay2D(Collider2D collision) {
        CurrentState.OnTriggerStay(collision);

    }
}
