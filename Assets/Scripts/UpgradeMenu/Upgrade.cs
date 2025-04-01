using UnityEngine;

/// <summary>
/// Script that hold a scriptable upgrade and link to a button
/// </summary>
public class Upgrade : MonoBehaviour
{
    [Header("Upgrade")]
    [SerializeField] private UpgradeSO upgradeInfo;

    /// <summary>
    ///  Used in a button, when clicked, call the select Upgrade method in upgrade manager
    /// </summary>
    public void SelectedUpgrade()
    {
        Debug.Log("Clicked");
        if (!upgradeInfo.purchased)
        {
            UpgradeManager.instance.SelectUpgrade(upgradeInfo);
        }
        else
        {
            Debug.Log("Already bought");
        }
    }
}
