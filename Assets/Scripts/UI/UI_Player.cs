using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Player : MonoBehaviour
{
    [SerializeField] Image imgPlayerIcon;
    [SerializeField] RectTransform playerHealthGroup;
    [SerializeField] UI_PlayerHealth healthPrefab;

    List<UI_PlayerHealth> listPlayerHealth = new List<UI_PlayerHealth>();

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
    }
}
