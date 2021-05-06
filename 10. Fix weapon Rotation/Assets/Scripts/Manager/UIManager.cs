using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [Header("Setting")]
    [SerializeField] private Image healthBar;
    [SerializeField] private TextMeshProUGUI currentHealthTMP;
    [SerializeField] private Image shieldBar;
    [SerializeField] private TextMeshProUGUI currentShieldTMP;

    [Header("Weapon")]
    [SerializeField] private TextMeshProUGUI cuurentAmmoTMP;

    private float playerCurrentHealth;
    private float playerMaxHealth;
    private float playerCurrentShield;
    private float playerMaxShield;

    private int playerCurrentAmmo;
    private int playerMaxAmmo;

    private void Update()
    {
        InternalUpdate();
    }

    public void UpdateHealth(float currentHealth, float maxHealth)
    {
        playerCurrentHealth = currentHealth;
        playerMaxHealth = maxHealth;
    }

    public void UpdateShield(float currentShield, float maxShield)
    {
        playerCurrentShield = currentShield;  
        playerMaxShield = maxShield;
    }

    public void UpdateAmmo(int curretnAmmo, int maxAmmo)
    {
        playerCurrentAmmo = curretnAmmo;
        playerMaxAmmo = maxAmmo;
    }

    private void InternalUpdate()
    {
        healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, playerCurrentHealth / playerMaxHealth, 10 * Time.fixedDeltaTime);
        currentHealthTMP.text = playerCurrentHealth.ToString() + "/" + playerMaxHealth.ToString();

        shieldBar.fillAmount = Mathf.Lerp(shieldBar.fillAmount, playerCurrentShield / playerMaxShield, 10 * Time.fixedDeltaTime);
        currentShieldTMP.text = playerCurrentShield.ToString() + "/" + playerMaxShield.ToString();

        //update Ammo
        cuurentAmmoTMP.text = playerCurrentAmmo + "/" + playerMaxAmmo;
    }

    
}
