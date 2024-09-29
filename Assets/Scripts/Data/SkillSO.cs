using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new SkillSO", menuName = "Scriptable Objects/SkillSO")]
public class SkillSO : ScriptableObject
{
    [Header("Info")]
    public int ID;
    public string Name;
    public string Description;
    public float CoolTime;
    public Sprite Icon;
    public SkillBase Prefab;
}
