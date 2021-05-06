using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAim : MonoBehaviour
{
   [SerializeField] private GameObject reticlePrefabs;

    //Returns the current absolute angle
    public float currentAimAngleAbsolute{ get; set;}

    //returns the current angel
    public float currentAimAngle {get; set;}

    private Weapon weapon;
   private Camera mainCamera;
   private GameObject reticle;

   private Vector3 direction;
   private Vector3 mousePosition;
   private Vector3 reticlePosition;
   private Vector3 currentAim = Vector3.zero;
   private Vector3 currentAimAbsolute = Vector3.zero;
   private Quaternion initialRotaiton;
   private Quaternion lookRotation;

   private void Start()
   {
       Cursor.visible = false;
       weapon = GetComponent<Weapon>();
       initialRotaiton = transform.rotation;

       mainCamera = Camera.main;
       reticle = Instantiate(reticlePrefabs);
   }

   private void Update()
   {
       GetMousePosition();
       MoveReticle();
       RotateWeapon();
   }

   //Get the exact mouse position in order to aim
   private void GetMousePosition()
   {
       //Get Mouse position
       mousePosition = Input.mousePosition;
       mousePosition.z = 5f; // Set this value to ensure the camera always stays infront to view everything in game.

       //Get world space position
       direction = mainCamera.ScreenToWorldPoint(mousePosition);
       direction.z = transform.position.z;
       reticlePosition = direction;

       currentAimAbsolute = direction - transform.position;
       
       if(weapon.WeaponOwner.GetComponent<CharacterFilp>().FacingRight)
       {
           currentAim = direction - transform.position;
       }
       else
       {
           currentAim = transform.position - direction;
       }
   }

   public void RotateWeapon()
   {
       if(currentAim != Vector3.zero && direction != Vector3.zero)
       {
           //Get Angle
           currentAimAngle = Mathf.Atan2(currentAim.y,currentAim.x) * Mathf.Rad2Deg;
           currentAimAngleAbsolute = Mathf.Atan2(currentAimAbsolute.y, currentAimAbsolute.x) * Mathf.Rad2Deg;

           //clamp for rotation
           if(weapon.WeaponOwner.GetComponent<CharacterFilp>().FacingRight)
           {
               currentAimAngle = Mathf.Clamp(currentAimAngle, -180, 180);
           }
           else
           {
               currentAimAngle = Mathf.Clamp(currentAimAngle, -180, 180);
           }

           //Apply the angle
           lookRotation = Quaternion.Euler(currentAimAngle * Vector3.forward);
           transform.rotation = lookRotation;
       }
       else
       {
           currentAimAngle = 0f; // If the mouse is not moving at all at the beginning
           transform.rotation = initialRotaiton;
       }
   }

   //Moves reticle towards our mouse position
   private void MoveReticle()
   {
       reticle.transform.rotation = Quaternion.identity; //Set the normal rotation
       reticle.transform.position = reticlePosition;
   }
}
