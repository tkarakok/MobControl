using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public List<Transform> targets;
    public Transform cannon, forcePoint;
    public float forceSpeedForPlayer;
}

