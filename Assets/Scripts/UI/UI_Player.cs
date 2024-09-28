using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Player : MonoBehaviour
{
    [SerializeField] Image imgPlayerIcon;
    [SerializeField] UI_SliderBar healthBar;
    [SerializeField] UI_SliderBar manaBar;
    [SerializeField] TextMeshProUGUI txtCoins;

    void Start()
    {
        GameObject go = GameObject.FindGameObjectWithTag("Player");
        if (go == null)
            return;          

        PlayerController player = go.GetComponent<PlayerController>();
        if (player == null)
            return;

        imgPlayerIcon.sprite = DataManager.Instance.PlayerDict[player.Data.ID].Icon;
        if (player.Data.NeedToFlip)
        {
            imgPlayerIcon.rectTransform.localScale = new Vector3(-1, imgPlayerIcon.rectTransform.localScale.y, imgPlayerIcon.rectTransform.localScale.z);
        }

        GameManager.Instance.Data.PlayerData.OnChangedStat += UpdateChanged;
        GameManager.Instance.Data.OnChangeCoin += UpdateChanged;

        // init once
        healthBar.UpdateSliderBar(player.Data.HP, player.Data.MaxHP, true);
        manaBar.UpdateSliderBar(player.Data.MP, player.Data.MaxMP, false);
        txtCoins.text = $"{GameManager.Instance.Data.Coins}";
    }

    public void UpdateChanged(Enums.EEvents eEvent, float current, float max)
    {
        switch (eEvent)
        {
            case Enums.EEvents.ChangedHP:
                {                  
                    healthBar.UpdateSliderBar(current, max, true);
                }
                break;
            case Enums.EEvents.ChangedMP:
                {
                    manaBar.UpdateSliderBar(current, max, false);
                }
                break;
            case Enums.EEvents.ChangedCoin:
                {
                    txtCoins.text = $"{current}";
                }
                break;
        }
    }

    private void OnDisable()
    {
        GameManager.Instance.Data.PlayerData.OnChangedStat -= UpdateChanged;
        GameManager.Instance.Data.OnChangeCoin -= UpdateChanged;
    }
}
