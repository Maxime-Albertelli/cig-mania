using UnityEngine;

/// <summary>
/// Informations of an upgrade
/// </summary>
public class Upgrade : MonoBehaviour
{
    [Header("Upgrade")]
    [SerializeField] private UpgradeSO upgradeInfo;

    public void SelectedUpgrade()
    {
        Debug.Log("Clicked");
        UpgradeManager.instance.SelectUpgrade(upgradeInfo);
    }
}
