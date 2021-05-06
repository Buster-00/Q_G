using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterComponent : MonoBehaviour
{
    protected float horizontalInput;
    protected float verticalInput;
    protected float WalkMoveSpeed;
    protected Vector2 movementNormalized { get; set; }

    protected CharacterController controller;
    protected CharacterMovement characterMovement;
    protected CharacterWeapon characterWeapon;
    protected Animator animator;
    protected Character character;
    

    protected virtual void Start()
    {
        controller = GetComponent<CharacterController>();
        characterMovement = GetComponent<CharacterMovement>();
        characterWeapon  = GetComponent<CharacterWeapon>();
        animator = GetComponent<Animator>();
        character = GetComponent<Character>();
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        HandleAbility();
    }

    //Main method, Here we put the logic of each ability
    protected virtual void HandleAbility()
    {
        InternalInput();
        HandleInput();
    }

    protected virtual void HandleInput()
    {

    }

    protected virtual void InternalInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }
}
