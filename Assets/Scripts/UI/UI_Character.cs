using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Character : MonoBehaviour, ISelectHandler, IPointerClickHandler
{
    int _characterID;
    UI_Select _parent;

    [SerializeField] Color normalColor;
    [SerializeField] Color selectedColor;

    [SerializeField] Image imgBackGround;
    [SerializeField] Image imgIcon;

    public int CharactedID { get { return _characterID; } }

    public void SetInfo(UI_Select parent, int characterID)
    {
        _parent = parent;
        _characterID = characterID;
        imgIcon.sprite = DataManager.Instance.PlayerDict[characterID].Icon;
    }

    public void OnSelect(BaseEventData eventData)
    {
        _parent.SetCharacter(this);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _parent.SetCharacter(this);
    }

    public void ChangeColor(bool bSelected)
    {
        if (bSelected)
        {
            imgBackGround.color = selectedColor;
        }
        else
        {
            imgBackGround.color = normalColor;
        }
    }
}
