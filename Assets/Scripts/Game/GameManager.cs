using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public List<Transform> targets;
    public Transform cannon, forcePoint;
    public float forceSpeedForPlayer;

    Transform _target;
    int _currentTarget = 0;

    public Transform Target { get => _target; set => _target = value; }

    private void Start() {
        CurrentTarget();
    }

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
}

