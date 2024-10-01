using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Game : MonoBehaviour
{
    [SerializeField] Image imgBossIcon;
    [SerializeField] UI_SliderBar healthBar;
    [SerializeField] UI_SliderBar manaBar;

    void Start()
    {
        GameObject go = GameObject.FindGameObjectWithTag("Boss");
        if (go == null)
            return;          

        BossController boss = go.GetComponent<BossController>();
        if (boss == null)
            return;

        BossSO data = DataManager.Instance.BossDict[boss.ID];
        imgBossIcon.sprite = data.Icon;
        if (data.needToFilp)
        {
            imgBossIcon.rectTransform.localScale = new Vector3(-1, imgBossIcon.rectTransform.localScale.y, imgBossIcon.rectTransform.localScale.z);
        }

        boss.OnChangedHP += UpdateHP;
        boss.OnChangedStat += UpdateChanged;

        // init once
        healthBar.UpdateSliderBar(boss.HP, boss.MaxHP, false);
        manaBar.UpdateSliderBar(boss.MP, boss.MaxMP, false);
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
        }
    }

    // HP´Â ºÐ¸®
    public void UpdateHP(float hp, float maxHp)
    {
        healthBar.UpdateSliderBar(hp, maxHp, false);
    }

    private void OnDisable()
    {
        BossController boss = FindObjectOfType<BossController>();
        if (boss != null)
        {
            boss.OnChangedHP -= UpdateHP;
            boss.OnChangedStat -= UpdateChanged;
        }
    }
}
