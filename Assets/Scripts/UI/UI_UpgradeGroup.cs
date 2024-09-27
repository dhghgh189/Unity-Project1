using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using static Cinemachine.DocumentationSortingAttribute;

public class UI_UpgradeGroup : MonoBehaviour
{
    [SerializeField] RectTransform levelGroup;
    [SerializeField] TextMeshProUGUI txtName;
    [SerializeField] TextMeshProUGUI txtButton;
    [SerializeField] TextMeshProUGUI txtLevel;
    [SerializeField] TextMeshProUGUI txtAmount;
    [SerializeField] UI_UpgradeLevel levelUIPrefab;
    [SerializeField] Vector2 levelUIspacing;

    List<UI_UpgradeLevel> _listUpgradeLevels = new List<UI_UpgradeLevel>();
    Vector2 levelUISize;

    Enums.EUpgradeType _type;

    public UnityAction<Enums.EUpgradeType> OnClickUpgrade;

    public void Init(Enums.EUpgradeType type)
    {
        _type = type;

        UI_UpgradeLevel levelUI;
        for (int i = 0; i < Define.upgradeInfos[(int)_type].maxLevel; i++)
        {
            levelUI = Instantiate(levelUIPrefab);
            levelUI.SetColor(false);
            levelUI.transform.SetParent(levelGroup);
            _listUpgradeLevels.Add(levelUI);
        }

        txtName.text = $"{Define.upgradeInfos[(int)_type].name}";
        txtButton.text = $"{Define.upgradeInfos[(int)_type].price} Gold";

        // character UI Size
        RectTransform rectTransform = _listUpgradeLevels[0].GetComponent<RectTransform>();
        levelUISize = new Vector2(rectTransform.rect.width, rectTransform.rect.height);

        InitLayout();
    }

    void InitLayout()
    {
        RectTransform rectTransform;
        float offsetX = (levelUISize.x / 2) + levelUIspacing.x;

        for (int i = 0; i < _listUpgradeLevels.Count; i++)
        {
            rectTransform = _listUpgradeLevels[i].GetComponent<RectTransform>();
            rectTransform.anchoredPosition = new Vector2
                (
                    offsetX + ((levelUISize.x) + levelUIspacing.x) * i,
                    0
                );
        }
    }

    public void UpdateLevel(int level)
    {
        for (int i = 0; i < level; i++)
        {
            if (i < level)
            {
                _listUpgradeLevels[i].SetColor(true);
            }
            else
            {
                _listUpgradeLevels[i].SetColor(false);
            }
        }

        txtLevel.text = $"Lv.{level}";
    }

    public void UpdateAmountText(string text)
    {
        txtAmount.text = text;
    }

    public void OnClickedBtnUpgrade()
    {
        OnClickUpgrade?.Invoke(_type);
    }
}
