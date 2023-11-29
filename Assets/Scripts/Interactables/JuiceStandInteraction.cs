using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class JuiceStandInteraction : IInteraction {
    [SerializeField]
    private float CooldownToBuy;
    private float Timer;

    public JuicesScriptableObject Juice;

    public override void Interaction(PlayerStats playerStats) {
        if (!playerStats.DiscountAmount(Cost)  || Time.time < Timer) return;

        playerStats.AddNewJuice(Juice);

        Timer = Time.time + CooldownToBuy;
        Sfx.PlayInteract(Juice.Clip);

    }

    public override bool CanInteract() {
        return Time.time > Timer;
    }
    public override float TimeToInteract() {
        return Timer - Time.time;
    }

    public override string GetInteractableName() {
        return "a bebida " + Juice.name;
    }
}
