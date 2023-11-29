using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "ScriptableObjects/Weapon", order = 2)]
public class WeaponScriptableObject : ScriptableObject {
    public string WeaponName;
    public float FireRateBase;
    public float ReloadTime;
    public float Damage;
    public float Range;
    public int MaxMunition;
    public int Munition;

    public TypesOfWeapons Weapons;
    public Sprite WeaponSprite;
    public Sprite AmmoSprite;
    public AudioClip WeaponAudioClip;
}
