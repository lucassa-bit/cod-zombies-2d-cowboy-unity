using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField]
    private float raycastLenght = 1.0f;

    private PlayerMovement playerMovement;
    private RaycastHit2D colide;

    private void Awake() {
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update() {
        colide = Physics2D.Raycast(transform.position, playerMovement.lastFacedDirection, raycastLenght, LayerMask.GetMask("Interactables"));

        // Put a visual select
    }

    public void CheckInteraction() {
        if(colide)
            colide.collider.gameObject.GetComponent<IInteraction>()?.Interaction(GetComponent<PlayerWallet>());
    }
}
