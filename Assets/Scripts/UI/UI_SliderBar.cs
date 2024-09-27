using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_SliderBar : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI txtValue;

    Slider sldHealthBar; 

    private void Awake()
    {
        sldHealthBar = GetComponent<Slider>();
    }

    public void UpdateSliderBar(float current, float max, bool bPrintFloat)
    {
        float amount = current / max;
        
        sldHealthBar.value = amount;
        if (bPrintFloat)
            txtValue.text = $"{current:F2} / {max:F2}";
        else
            txtValue.text = $"{current} / {max}";
    }
}
