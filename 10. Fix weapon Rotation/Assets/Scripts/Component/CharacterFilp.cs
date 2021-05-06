using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterFilp : CharacterMovement
{
    public enum FilpMode
    {
        MovementDirection,
        WeaponDirection
    }

    [SerializeField] private FilpMode flipMode = FilpMode.MovementDirection;
    [SerializeField] private float threshold = 0.1f;

    //Reference if our character is facing Right
    public bool FacingRight { get; set;}

    private void Awake()
    {
        FacingRight = true;
    }

    protected override void HandleAbility()
    {
        base.HandleAbility();
        FaceDirection(controller.CurrentMovement.x);

    }

    private void FilpToWeaponDirection()
    {
        if(characterWeapon != null)
        {
            float weaponAngle = characterWeapon.weaponAim.currentAimAngleAbsolute;

            if(weaponAngle > 90 || weaponAngle < -90)
            {
                FaceDirection(-1);
            }
            else
            {
                FaceDirection(1);
            }
        }
    }

    private void FaceDirection(float newDirection)
    {
        if(newDirection == 1)
        {
            character.CharacterSprite.transform.localScale = new Vector3(1, 1, 1);
            FacingRight = true;
        }
        else
        {
            character.CharacterSprite.transform.localScale = new Vector3(-1, 1, 1);
            FacingRight = false;
        }
    }
}
