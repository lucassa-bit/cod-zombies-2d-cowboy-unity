using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField]
    private float RayCastLenght = 1.0f;

    private PlayerBase PlayerBase;
    private RaycastHit2D Colide;

    private void Awake() {
        PlayerBase = GetComponent<PlayerBase>();
    }

    private void Update() {
        Colide = Physics2D.Raycast(transform.position, PlayerBase.LastFacedDirection, RayCastLenght, LayerMask.GetMask("Interactables", "Doors"));
    }

    public void CheckInteraction() {
        if(Colide)
            Colide.collider.gameObject.GetComponent<IInteraction>()?.Interaction(GetComponent<PlayerStats>());
    }
}
