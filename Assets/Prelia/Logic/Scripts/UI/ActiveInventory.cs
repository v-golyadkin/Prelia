using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveInventory : PersistentSingleton<ActiveInventory>
{
    private int _activeSlotIndexNumber = 0;

    private PlayerActions _playerActions;

    protected override void Awake()
    {
        base.Awake();

        _playerActions = new PlayerActions();
    }

    private void Start()
    {
        _playerActions.Inventory.Keyboard.performed += OnKeyboardPerformed;
    }

    private void OnEnable()
    {
        _playerActions?.Enable();       
    }

    private void OnDisable()
    {
        _playerActions?.Disable();
    }

    private void OnDestroy()
    {
        if(_playerActions != null)
        {
            _playerActions.Inventory.Keyboard.performed -= OnKeyboardPerformed;
        }
    }

    private void OnKeyboardPerformed(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        ToggleActiveSlot((int)context.ReadValue<float>());
    }

    private void ToggleActiveSlot(int numberValue)
    {
        ToggleActiveHighlight(numberValue - 1);
    }

    private void ToggleActiveHighlight(int indexNumber)
    {
        _activeSlotIndexNumber = indexNumber;

        if(this.transform.childCount == 0)
        {
            Debug.LogWarning("No inventory slots found!");
            return;
        }

        if(indexNumber < 0 || indexNumber >= this.transform.childCount)
        {
            Debug.Log($"Invalid slot index: {indexNumber}");
            return;
        }

        foreach(Transform inventorySlot in this.transform)
        {
            if(inventorySlot.childCount > 0)
            {
                inventorySlot.GetChild(0).gameObject.SetActive(false);
            }
        }

        Transform selectedSlot = this.transform.GetChild(indexNumber);
        if(selectedSlot.childCount > 0)
        {
            this.transform.GetChild(indexNumber).GetChild(0).gameObject.SetActive(true);
        }
        
        ChangeActiveWeapon();
    }

    private void ChangeActiveWeapon()
    {
        if(ActiveWeapon.Instance.CurrentActiveWeapon != null)
        {
            Destroy(ActiveWeapon.Instance.CurrentActiveWeapon.gameObject);
        }

        Transform childTransform = transform.GetChild(_activeSlotIndexNumber);
        InventorySlot inventorySlot = childTransform.GetComponentInChildren<InventorySlot>();
        WeaponConfig weaponInfo = inventorySlot.GetWeaponInfo();
        GameObject weaponToSpawn = weaponInfo.weaponPrefab;

        if(weaponInfo == null)
        {
            ActiveWeapon.Instance.WeaponNull();
            return;
        }

        var newWeapon = Instantiate(weaponToSpawn, ActiveWeapon.Instance.transform);

        //ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, 0, 0);
        //newWeapon.transform.parent = ActiveWeapon.Instance.transform;

        ActiveWeapon.Instance.NewWeapon(newWeapon.GetComponent<MonoBehaviour>());
    }

    public void EquipStartingWeapon()
    {
        ToggleActiveHighlight(0);
    }
}
