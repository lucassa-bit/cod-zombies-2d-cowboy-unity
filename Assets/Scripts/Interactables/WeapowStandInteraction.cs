using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class WeapowStandInteraction : IInteraction {
    [SerializeField]
    private WeaponScriptableObject WeaponScriptableObject;
    [SerializeField]
    private SpriteRenderer weaponStand;

    private void Start()
    {
        weaponStand.sprite = WeaponScriptableObject.WeaponSprite;
    }

    public override void Interaction(PlayerStats playerStats) {
        if (!playerStats.DiscountAmount(Cost)) return;

        switch (WeaponScriptableObject.Weapons) {
            case TypesOfWeapons.SHOTGUN:
                AddComponent<ShotgunWeapon>(playerStats.gameObject);
                break;
            case TypesOfWeapons.RIFLE:
                AddComponent<RifleWeapon>(playerStats.gameObject);
                break;
            case TypesOfWeapons.MELEE:
                AddComponent<MeleeWeapon>(playerStats.gameObject);
                break;
            case TypesOfWeapons.PISTOL:
                AddComponent<PistolWeapon>(playerStats.gameObject);
                break;
        }
    }

    private void AddComponent<T> (GameObject go) where T : ShootWeapon{
        T getComponent = go.GetComponent<T>() ?? go.AddComponent<T>();
        getComponent.WeaponStats = WeaponScriptableObject;
        go.GetComponent<PlayerColdre>().ChangeWeapon(getComponent);
    }

    public override string GetInteractableName() {
        return "a arma " + WeaponScriptableObject.name;
    }
}
