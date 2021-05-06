using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float timeBtwShots = 0.5f;

    [Header("Weapon")]
    [SerializeField] private bool useMagazine = true;
    [SerializeField] public int magazineSize = 30;
    [SerializeField] private bool autoReload= true;

    [Header("Recoil")]
    [SerializeField] private bool useRecoil = true;
    [SerializeField] private int recoilForce = 30; 

    //Reference of the Character that controls this weapon
    public Character WeaponOwner{get; set;}

    //Current Ammo we have
    public int CurrentAmmo {get; set;}

    //Reference to weapon ammo
    public WeaponAmmo weaponAmmo {get; set;}

    //Returns if this weapon use Magazine
    public bool UseMagazine
    {
        get{ return useMagazine;}
    }

    public int MagazineSize => magazineSize;

    //Return if we can shoot now
    public bool CanShoot{ get; set;}

    //Internal
    private float nextShotTime;
    private CharacterController controller;     //To konw the character is facing which side for Recoil

    private void Update()
    {
        WeaponCanShoot();
        RotateWeapon();
        //Debug.Log(CurrentAmmo);
    }

    private void Awake()
    {
        weaponAmmo = GetComponent<WeaponAmmo>();
        CurrentAmmo = magazineSize;
    }

    public void TriggerShot()
    {
        StartShooting();
    }

    //Makes our weapon stop working
    public void StopWeapon()
    {
        if(useRecoil)
        {
            controller.ApplyRecoil(Vector2.one, 0f);
        }
    }

    //Activate our weapon in order to shoot 
    private void StartShooting()
    {
        if(useMagazine)
        {
            if(weaponAmmo != null)
            {
                if(weaponAmmo.CanUseWeapon())
                {
                    RequestShoot();
                }
                else
                {
                    if(autoReload)
                    {
                        Reload();
                    }
                }
            }
            else
            {
                RequestShoot();
            }
        }
        

    }

    //Makes our weapon start shooting
    private void RequestShoot()
    {
        if(!CanShoot)
        {
            return;
        }

        if(useRecoil)
        {
            Recoil();
        }

        weaponAmmo.ConsumeAmmo();

        CanShoot= false;
    }

    //Apply a force to our movement when we start shoot.
    private void Recoil()
    {
        if(WeaponOwner != null)
        {
            if(WeaponOwner.GetComponent<CharacterFilp>().FacingRight)
            {
                controller.ApplyRecoil(Vector2.left, recoilForce);
            }
            else
            {
                controller.ApplyRecoil(Vector2.right, recoilForce);
            }
        }
    }

    //Controls the next time we can shoot
    private void WeaponCanShoot()
    {
        if(Time.time > nextShotTime) //Actual time in the game Greater than fire reate
        {
            CanShoot = true;
            nextShotTime = Time.time + timeBtwShots;
        }

    }

    //Reference the owner of this weapon
    public void SetOwner(Character owner)
    {
       WeaponOwner = owner;
       controller = WeaponOwner.GetComponent<CharacterController>();
    }

    public void Reload()
    {
       if(weaponAmmo != null)
       {
           if(useMagazine)
           {
               weaponAmmo.RefillAmmo();
           }
       }
    }

    private void RotateWeapon()
    {
        if(WeaponOwner.GetComponent<CharacterFilp>().FacingRight)
        {
            transform.localScale = new Vector3(1,1,1);
        }
        else
        {
            transform.localScale = new Vector3(1,1,1);
        }
    }
   
}
