using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameManager : Singleton<GameManager>
{
    public List<Transform> targets;
    public Transform cannon, forcePoint;
    public float forceSpeedForPlayer;


    List<GameObject> activeCharacters = new List<GameObject>();
    Transform _target;
    int _currentTarget = -1;
    int _currentCoin;
    int _currentRock;

    
    #region Capsullation
    public Transform Target { get => _target; set => _target = value; }
    public List<GameObject> ActiveCharacters { get => activeCharacters; set => activeCharacters = value; }
    public int CurrentTarget { get => _currentTarget; set => _currentTarget = value; }
    public int CurrentCoin { get => _currentCoin; set => _currentCoin = value; }
    public int CurrentRock { get => _currentRock; set => _currentRock = value; }
    #endregion

    #region Check Game Over
    public bool CheckGameOver()
    {
        if (CurrentTarget >= targets.Count - 1)
        {
            
            return true;
        }
        else
        {
            return false;
        }
    }
    #endregion

    #region AI TARGET
    public void FindCurrentTarget()
    {
        Target = targets[CurrentTarget];
        Target.GetComponent<TowerController>().enabled = true;
    }

    public void ChangeTarget()
    {
        CurrentTarget++;
        if (CurrentTarget <= targets.Count)
        {
            FindCurrentTarget();
        }

    }
    #endregion

    #region Defeat Tower
    public void ActiveAllCharactersDestroy()
    {
        for (int i = 0; i < ActiveCharacters.Count; i++)
        {
            activeCharacters[i].SetActive(false);
        }
    }
    #endregion

    #region Coin And Rock
    public void PlusCoin(){
        CurrentCoin++;
        UIManager.Instance.InGameCoinUpdate();
    }
    public void PlusRock(){
        CurrentRock++;
        UIManager.Instance.InGameRockUpdate();
    }
    public void SaveRockAndCoin(){
        PlayerPrefs.SetInt("Coin", (PlayerPrefs.GetInt("Coin") + CurrentCoin));
        PlayerPrefs.SetInt("Rock", (PlayerPrefs.GetInt("Rock") + CurrentRock));
    }
    #endregion
}

