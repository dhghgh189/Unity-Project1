using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_Select : MonoBehaviour
{
    [SerializeField] UI_Character characterUIPrefab;
    [SerializeField] RectTransform selectPanel;
    [SerializeField] Vector2 characterUISpacing;
    [SerializeField] TextMeshProUGUI txtInfo;

    List<UI_Character> listCharacterUI = new List<UI_Character>();

    UI_Character _selectedElement;

    int _selectedCharacterID = -1;
    Vector2 characterUISize;

    void Awake()
    {
        UI_Character characterUI;
        for (int i = 0; i < DataManager.Instance.PlayerDict.Count; i++)
        {
            characterUI = Instantiate(characterUIPrefab);
            characterUI.name = DataManager.Instance.PlayerDict[i].name;
            characterUI.SetInfo(this, DataManager.Instance.PlayerDict[i].ID);
            characterUI.transform.SetParent(selectPanel);
            listCharacterUI.Add(characterUI);
        }

        // character UI Size
        RectTransform rectTransform = listCharacterUI[0].GetComponent<RectTransform>();
        characterUISize = new Vector2(rectTransform.rect.width, rectTransform.rect.height);
    }

    void Start()
    {
        RectTransform rectTransform;

        // character UI move offset
        // example : (100 / 2) + 10 = 60(move offset)
        Vector2 offset = new Vector2
        (
            (characterUISize.x / 2) + characterUISpacing.x,
            (characterUISize.y / 2) + characterUISpacing.y
        );

        // x coordinate count
        // example : (int)(700 / (100 + 10)) = 6
        int xCount = (int)(selectPanel.rect.width / (characterUISize.x + characterUISpacing.x));

        for (int i = 0; i < listCharacterUI.Count; i++)
        {
            rectTransform = listCharacterUI[i].GetComponent<RectTransform>();

            // set x,y
            // x = offset.x + ((characterUISize.x + characterUISpacing.x) * (i % xCount))
            // x example :
            //      60 + ((100 + 10) * (0 % 6)) = 60
            //      60 + ((100 + 10) * (1 % 6)) = 170
            //      ~~
            //      60 + ((100 + 10) * (6 % 6)) = 60
            // y = -(offset.y + ((characterUISize.y + characterUISpacing.y) * (i / xCount)))
            // y example :
            //      -(60 + ((100 + 10) * (0 / 6))) = -60
            //      -(60 + ((100 + 10) * (1 / 6))) = -60
            //      ~~
            //      -(60 + ((100 + 10) * (6 / 6))) = -170
            rectTransform.anchoredPosition = new Vector2
                (
                    offset.x + ((characterUISize.x + characterUISpacing.x) * (i % xCount)), 
                    -(offset.y + ((characterUISize.y + characterUISpacing.y) * (i / xCount)))
                );
        }

        EventSystem.current.firstSelectedGameObject = listCharacterUI[0].gameObject;
    }

    public void SetCharacter(UI_Character character)
    {
        if (_selectedCharacterID != character.CharactedID)
        {
            // before element color change
            if (_selectedElement != null)
                _selectedElement.ChangeColor(false);

            _selectedElement = character;
            _selectedCharacterID = _selectedElement.CharactedID;

            // current element color change
            _selectedElement.ChangeColor(true);

            UpdateInfoPanel();
        }        
    }

    void UpdateInfoPanel()
    {
        PlayerSO playerData = DataManager.Instance.PlayerDict[_selectedCharacterID];
        txtInfo.text = 
            $"<color=yellow>이름 :</color> {playerData.name}\n<color=yellow>최대 체력 :</color> {playerData.MaxHP}\n{playerData.Description}";   
    }

    public void OnClickBtnSelect()
    {
        GameManager.Instance.StartGame(_selectedCharacterID);
    }
}
