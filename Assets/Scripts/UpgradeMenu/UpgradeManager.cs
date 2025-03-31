using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

/// <summary>
/// Manage the panels and apply all the effect of an upgrade
/// </summary>
public class UpgradeManager : MonoBehaviour
{
    [Header("Panel Menu Upgrade")]
    [Tooltip("The game object used for the upgrade menu")]
    [SerializeField] private GameObject upgradePanel;
    [Space(10)]

    [Header("Panel upgrades Menu")]
    [Tooltip("The game object used for the price upgrade menu")]
    [SerializeField] private GameObject prixUpgradePanel;
    [Tooltip("The game object used for the taxe upgrade menu")]
    [SerializeField] private GameObject taxeUpgradePanel;
    [Tooltip("The game object used for the influence upgrade menu")]
    [SerializeField] private GameObject influenceUpgradePanel;
    [Tooltip("The game object used for the addiction upgrade menu")]
    [SerializeField] private GameObject addictionUpgradePanel;
    [Tooltip("The game object used for the trust upgrade menu")]
    [SerializeField] private GameObject trustUpgradePanel;
    [Space(5)]

    [Header("Text panel menu")]
    [Tooltip("The title text of the upgrade menu")]
    [SerializeField] private TMP_Text menuTitle;
    [Tooltip("The description text of the upgrade menu")]
    [SerializeField] private TMP_Text menuDescription;
    [Space(10)]

    [Header("Text panel price")]
    [Tooltip("The title text of the price upgrade menu")]
    [SerializeField] private TMP_Text priceTitle;
    [Tooltip("The title text of the price upgrade menu")]
    [SerializeField] private TMP_Text priceDescription;
    [Space(10)]

    [Header("Text panel taxes")]
    [Tooltip("The title text of the taxe upgrade menu")]
    [SerializeField] private TMP_Text taxeTitle;
    [Tooltip("The description text of the taxe upgrade menu")]
    [SerializeField] private TMP_Text taxeDescription;
    [Space(10)]

    [Header("Text panel influence")]
    [Tooltip("The title text of the influence upgrade menu")]
    [SerializeField] private TMP_Text influenceTitle;
    [Tooltip("The description text of the influence upgrade menu")]
    [SerializeField] private TMP_Text influenceDescription;
    [Space(10)]

    [Header("Text panel addiction")]
    [Tooltip("The title text of the addiction upgrade menu")]
    [SerializeField] private TMP_Text addictionTitle;
    [Tooltip("The description text of the addiction upgrade menu")]
    [SerializeField] private TMP_Text addictionDescription;
    [Space(10)]

    [Header("Text panel trust")]
    [Tooltip("The title text of the trust upgrade menu")]
    [SerializeField] private TMP_Text trustTitle;
    [Tooltip("The description text of the trust upgrade menu")]
    [SerializeField] private TMP_Text trustDescription;
    [Space(10)]

    // List of all unlocked upgrade
    private List<UpgradeSO> unlockedUpgrade = new List<UpgradeSO>();

    public static UpgradeManager instance = null;

    // Make this script a singleton
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
 
    private void Start()
    {
        // Initiate the description and title of each panel
        menuTitle.text = "Gestion des produits";
        menuDescription.text = "Bonjour patron ! Bienvenu dans le département de R&D. Ici vous pouvez investir dans différents points de recherches.";

        priceTitle.text = "Prix\r\n_________________";
        priceDescription.text = "Bienvenu dans la gestion des prix." +
            "\nIci vous pouvez gérés le prix de vos produits !" +
            "\nL'argent directement dans vos poches !";

        taxeTitle.text = "Taxe\r\n_________________";
        taxeDescription.text = "Bienvenu dans la gestion des Taxes." +
            "\nIci vous pouvez gérés les taxes sur vos produits !" +
            "\nNégociez avec l'état pour une reduction de taxes";

        influenceTitle.text = "Influence\r\n_________________";
        influenceDescription.text = "Bienvenu dans la gestion de l'influence." +
            "\nIci vous pouvez gérés l'influence de vos produits !" +
            "\nPub, journaux, tik tok, influenceur, lobby, n'importe quoi pour vendre plus !";

        addictionTitle.text = "Addiction\r\n_________________";
        addictionDescription.text = "Bienvenu dans la gestion de l'addiction." +
            "\nIci vous pouvez gérés le gout de vos produits !" +
            "\nSi on augmente la dose, le gout est meilleur, aucun risque sur la santé, n'est ce pas ?";

        trustTitle.text = "Confiance\r\n_________________";
        trustDescription.text = "Bienvenu dans la gestion de la confiance." +
            "\nIci vous pouvez gérés l'image de la compagnie." +
            "\nSi nos ventes sont trop aggresives, les états le remarquerait ! Avec quelques pots de vins" +
            "\nIls se laisseront faire !";
    }

    /// <summary>
    /// When a upgrade is selected, check all prerequisites if yes, unlock Upgrade, in all cases Change Description
    /// </summary>
    /// <param name="upgrade"></param>
    public void SelectUpgrade(UpgradeSO upgrade)
    {
        if (PreReqsMet(upgrade))
        {
            UnlockUpgrade(upgrade);
        }

        ChangeDescription(upgrade);
    }

