using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDash : CharacterComponent
{
    [SerializeField] float dashDistance = 3f;
    [SerializeField] float dashTimer = 0.1f;

    protected  override void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            dash();
        }

        if(Input.GetKeyUp(KeyCode.Space))
        {
            stopDash();
        }
    }


    private void dash()
    {
        controller.isNormalMove = false;
        controller.MovePosition(transform.position + (Vector3)controller.CurrentMovement.normalized * dashDistance);
     
    }

    private void stopDash()
    {
        controller.isNormalMove = true; 
    }
}
