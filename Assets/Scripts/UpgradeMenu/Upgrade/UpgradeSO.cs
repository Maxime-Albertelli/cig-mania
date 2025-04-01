using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

/// <summary>
/// Scriptable Object that represent an upgrade. It generates automaticaly a description depending on the effect and on the percent or not.
/// </summary>
[CreateAssetMenu(fileName = "New Upgrade", menuName = "Upgrade System/New Upgrade", order = 0)]
public class UpgradeSO : ScriptableObject
{
    [Tooltip("List of all attributs affected")]
    public List<UpgradeData> upgradeData = new List<UpgradeData>();
    [Tooltip("Check if percentage, uncheck for flat values")]
    public bool isAbility;
    [Tooltip("Upgrade's name")]
    public string upgradeName;
    [Tooltip("Check if you want your own description, uncheck for the automatic description")]
    public bool overrideDescription;
    [Tooltip("Upgrade's descriptions")]
    [TextArea(1,4)] public string description;
    [Tooltip("List of all the necessary upgrades")]
    public List<UpgradeSO> upgradePrerequisites = new List<UpgradeSO>();
    [Tooltip("Upgrade's tier")]
    public int tier;
    [Tooltip("Upgrade's cost, keep in mind that it has to coincide with your money")]
    public float cost;
    [Tooltip("Check if upgrade already brought")]
    public bool purchased;

    private void OnValidate()
    {
        upgradeName = name;
        if (upgradeData.Count == 0) return;
        if (overrideDescription) return;

        GenerateDescription();
    }

    private void GenerateDescription()
    {
        if (isAbility)
        {
            switch (upgradeData[0].statType)
            {
                default:
                    Debug.Log("Not Implemented Yet");
                    break;
            }
        } 
        else
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"{upgradeName} augmente ");
            for (int i = 0; i < upgradeData.Count; i++) 
            {
                sb.Append(upgradeData[i].statType.ToString());
                sb.Append(" de ");
                sb.Append(upgradeData[i].skillIncreaseAmount.ToString());
                sb.Append(upgradeData[i].isPercentage ? "%" : " point(s)");
                sb.Append(i < upgradeData.Count - 1 ? ", " : ".");
            }

            description = sb.ToString();    
        }
    }
}


