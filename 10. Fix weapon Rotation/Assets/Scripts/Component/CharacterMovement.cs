using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : CharacterComponent
{
    [SerializeField] public float walkSpeed = 6f;
    [SerializeField] public float moveSpeed;

    private readonly int movingParameter = Animator.StringToHash("Moving");

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        moveSpeed = walkSpeed;
    }

    protected override void HandleAbility()
    {
        base.HandleAbility();
        MoveCharacter();
        updateAnimation();
    }

    private void MoveCharacter()
    {
        Vector2 movement = new Vector2(horizontalInput, verticalInput);
        Vector2 movementSpeed = movement.normalized * moveSpeed;
        controller.SetMovement(movementSpeed);
    }

    public void ResetSpeed()
    {
        moveSpeed = walkSpeed;
    }

    protected void updateAnimation()
    {
        if(Mathf.Abs(horizontalInput) > 0.1f || Mathf.Abs(verticalInput) > 0.1f)
        {
            if(character.CharacterAnimator != null)
            {
                character.CharacterAnimator.SetBool(movingParameter, true);
            }
        }
        else
        {
            if(character.CharacterAnimator != null)
            {
                character.CharacterAnimator.SetBool(movingParameter, false);
            }
        }
    }

}
