using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Configs/WeaponConfig")]
public class WeaponConfig : ScriptableObject
{
    public GameObject weaponPrefab;
    public int weaponDamage;
    public float weaponCooldown;
    public float weaponRange;
}
