using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAmmo : MonoBehaviour
{
    private Weapon weapon;
    
    private void Start()
    {
        weapon = GetComponent<Weapon>();
        RefillAmmo();
    }

    //consume our ammo when we shoot
    public void ConsumeAmmo()
    {
        if(weapon.UseMagazine)
        {
            weapon.CurrentAmmo -= 1;  
        }
    }

    //Return true if we have enough ammo
    public bool CanUseWeapon()
    {
        if(weapon.CurrentAmmo > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //Refill our weapon with ammo
    public void RefillAmmo()
    {
        if(weapon.UseMagazine)
        {
            weapon.CurrentAmmo = weapon.MagazineSize;
        }
    }
}
