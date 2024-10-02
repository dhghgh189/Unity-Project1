using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_Game : MonoBehaviour
{
    [SerializeField] Image imgBossIcon;
    [SerializeField] UI_SliderBar healthBar;
    [SerializeField] UI_SliderBar manaBar;
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] GameObject gameClearPanel;

    void Start()
    {
        PlayerController player = GameManager.Instance.Player;
        if (player == null)
            return;

        player.OnDead += ShowGameOverPanel;

        BossController boss = GameManager.Instance.Boss;
        if (boss == null)
            return;

        BossSO data = DataManager.Instance.BossDict[boss.ID];
        imgBossIcon.sprite = data.Icon;
        if (data.needToFilp)
        {
            imgBossIcon.rectTransform.localScale = new Vector3(-1, imgBossIcon.rectTransform.localScale.y, imgBossIcon.rectTransform.localScale.z);
        }

        boss.OnDead += ShowGameClearPanel;
        boss.OnChangedHP += UpdateHP;
        boss.OnChangedStat += UpdateChanged;

        // init once
        healthBar.UpdateSliderBar(boss.HP, boss.MaxHP, true);
        manaBar.UpdateSliderBar(boss.MP, boss.MaxMP, false);

        gameOverPanel.SetActive(false);
        gameClearPanel.SetActive(false);
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
        healthBar.UpdateSliderBar(hp, maxHp, true);
    }

    public void OnClickedBtnTitle()
    {
        ExSceneManager.Instance.LoadScene("Title");
    }

    public void OnClickedBtnQuit()
    {
        Application.Quit();
    }

    public void ShowGameClearPanel()
    {
        gameClearPanel.SetActive(true);
    }

    public void ShowGameOverPanel()
    {
        gameOverPanel.SetActive(true);
    }

    private void OnDisable()
    {
        //BossController boss = FindObjectOfType<BossController>();
        //if (boss != null)
        //{
        //    boss.OnChangedHP -= UpdateHP;
        //    boss.OnChangedStat -= UpdateChanged;
        //}

        if (GameManager.Instance != null)
        {
            PlayerController player = GameManager.Instance.Player;
            if (player != null)
            {
                player.OnDead -= ShowGameOverPanel;
            }

            BossController boss = GameManager.Instance.Boss;
            if (boss != null)
            {
                boss.OnDead -= ShowGameClearPanel;
                boss.OnChangedHP -= UpdateHP;
                boss.OnChangedStat -= UpdateChanged;
            }
        }
    }
}
