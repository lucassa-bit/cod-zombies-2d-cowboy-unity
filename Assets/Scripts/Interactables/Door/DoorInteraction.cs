using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class DoorInteraction : MonoBehaviour, IInteraction {
    [SerializeField]
    private long cost;

    public void Interaction(PlayerWallet wallet) {
        GetComponent<BoxCollider2D>().enabled = false;

        Transform[] transforms = GetComponentsInChildren<Transform>();

        foreach (Transform transform in transforms) {
            if(transform.gameObject.name == "Cost_visual") {
                transform.gameObject.SetActive(false);
            }
        }

        wallet.discountAmount(cost);
    }

    public long getCost() { return cost; }
}
