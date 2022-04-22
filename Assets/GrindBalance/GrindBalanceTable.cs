using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Grid Balance Table", fileName = "New Grind Balance Table")]
public class GrindBalanceTable : ScriptableObject
{
    public GrindBalanceModel[] grindBalanceData;
}