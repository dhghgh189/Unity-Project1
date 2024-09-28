using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_Cheat : MonoBehaviour
{
    [SerializeField] int CheatCoinAmount;

    [SerializeField] TextMeshProUGUI txtCoinButton;

    private void Awake()
    {
        // init
        txtCoinButton.text = $"Coin + {CheatCoinAmount}";
    }

    public void UseCheat_Coin()
    {
        GameManager.Instance.Data.Coins += CheatCoinAmount;
    }
}
