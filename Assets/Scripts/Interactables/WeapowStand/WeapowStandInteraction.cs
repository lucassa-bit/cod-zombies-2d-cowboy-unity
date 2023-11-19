using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class WeapowStandInteraction : MonoBehaviour, IInteraction {
    public void Interaction(PlayerWallet wallet) {
        Debug.Log("WeapowStand activated");
    }

    public long getCost() { return 0; }
}
