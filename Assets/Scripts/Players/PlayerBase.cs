using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerInteraction))]
public class PlayerBase : MonoBehaviour
{
    [SerializeField]
    private GameInput GameInput;
    [SerializeField]
    private GameController GameController;
    public SfxPlaying SoundController;

    private PlayerMovement PlayerMovement;
    private PlayerAnimation PlayerAnimation;
    private PlayerAim PlayerAim;
    private PlayerColdre PlayerColdre;

    [HideInInspector]
    public Vector3 LastFacedDirection;

    private void Awake()
    {
        PlayerMovement = GetComponent<PlayerMovement>();
        PlayerAnimation = GetComponentInChildren<PlayerAnimation>();
        PlayerAim = GetComponent<PlayerAim>();
        PlayerColdre = GetComponent<PlayerColdre>();
    }

    private void Start() {
        GameInput.OnInteractionAction = OnInteractionAction;
        GameInput.OnSwitchWeaponAction = OnSwitchWeapon;
        GameInput.OnPauseGameAction = OnPauseGameAction;
    }

    private void Update() {
        Vector3 movementDirection = GameInput.getMovementVectorNormalized();
        Vector3 ShootDirection = GameInput.getShootVectorNormalized();

        bool canShoot = PlayerColdre.Weapons.Count > 0 && !PlayerColdre.Weapons[PlayerColdre.IsPrimary ? 0 : 1].CantShoot && !PlayerColdre.Weapons[PlayerColdre.IsPrimary ? 0 : 1].IsKnife();

        PlayerMovement.SetDirection(movementDirection);
        PlayerAim.SetDirection(ShootDirection);

        if ((movementDirection != Vector3.zero || ShootDirection != Vector3.zero) && (movementDirection != LastFacedDirection || ShootDirection != LastFacedDirection))
        {
            LastFacedDirection = movementDirection;
        }

        PlayerAnimation.ChangePlayerMoveAnimation(PlayerMovement.VelocityVector, PlayerAim.ShootDirection, LastFacedDirection, canShoot); 
    }

    private void OnInteractionAction(object sender, System.EventArgs e) {
        GetComponent<PlayerInteraction>().CheckInteraction();
    }

    private void OnPauseGameAction(object sender, System.EventArgs e)
    {
        GameController.PauseGame();
    }

    private void OnSwitchWeapon(object sender, System.EventArgs e) {
        PlayerColdre.SwitchWeapon();
    }

    private void OnDestroy()
    {
        GameInput.OnInteractionAction -= OnInteractionAction;
        GameInput.OnSwitchWeaponAction -= OnSwitchWeapon;
        GameInput.OnPauseGameAction -= OnPauseGameAction;
        GameController.GameOver();
    }
}
