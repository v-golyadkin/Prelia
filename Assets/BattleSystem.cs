using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public enum BattleState
{
    Start,
    PlayerTurn,
    EnemyTurn,
    Won,
    Lost
}
public class BattleSystem : MonoBehaviour
{
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private GameObject _enemyPrefab;

    [SerializeField] private Transform _playerBattleStation;
    [SerializeField] private Transform _enemyBattleStation;

    private Unit _playerUnit;
    private Unit _enemyUnit;

    [SerializeField] private BattleHUD _playerHUD;
    [SerializeField] private BattleHUD _enemyHUD;

    [SerializeField] private TMP_Text _dialogueText;

    public BattleState state;

    private void Start()
    {
        state = BattleState.Start;
        StartCoroutine(InitBattle());
    }

    IEnumerator InitBattle()
    {
        GameObject playerGO = Instantiate(_playerPrefab, _playerBattleStation);
        playerGO.GetComponent<PlayerController>().enabled = false;
        //playerGO.GetComponentInChildren<PlayerVisual>().enabled = false;

        playerGO.GetComponent<Animator>().SetFloat("directionX", 1);

        _playerUnit = playerGO.GetComponent<Unit>();

        GameObject enemyGO = Instantiate(_enemyPrefab, _enemyBattleStation);
        _enemyUnit = enemyGO.GetComponent<Unit>();

        _playerHUD.SetHUD(_playerUnit);
        _enemyHUD.SetHUD(_enemyUnit);

        yield return new WaitForSeconds(2f);
    
        state = BattleState.PlayerTurn;
        PlayerTurn();
    }

    private void PlayerTurn()
    {
        _dialogueText.text = "Choose an action";
    }

    public void OnAttackButton()
    {
        if (state != BattleState.PlayerTurn)
            return;

        StartCoroutine(PlayerAttack());
    }

    public void OnHealButton()
    {
        if (state != BattleState.PlayerTurn)
            return;

        _playerUnit.Heal(2);
        _playerHUD.SetHP(_playerUnit.currentHP);

        StartCoroutine(EnemyTurn());
    }

    public void OnKillButton()
    {
        if (state != BattleState.PlayerTurn)
            return;

        bool isDead = _enemyUnit.TakeDamege(_enemyUnit.maxHP);

        _dialogueText.text = "Attack is successful";
        _enemyHUD.SetHP(_enemyUnit.currentHP);

        if (isDead)
        {
            state = BattleState.Won;
            EndBattle();
        }
        else
        {
            state = BattleState.EnemyTurn;
            StartCoroutine(EnemyTurn());
        }
    }

    IEnumerator PlayerAttack()
    {
        bool isDead = _enemyUnit.TakeDamege(_playerUnit.damage);

        _dialogueText.text = "Attack is successful";
        _enemyHUD.SetHP(_enemyUnit.currentHP);

        yield return new WaitForSeconds(1f);

        if (isDead)
        {
            state = BattleState.Won;
            EndBattle();
        }
        else
        {
            state = BattleState.EnemyTurn;
            StartCoroutine(EnemyTurn());
        }
            
        

        yield return new WaitForSeconds(2f);
    }

    IEnumerator EnemyTurn()
    {
        _dialogueText.text = _enemyUnit.unitName + " attack";

        yield return new WaitForSeconds(1f);

        bool isDead = _playerUnit.TakeDamege(_enemyUnit.damage);

        _playerHUD.SetHP(_playerUnit.currentHP);

        yield return new WaitForSeconds(1f);

        if (isDead)
        {
            state = BattleState.Lost;
            EndBattle();
        }
        else
        {
            state = BattleState.PlayerTurn;
            PlayerTurn();
        }
    }

    private void EndBattle()
    {
        

        if(state == BattleState.Won)
        {
            _dialogueText.text = "Congritulation!!! You WON";
        }
        else
        {
            _dialogueText.text = "You are defeted";
        }
        
        SceneManager.LoadSceneAsync("SampleScene");
        SceneManager.UnloadSceneAsync("BattleTestScene");



    }
}