    /// <summary>
    /// Change the description and the title on the active panel depending of the upgrade
    /// </summary>
    /// <param name="upgrade"></param>
    private void ChangeDescription(UpgradeSO upgrade)
    {
        if (prixUpgradePanel.activeSelf)
        {
            priceTitle.text = upgrade.name + "\r\n_________________";
            priceDescription.text = upgrade.description;
        }

        if (taxeUpgradePanel.activeSelf)
        {
            taxeTitle.text = upgrade.name + "\r\n_________________";
            taxeDescription.text = upgrade.description;
        }

        if (influenceUpgradePanel.activeSelf)
        {
            influenceTitle.text = upgrade.name + "\r\n_________________";
            influenceDescription.text = upgrade.description;
        }

        if (addictionUpgradePanel.activeSelf)
        {
            addictionTitle.text = upgrade.name + "\r\n_________________";
            addictionDescription.text = upgrade.description;
        }
    }

    /// <summary>
    /// Apply effect depending of the upgrade' Stat Type
    /// </summary>
    /// <param name="upgrade"></param>
    /// <exception cref="ArgumentOutOfRangeException">Throw an exception if a new state type has been added but not in the switch case</exception>
    private void ApplyEffect(UpgradeSO upgrade)
    {
        /*
        // Upgrade prices
        if (upgrade.prices.Length > upgrade.actualLevel)
            GameManager.Instance.cigarette.price += upgrade.prices[upgrade.actualLevel];

        // Upgrade toxicities
        if (upgrade.toxicities.Length > upgrade.actualLevel)
            GameManager.Instance.cigarette.toxicity *= upgrade.toxicities[upgrade.actualLevel];

        // Upgrade addictions
        var addiction = GameManager.Instance.cigarette.addiction;
        if (upgrade.addictions.Length > upgrade.actualLevel)
            GameManager.Instance.cigarette.addiction =
                addiction + (1 - addiction) / upgrade.addictions[upgrade.actualLevel];

        // Upgrade influences
        if (upgrade.influences.Length > upgrade.actualLevel)
            GameManager.Instance.cigarette.influence *= upgrade.influences[upgrade.actualLevel];
        */

        foreach (UpgradeData data in upgrade.upgradeData)
        {
            switch (data.statType)
            {
                case StatType.Prix:
                    ModifyStat(ref GameManager.Instance.cigarette.price, data);
                    break;

                case StatType.Influence:
                    ModifyStat(ref GameManager.Instance.cigarette.influence, data);
                    break;

                case StatType.Addiction:
                    ModifyStat(ref GameManager.Instance.cigarette.addiction, data);
                    break;

                case StatType.Toxicite:
                    ModifyStat(ref GameManager.Instance.cigarette.toxicity, data);
                    break;

                case StatType.Confiance:
                    ModifyStat(ref GameManager.Instance.trustRate, data);
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(data.statType));
            }
        }
    }

    /// <summary>
    /// Modify ciragette's stat, wether it's a percentage or flat 
    /// </summary>
    /// <param name="stat">The stat's reference, for instance, addiction</param>
    /// <param name="data">The upgrade's data</param>
    private void ModifyStat(ref float stat, UpgradeData data)
    {
        bool isPercent = data.isPercentage;

        Debug.Log("before : " + stat);

        if (isPercent)
        {
            stat += stat * (data.skillIncreaseAmount / 100f);
        }
        else
        {
            stat += data.skillIncreaseAmount;
        }

        Debug.Log("After : " + stat);

    }

    /// <summary>
    /// Verify if the upgrade is already obtained
    /// </summary>
    /// <param name="upgrade">The Upgrade selected</param>
    /// <returns>True if the upgrade is in unlockedUpgrade list</returns>
    public bool IsUpgradeUnlocked(UpgradeSO upgrade)
    {
        return unlockedUpgrade.Contains(upgrade);
    }

    /// <summary>
    /// Check if an upgrade is purchasable.
    /// Check if already purchased, check if there's no upgrade prerequisites or if all upgradePrerequisites are unlocked
    /// Do not check if enough money to buy it.
    /// </summary>
    /// <param name="upgrade"></param>
    /// <returns>True if all prerequisites are met</returns>
    public bool PreReqsMet(UpgradeSO upgrade)
    {
        bool preReqsMet = false;
        Debug.Log("Already have this one");
        if (!upgrade.purchased)
        {
            preReqsMet = upgrade.upgradePrerequisites.Count == 0 || upgrade.upgradePrerequisites.All(unlockedUpgrade.Contains);
        }
        return preReqsMet;
    }

    /// <summary>
    /// Verify if the upgrade is affordable
    /// </summary>
    /// <param name="upgrade"></param>
    /// <returns>True if enough to buy the upgrade</returns>
    public bool CanAffordUpgrade(UpgradeSO upgrade)
    {
        return GameManager.Instance.moneyValue >= upgrade.cost;
    }

    /// <summary>
    /// First, check if enough money to buy, if yes, apply its effect then add it to the unlockedUpgrade list
    /// </summary>
    /// <param name="upgrade"></param>
    public void UnlockUpgrade(UpgradeSO upgrade)
    {
        if (!CanAffordUpgrade(upgrade))
        {
            Debug.Log("Not enought money");
            SoundManager.PlaySound(SoundType.UPGRADE_DENIED);
            return;
        }
        ApplyEffect(upgrade);
        unlockedUpgrade.Add(upgrade);
        GameManager.Instance.moneyValue -= (ulong)upgrade.cost;
        Debug.Log("Skill obtained : " + upgrade.name);
        SoundManager.PlaySound(SoundType.UPGRADE);
    }
}