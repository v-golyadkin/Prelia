using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EconomyManager : Singleton<EconomyManager>
{
    [SerializeField] private TMP_Text _goldCoinAmountText;

    private int _currentGoldCoinCount;

    public void AddCoin() => AddCoins(1);

    public void AddCoins(int amount)
    {
        if (amount < 0)
        {
            return;
        }

        _currentGoldCoinCount += amount;

        UpdateCoinCounterText();
    }

    private void UpdateCoinCounterText()
    {
        if(_goldCoinAmountText == null)
        {
            _goldCoinAmountText = GameObject.Find("Gold Coin Amount Text").GetComponent<TMP_Text>();
        }

        _goldCoinAmountText.text = _currentGoldCoinCount.ToString("D3");
    }
}
