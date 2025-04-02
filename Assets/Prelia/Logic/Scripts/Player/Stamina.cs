using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Stamina : MonoBehaviour
{
    [SerializeField] private Sprite _fullStaminaSprite, _emptyStaminaSprite;
    [SerializeField] private int _timeBetweenStaminaRefresh = 3;

    [SerializeField] private Transform _staminaContainer;

    public int CurrentStamina => _currentStamina;

    private int _currentStamina, _maxStamina = 3;

    private void Start()
    {
        _currentStamina = _maxStamina;
    }

    public void UseStamina()
    {
        _currentStamina--;
        UpdateStaminaUI();
    }

    public void RefreshStamina()
    {
        if(_currentStamina < _maxStamina)
        {
            _currentStamina++;
        }
        UpdateStaminaUI();
    }

    private IEnumerator RefreshStaminaRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(_timeBetweenStaminaRefresh);
            RefreshStamina();
        }
    }

    private void UpdateStaminaUI()
    {
        if(_staminaContainer == null)
        {
            _staminaContainer = GameObject.Find("Stamina Container").transform;
        }

        for(int i = 0; i < _maxStamina; i++)
        {
            if(i < _currentStamina)
            {
                _staminaContainer.GetChild(i).GetComponent<Image>().sprite = _fullStaminaSprite;
            }
            else
            {
                _staminaContainer.GetChild(i).GetComponent<Image>().sprite = _emptyStaminaSprite;
            }
        }

        if(_currentStamina < _maxStamina)
        {
            StopAllCoroutines();
            StartCoroutine(RefreshStaminaRoutine());
        }
    }
}
