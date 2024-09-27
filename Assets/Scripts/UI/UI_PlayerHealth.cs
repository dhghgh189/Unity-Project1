using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_PlayerHealth : MonoBehaviour
{
    [SerializeField] Image imgPlayerHealth;
    
    public void SetHealth(float amount)
    {
        imgPlayerHealth.fillAmount = amount;
    }
}
