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
       

    #region Capsullation
    public Transform Target { get => _target; set => _target = value; }
    public List<GameObject> ActiveCharacters { get => activeCharacters; set => activeCharacters = value; }
    #endregion


    #region AI TARGET
    public void CurrentTarget()
    {
        Target = targets[_currentTarget];
        Target.GetComponent<TowerController>().enabled = true;
    }

    public void ChangeTarget()
    {
        _currentTarget++;
        CurrentTarget();
    }
    #endregion
    
    #region Defeat Tower
        public void ActiveAllCharactersDestroy(){
            for (int i = 0; i < ActiveCharacters.Count; i++)
            {
                activeCharacters[i].SetActive(false);
            }
        }
    #endregion
}

