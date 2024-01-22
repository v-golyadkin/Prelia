using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Visual : MonoBehaviour
{
    private Flash flash;

    private void Awake()
    {
        flash = GetComponent<Flash>();
    }

    public Flash GetFlash()
    {
        return flash;
    }

}
