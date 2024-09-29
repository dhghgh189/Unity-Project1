using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new PlayerSO", menuName = "Scriptable Objects/PlayerSO")]
public class PlayerSO : ScriptableObject
{
    [Header("Info")]
    public int ID;
    public string Name;
    [TextArea(1, 3)] public string Description;

    [Header("Shape")]
    public RuntimeAnimatorController AnimController;
    public Vector2 ColliderOffset;
    public Vector2 ColliderSize;
    public Sprite Icon;
    public bool needToFilp = false;

    [Header("Stats")]
    public float Attack;
    public float MaxHP;
    public float MaxMP;
    public float UtilAmount;

    [Header("Skills")]
    public List<int> useSkillsID;
}
