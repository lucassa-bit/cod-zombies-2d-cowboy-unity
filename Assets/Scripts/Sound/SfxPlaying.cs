using UnityEngine;

public class SfxPlaying : MonoBehaviour
{
    [SerializeField]
    private AudioSource Shoot;
    [SerializeField]
    private AudioSource Zombie;
    [SerializeField]
    private AudioSource Interact;

    public void PlayShoot(AudioClip clip)
    {
        if(clip != Shoot.clip)
            Shoot.clip = clip;

        Shoot.Play();
    }

    public void PlayZombie(AudioClip clip)
    {
        if (clip != Zombie.clip)
            Zombie.clip = clip;

        Zombie.Play();
    }

    public void PlayInteract(AudioClip clip)
    {
        if (clip != Interact.clip)
            Interact.clip = clip;

        Interact.Play();
    }
}
