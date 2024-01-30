using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Unit : MonoBehaviour
{
    [SerializeField] public String unitName;
    [SerializeField] public int level;
    [SerializeField] public int damage;
    [SerializeField] public int maxHP;
    [SerializeField] public int currentHP;

    public bool TakeDamege(int damage)
    {
        currentHP -= damage;

        if (currentHP <= 0)
            return true;
        else return false;
    }

    public void Heal(int amount)
    {
        currentHP += amount;

        if(currentHP > maxHP)
            currentHP = maxHP;
    }

}
