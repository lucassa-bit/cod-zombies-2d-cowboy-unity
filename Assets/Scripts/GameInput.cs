using System;
using UnityEngine;

public class GameInput : MonoBehaviour {
    private Vector2 Direction;
    private Vector2 ShootDirection;
    private PlayerInputActions PlayerInputActions;

    public EventHandler OnInteractionAction;
    public EventHandler OnSwitchWeaponAction;
    public EventHandler OnPauseGameAction;

    private void Awake() {
        PlayerInputActions = new PlayerInputActions();
        PlayerInputActions.Keyboard.Enable();

        PlayerInputActions.Keyboard.InteractWithObjects.performed += Interacting;
        PlayerInputActions.Keyboard.SwitchWeapon.performed += SwitchWeapon;
        PlayerInputActions.Keyboard.Pause.performed += PauseGame;
    }

    private void Start() {
        Direction = Vector2.zero;
        ShootDirection = Vector2.zero;
    }

    private void Interacting(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        OnInteractionAction?.Invoke(this, EventArgs.Empty);
    }

    private void SwitchWeapon(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        OnSwitchWeaponAction?.Invoke(this, EventArgs.Empty);
    }

    private void PauseGame(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnPauseGameAction?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 getMovementVectorNormalized() {
        ResetDirection();

        Direction = PlayerInputActions.Keyboard.Move.ReadValue<Vector2>();

        return Direction.normalized;
    }

    public Vector2 getShootVectorNormalized()
    {
        ResetShootDirection();

        ShootDirection = PlayerInputActions.Keyboard.Shoot.ReadValue<Vector2>();

        return ShootDirection.normalized;
    }

    public void ResetDirection() { Direction.Set(0, 0); }
    public void ResetShootDirection() { ShootDirection.Set(0, 0); }
}
