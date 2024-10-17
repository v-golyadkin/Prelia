using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveInventory : MonoBehaviour
{
    private int _activeSlotIndexNumber = 0;

    private PlayerActions _playerActions;

    private void Awake()
    {
        _playerActions = new PlayerActions();
    }

    private void Start()
    {
        _playerActions.Inventory.Keyboard.performed += context => ToggleActiveSlot((int)context.ReadValue<float>());

        ToggleActiveHighlight(0);
    }

    private void OnEnable()
    {
        _playerActions.Enable();
    }

    private void ToggleActiveSlot(int numberValue)
    {
        ToggleActiveHighlight(numberValue - 1);
    }

    private void ToggleActiveHighlight(int indexNumber)
    {
        _activeSlotIndexNumber = indexNumber;

        foreach(Transform inventorySlot in this.transform)
        {
            inventorySlot.GetChild(0).gameObject.SetActive(false);
        }

        this.transform.GetChild(indexNumber).GetChild(0).gameObject.SetActive(true);

        ChangeActiveWeapon();
    }

    private void ChangeActiveWeapon()
    {
        if(ActiveWeapon.Instance.CurrentActiveWeapon != null)
        {
            Destroy(ActiveWeapon.Instance.CurrentActiveWeapon.gameObject);
        }

        if (transform.GetChild(_activeSlotIndexNumber).GetComponentInChildren<InventorySlot>().GetWeapon() == null)
        {
            ActiveWeapon.Instance.WeaponNull();
            return;
        }

        var weaponToSpawn = transform.GetChild(_activeSlotIndexNumber).GetComponentInChildren<InventorySlot>().GetWeapon().weaponPrefab;
        
        var newWeapon = Instantiate(weaponToSpawn, ActiveWeapon.Instance.transform.position, Quaternion.identity);

        ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, 0, 0);
        newWeapon.transform.parent = ActiveWeapon.Instance.transform;

        ActiveWeapon.Instance.NewWeapon(newWeapon.GetComponent<MonoBehaviour>());
    }
}
