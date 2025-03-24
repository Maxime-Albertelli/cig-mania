using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "New Upgrade Library", menuName = "Upgrade System/Upgrade Tree Library", order = 0)]
public class UpgradeTreeLibrarySO : ScriptableObject
{
    public List<UpgradeSO> upgradeLibrary;

    public List<UpgradeSO> GetUpgradeOfTier(int tier)
    {
        return upgradeLibrary.Where(upgrade => upgrade.tier == tier).ToList();
    }

}
