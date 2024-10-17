using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlot : MonoBehaviour
{
    [SerializeField] private WeaponConfig _weapon;

    public WeaponConfig GetWeapon() 
    { 
        return _weapon; 
    }
}
