using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_Preparation : MonoBehaviour
{
    [SerializeField] GameObject upgradePanel;
    [SerializeField] GameObject cheatButton;
    [SerializeField] GameObject cheatPanel;
    [SerializeField] GameObject blocker;
    [SerializeField] UI_UpgradeGroup attackUpgradeGroup;
    [SerializeField] UI_UpgradeGroup hpUpgradeGroup;
    [SerializeField] UI_UpgradeGroup utilUpgradeGroup;

    private void Awake()
    {
        HideUpgradePanel();

#if UNITY_EDITOR    
        HideCheatPanel();
        cheatButton.SetActive(true);
#else
        HideCheatPanel();
        cheatButton.SetActive(false);
#endif

        attackUpgradeGroup.OnClickUpgrade += Upgrade;
        hpUpgradeGroup.OnClickUpgrade += Upgrade;
        utilUpgradeGroup.OnClickUpgrade += Upgrade;

        attackUpgradeGroup.Init(Enums.EUpgradeType.AttackPoint);
        hpUpgradeGroup.Init(Enums.EUpgradeType.HP);
        utilUpgradeGroup.Init(Enums.EUpgradeType.Util);
    }

    private void Start()
    {
        GameObject go = GameObject.FindGameObjectWithTag("Player");
        if (go == null)
            return;

        PlayerController player = go.GetComponent<PlayerController>();
        if (player == null)
            return;

        GameManager.Instance.Data.PlayerData.OnUpgradeStat += UpdateUpgrade;

        // init once
        UpdateUpgrade(Enums.EEvents.UpgradeAttackPoint, player.Data.AttackLevel);
        UpdateUpgrade(Enums.EEvents.UpgradeMaxHP, player.Data.HPLevel);
        UpdateUpgrade(Enums.EEvents.UpgradeUtil, player.Data.UtilLevel);
    }

    public void UpdateUpgrade(Enums.EEvents eEvent, int level)
    {
        switch (eEvent)
        {
            case Enums.EEvents.UpgradeAttackPoint:
                attackUpgradeGroup.UpdateLevel(level);
                attackUpgradeGroup.UpdateAmountText($"데미지 증가 : {level * Define.upgradeInfos[(int)Enums.EUpgradeType.AttackPoint].amount}%");
                break;
            case Enums.EEvents.UpgradeMaxHP:
                hpUpgradeGroup.UpdateLevel(level);
                hpUpgradeGroup.UpdateAmountText($"증가된 체력 : {level * Define.upgradeInfos[(int)Enums.EUpgradeType.HP].amount}");
                break;
            case Enums.EEvents.UpgradeUtil:
                utilUpgradeGroup.UpdateLevel(level);
                utilUpgradeGroup.UpdateAmountText($"쿨다운 감소 & 마나 리젠 : {level * Define.upgradeInfos[(int)Enums.EUpgradeType.Util].amount}%");
                break;
        }
    }

    public void ShowUpgradePanel()
    {
        blocker.SetActive(true);
        upgradePanel.SetActive(true);
    }

    public void HideUpgradePanel()
    {
        blocker.SetActive(false);
        upgradePanel.SetActive(false);
    }

    public void ShowCheatPanel()
    {
        blocker.SetActive(true);
        cheatPanel.SetActive(true);
    }

    public void HideCheatPanel()
    {
        blocker.SetActive(false);
        cheatPanel.SetActive(false);
    }

    public void Upgrade(Enums.EUpgradeType type)
    {
        PlayerController player = FindObjectOfType<PlayerController>();
        player?.UpgradeStat(type);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }

    private void OnDisable()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.Data.PlayerData.OnUpgradeStat -= UpdateUpgrade;
        }

        attackUpgradeGroup.OnClickUpgrade -= Upgrade;
        hpUpgradeGroup.OnClickUpgrade -= Upgrade;
        utilUpgradeGroup.OnClickUpgrade -= Upgrade;
    }
}
