using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Player : MonoBehaviour
{
    [SerializeField] Image imgPlayerIcon;
    [SerializeField] UI_SliderBar healthBar;
    [SerializeField] UI_SliderBar manaBar;

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

        player.Data.OnChangedStat += UpdateChangedStat;

        // init once
        healthBar.UpdateSliderBar(player.Data.HP, player.Data.MaxHP, true);
        manaBar.UpdateSliderBar(player.Data.MP, player.Data.MaxMP, false);
    }

    public void UpdateChangedStat(Enums.EEvents eEvent, float current, float max)
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
        }
    }
}
