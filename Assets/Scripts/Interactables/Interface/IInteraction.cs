using UnityEngine;

public abstract class IInteraction : MonoBehaviour {
    public long Cost;
    public AudioClip Clip;
    public SfxPlaying Sfx;

    public abstract void Interaction(PlayerStats playerStats);
    public long GetCost() { return Cost; }
    public virtual bool CanInteract()
    {
        return true;
    }

    public virtual float TimeToInteract()
    {
        return 0;
    }

    public abstract string GetInteractableName();
}
