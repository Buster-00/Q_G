using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private float initalHealth = 10f;
    [SerializeField] private float maxHealth = 10f;

    [Header("Shiled")]
    [SerializeField] private float initalShield = 10f;
    [SerializeField] private float maxShield = 10f;


    [Header("Setting")]
    [SerializeField] private bool destroyObject;

    private Character character;
    private CharacterController controller;
    private Collider2D collider2D;
    private SpriteRenderer spriteRenderer;
    private bool isShieldBroken;

    //Controls the current Health of the object
    public float CurrentHealth { get; set; }

    public float CurrentShield { get; set; }

    private void Awake()
    {
        character = GetComponent<Character>();
        collider2D = GetComponent<Collider2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        controller = GetComponent<CharacterController>();

        CurrentHealth = initalHealth;
        CurrentShield = initalShield;
    
        isShieldBroken = false;

        UIManager.Instance.UpdateHealth(initalHealth, maxHealth);
        UIManager.Instance.UpdateShield(initalShield,maxShield);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            TakeDamage(1);
        }
    }

    //Take the amount of damage with pass in parameters
    public void TakeDamage(int damage)
    {
        if(CurrentHealth <= 0)
        {
            return;
        }

        if(!isShieldBroken && character != null)
        {
            CurrentShield -= damage;
            UIManager.Instance.UpdateHealth(CurrentHealth, maxHealth);
            UIManager.Instance.UpdateShield(CurrentShield,maxShield);

            if(CurrentShield <= 0)
            {
                isShieldBroken = true;
            }

            return;
        }
        
        CurrentHealth -= damage;

        UIManager.Instance.UpdateHealth(CurrentHealth, maxHealth);
        UIManager.Instance.UpdateShield(CurrentShield,maxShield);

        if(CurrentHealth <= 0)
        {
            Die();
        }

    }

    //kills the game object
    private void Die()
    {
        if(character != null)
        {
            collider2D.enabled = false;
            spriteRenderer.enabled = false;

            character.enabled = false;
            controller.enabled = false;

        }

        if(destroyObject)
        {
            DestoryObject();
        }
    }

    //Revive this game object
    public void Revive()
    {
        if(character != null)
        {
            collider2D.enabled = true;
            spriteRenderer.enabled = true;

            character.enabled = true;
            controller.enabled = true;
        }

        gameObject.SetActive(true);
        CurrentHealth = initalHealth;
        UIManager.Instance.UpdateHealth(CurrentHealth,maxHealth);
    }

    //If destroyObject is selected, we destroy this game object
    private void DestoryObject()
    {
        gameObject.SetActive(false);
    }
}
