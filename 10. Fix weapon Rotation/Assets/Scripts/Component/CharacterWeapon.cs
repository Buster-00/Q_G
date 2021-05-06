using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterWeapon : CharacterComponent
{
    [Header("Weapon Settings")]
    [SerializeField] private Weapon weaponToUse;
    [SerializeField] private Transform weaponHolderPosition;

    public Weapon CurrentWeapon {get;set;}

    //returns the reference to our Current Weapon aim
    public WeaponAim weaponAim {get; set;}

    protected override void Start()
    {
        base.Start();
        EquipWeapon(weaponToUse,weaponHolderPosition);
    }

    protected override void HandleInput()
    {
        if(Input.GetMouseButton(0))
        {
            Shoot();
        }

        if(Input.GetMouseButtonUp(0))
        {
            StopWeapon();
        }

        if(Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }
    }
    public void Shoot()
    {
        if(CurrentWeapon == null)
        {
            return;
        }

        CurrentWeapon.TriggerShot();
        if(character.CharacterType == Character.CharacterTypes.Player)
        {
            UIManager.Instance.UpdateAmmo(CurrentWeapon.CurrentAmmo, CurrentWeapon.MagazineSize);
        }
    }

    //when we stop shooting we stop using our weapon
    public void StopWeapon()
    {
        if(CurrentWeapon == null)
        {
            return;
        }

        CurrentWeapon.StopWeapon();
    }

    public void Reload()
    {
        if(CurrentWeapon == null)
        {
            return;
        }

        CurrentWeapon.Reload();
        if(character.CharacterType == Character.CharacterTypes.Player)
        {
            UIManager.Instance.UpdateAmmo(CurrentWeapon.CurrentAmmo, CurrentWeapon.MagazineSize);
        }
    }

    public void EquipWeapon(Weapon weapon, Transform weaponPosition)
    {
        CurrentWeapon = Instantiate(weapon, weaponPosition.position, weaponPosition.rotation);
        CurrentWeapon.transform.parent = weaponPosition;
        CurrentWeapon.SetOwner(character);
        weaponAim = CurrentWeapon.GetComponent<WeaponAim>();
        
        if(character.CharacterType == Character.CharacterTypes.Player)
        {
            UIManager.Instance.UpdateAmmo(CurrentWeapon.CurrentAmmo, CurrentWeapon.MagazineSize);
        }
    }
}

