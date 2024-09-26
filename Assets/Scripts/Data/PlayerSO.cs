using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

[CreateAssetMenu(fileName = "new PlayerSO", menuName = "Scriptable Objects/PlayerSO")]
public class PlayerSO : ScriptableObject
{
    [Header("Info")]
    public int ID;
    public string Name;
    public string Description;

    [Header("Shape")]
    public AnimatorController AnimController;
    public Vector2 ColliderOffset;
    public Vector2 ColliderSize;

    [Header("Stats")]
    public float Attack;
    public float MaxHP;
    public float MaxMP;
    public float CooldownAmount;
    public float MPRegenAmount;
}
