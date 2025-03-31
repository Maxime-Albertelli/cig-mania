using UnityEngine;

/// <summary>
/// statType : represents the type of stat, for instance : prix, toxicite, addiction, influence.
/// skillIncreaseAmount : represent the increase value of a skill (in percentage or in flat point).
/// isPercentage : allows to chose between a percentage or a point value for the skillIncreaseAmount.
/// </summary>
[System.Serializable]
public class UpgradeData
{
    public StatType statType;
    public float skillIncreaseAmount;
    public bool isPercentage;

}