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

        player.OnChangedHP += UpdateHP;

        GameManager.Instance.Data.PlayerData.OnChangedStat += UpdateChanged;
        GameManager.Instance.Data.OnChangeCoin += UpdateChanged;

        // init once
        healthBar.UpdateSliderBar(player.HP, player.Data.MaxHP, true);
        manaBar.UpdateSliderBar(player.Data.MP, player.Data.MaxMP, false);
        txtCoins.text = $"{GameManager.Instance.Data.Coins}";
    }

    public void UpdateChanged(Enums.EEvents eEvent, float current, float max)
    {
        switch (eEvent)
        {
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

    // HP�� �и�
    public void UpdateHP(float hp, float maxHp)
    {
        healthBar.UpdateSliderBar(hp, maxHp, true);
    }

    private void OnDisable()
    {
        PlayerController player = FindObjectOfType<PlayerController>();
        if (player != null)
        {
            player.OnChangedHP -= UpdateHP;
        }

        if (GameManager.Instance != null)
        {
            GameManager.Instance.Data.PlayerData.OnChangedStat -= UpdateChanged;
            GameManager.Instance.Data.OnChangeCoin -= UpdateChanged;
        }       
    }
}
