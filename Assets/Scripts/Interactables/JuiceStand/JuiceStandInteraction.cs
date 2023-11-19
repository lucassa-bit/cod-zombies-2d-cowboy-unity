using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class JuiceStandInteraction : MonoBehaviour, IInteraction {
    public void Interaction(PlayerWallet wallet) {
        Debug.Log("JuiceStand activated");
    }

    public long getCost() { return 0; }
}
