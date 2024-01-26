using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewStats", menuName = "Character/Stats", order = 1)]
public class CharacterStats : ScriptableObject
{
    [field: SerializeField, Range(0f, 10f)] public float Speed {  get; private set; }
}
