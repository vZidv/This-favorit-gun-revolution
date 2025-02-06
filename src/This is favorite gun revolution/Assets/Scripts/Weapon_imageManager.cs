using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon_imageManager : MonoBehaviour
{
    public Sprite[] weaponSprits;
    [Header("Don't touch")]
    public List<string> WeaponName;
    private Image weaponImage;

    Player_Weapon playerWeapon;

    Color c = Color.white;
    private void Start()
    {
        playerWeapon = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Weapon>();
        weaponImage = gameObject.GetComponent<Image>();
        c.a = 0;
        weaponImage.color = c;

        for (int i = 0; i < weaponSprits.Length; i++)
        {
            switch (i)
            {
                case 0:
                    WeaponName.Add("Snip_rifle");
                    break;
                case 1:
                    WeaponName.Add("Mac-10");
                    break;
                case 2:
                    WeaponName.Add("Pistol");
                    break;
                case 3:
                    WeaponName.Add("Rifle");
                    break;
                case 4:
                    WeaponName.Add("ShotGun");
                    break;
            }
        }
    }
    public void SwitchWeaponImage(string nameGun)
    {
        int i = 0;
        while(WeaponName[i] != nameGun)
        {
            i++;
        }
        c.a = 1;
        weaponImage.color = c;
        weaponImage.sprite = weaponSprits[i];
    }

}
