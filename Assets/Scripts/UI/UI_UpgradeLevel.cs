using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_UpgradeLevel : MonoBehaviour
{
    [SerializeField] Image imgFill;
    [SerializeField] Color normalColor;
    [SerializeField] Color fillColor;

    public void SetColor(bool bFilled)
    {
        if (bFilled)
        {
            imgFill.color = fillColor;
        }
        else
        {
            imgFill.color = normalColor;
        }
    }
}
