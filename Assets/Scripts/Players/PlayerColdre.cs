using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerColdre : MonoBehaviour
{
    [Header("Initial kit")]
    [SerializeField]
    private List<ShootWeapon> InitialKit;

    [Header("Components")]
    [SerializeField]
    private List<Image> WeaponsImages;
    [SerializeField]
    private Image AmmoImage;
    [SerializeField]
    private TextMeshProUGUI Text;

    [HideInInspector]
    public List<ShootWeapon> Weapons;
    public bool IsPrimary = true;

    private void Start() {
        foreach (ShootWeapon weapon in InitialKit)
        {
            ChangeWeapon(weapon);
        }

        SwitchWeapon();
    }

    private void Update()
    {
        if(Weapons.Count > 0 && !Weapons[IsPrimary ? 0 : 1].IsKnife())
        {
            WeaponScriptableObject weaponStats = Weapons[IsPrimary ? 0 : 1].WeaponStats;
            if(weaponStats.Munition > 0)
            {
                Text.text = weaponStats.Munition + " / " + weaponStats.MaxMunition;
            }
            else
            {
                Text.text = "Reloading...";
            }
        }
        else
        {
            Text.text = "";
        }
    }

    public void ChangeWeapon(ShootWeapon newWeapon) {
        if (Weapons.Count < 2 && !Weapons.Exists(value => value.Equals(newWeapon))) {
            Weapons.Add(newWeapon);
            WeaponsImages[Weapons.Count - 1].sprite = newWeapon.WeaponStats.WeaponSprite;
            WeaponsImages[Weapons.Count - 1].enabled = true;
            if(newWeapon.WeaponStats.AmmoSprite != null)
            {
                AmmoImage.enabled = true;
                AmmoImage.sprite = newWeapon.WeaponStats.AmmoSprite;
            }
            else
            {
                AmmoImage.enabled = false;
            }

            if (Weapons.Count == 2)
                SwitchWeapon();
        }
        else {
            Debug.Log(IsPrimary);

            Weapons[IsPrimary ? 0 : 1].enabled = false;
            Weapons[IsPrimary ? 0 : 1] = newWeapon;
            WeaponsImages[0].sprite = newWeapon.WeaponStats.WeaponSprite;
            if (newWeapon.WeaponStats.AmmoSprite != null)
            {
                AmmoImage.enabled = true;
                AmmoImage.sprite = newWeapon.WeaponStats.AmmoSprite;
            }
            else
            {
                AmmoImage.enabled = false;
            }
            Weapons[IsPrimary ? 0 : 1].enabled = true;
        }
    }

    public void SwitchWeapon() {
        if (Weapons.Count < 2) return;

        IsPrimary = !IsPrimary;
        Weapons[0].enabled = IsPrimary;
        Weapons[1].enabled = !IsPrimary;
        if (Weapons[IsPrimary ? 0 : 1].WeaponStats.AmmoSprite != null)
        {
            AmmoImage.enabled = true;
            AmmoImage.sprite = Weapons[IsPrimary ? 0 : 1].WeaponStats.AmmoSprite;
        } else
        {
            AmmoImage.enabled = false;
        }
        SwitchSprite();
    }

    private void SwitchSprite()
    {
        Sprite sprite = WeaponsImages[0].sprite;
        WeaponsImages[0].sprite = WeaponsImages[1].sprite;
        WeaponsImages[1].sprite = sprite;
    }
}
