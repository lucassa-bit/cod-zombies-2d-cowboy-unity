using System;
using UnityEngine;

public class GameInput : MonoBehaviour {
    private Vector2 direction;
    private Vector2 shootDirection;
    private PlayerInputActions playerInputActions;

    public EventHandler OnInteractionAction;
    public EventHandler OnShootAction;

    private void Awake() {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Keyboard.Enable();
        playerInputActions.Keyboard.InteractWithObjects.performed += Interacting;
    }

    private void Start() {
        direction = Vector2.zero;
        shootDirection = Vector2.zero;
    }

    private void Interacting(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        OnInteractionAction?.Invoke(this, EventArgs.Empty);
    }

    private void Shooting(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        OnShootAction?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 getMovementVectorNormalized() {
        ResetDirection();

        direction = playerInputActions.Keyboard.Move.ReadValue<Vector2>();

        return direction.normalized;
    }

    public Vector2 getShootVectorNormalized()
    {
        ResetShootDirection();

        shootDirection = playerInputActions.Keyboard.Shoot.ReadValue<Vector2>();

        return shootDirection.normalized;
    }

    public void ResetDirection() { direction.Set(0, 0); }
    public void ResetShootDirection() { shootDirection.Set(0, 0); }
}
