using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterSettings", menuName = "MobControl/CharacterSettings", order = 0)]
public class CharacterSettings : ScriptableObject
{
    public int totalTrigger;
    public ChracterType chracterType;
    public bool big;
}


