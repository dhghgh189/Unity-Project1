using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new SkillSO", menuName = "Scriptable Objects/SkillSO")]
public class SkillSO : ScriptableObject
{
    [Header("Info")]
    public int ID;
    public string Name;
    [TextArea(1, 5)] public string Description;
    public Enums.ESkillSlot Slot;
    public float CoolTime;
    // 소비 마나
    public float MPAmount;
    public Sprite Icon;
    public SkillBase Prefab;
}
