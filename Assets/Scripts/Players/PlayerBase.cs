using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerInteraction))]
public class PlayerBase : MonoBehaviour
{
    [SerializeField]
    private GameInput gameInput;

    private void Start() {
        gameInput.OnInteractionAction += OnInteractionAction;
    }

    private void Update() {
        GetComponent<PlayerMovement>().SetDirection(gameInput.getMovementVectorNormalized());
        GetComponent<PlayerAim>().SetDirection(gameInput.getShootVectorNormalized());
    }

    private void OnInteractionAction(object sender, System.EventArgs e) {
        GetComponent<PlayerInteraction>().CheckInteraction();
    }
}
