using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleHUD : MonoBehaviour
{
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private TMP_Text _levelText;
    [SerializeField] private Slider _hpSlider;

    public void SetHUD(Unit unit)
    {
        _nameText.text = unit.unitName;
        _levelText.text = "Lvl" + unit.level.ToString();
        _hpSlider.maxValue = unit.maxHP;
        _hpSlider.value = unit.currentHP;
    }

    public void SetHP(int hp)
    {
        _hpSlider.value = hp;
    }
}
