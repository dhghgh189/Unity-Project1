using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Skill : MonoBehaviour
{
    [SerializeField] Image imgIcon;
    [SerializeField] TextMeshProUGUI txtKey;

    public void SetInfo(int skillID, int index)
    {
        imgIcon.sprite = DataManager.Instance.SkillDict[skillID].Icon;
        txtKey.text = $"{GameManager.Instance.Player.SkillKeys[index]}";
    }
}
