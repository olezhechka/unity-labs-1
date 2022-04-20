using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "New Level Up Balance Config", menuName = "Level Up Balance Config")]
public class LevelUpBalanceConfig : ScriptableObject
{
    private static List<LevelUpBalanceConfig> _levelUpConfigsPool = new List<LevelUpBalanceConfig>();

    public int LevelId;
    public int Experience;
    
    public int ExpCumulative
    {
        get => LevelUpBalanceConfig._levelUpConfigsPool
                .Where(levelUpConfig => levelUpConfig.LevelId < this.LevelId)
                .Sum(levelUpConfig => levelUpConfig.Experience);
        private set { }
    }

    public LevelUpBalanceConfig()
    {
        LevelUpBalanceConfig._levelUpConfigsPool.Add(this);
    }
}