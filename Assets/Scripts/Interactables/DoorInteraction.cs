using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class DoorInteraction : IInteraction {
    public override void Interaction(PlayerStats playerStats) {
        if(playerStats.DiscountAmount(Cost))
        {
            GetComponent<BoxCollider2D>().enabled = false;
            gameObject.SetActive(false);
            Sfx.PlayInteract(Clip);
        }
    }

    public override float TimeToInteract() {
        return 0;
    }

    public override string GetInteractableName() {
        return "a porta";
    }
}
