using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveWeapon : MonoBehaviour
{
    [SerializeField] private Sword sword;

    public static ActiveWeapon Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public Sword GetActiveWeapon()
    {
        return sword;
    }
}
