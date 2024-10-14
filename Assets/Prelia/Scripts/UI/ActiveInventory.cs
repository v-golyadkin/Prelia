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
    }
}
