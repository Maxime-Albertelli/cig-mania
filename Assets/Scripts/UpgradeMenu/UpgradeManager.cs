using System;
using TMPro;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{



    [SerializeField] private GameObject upgradePanel;

    [SerializeField] private GameObject prixUpgradePanel;
    [SerializeField] private GameObject taxeUpgradePanel;
    [SerializeField] private GameObject influenceUpgradePanel;
    [SerializeField] private GameObject addictionUpgradePanel;

    private Upgrade _selectedUpgrade;


    private void Start()
    {

    }

    public void BuyUpgrade()
    {

    }

    public void ShowPrixUpgradePanel()
    {
        prixUpgradePanel.SetActive(true);
        Tooltip.instance.Hide();
    }

    public void ShowTaxeUpgradePanel()
    {
        taxeUpgradePanel.SetActive(true);
        Tooltip.instance.Hide();
    }

    public void ShowAddictionUpgradePanel()
    {
        addictionUpgradePanel.SetActive(true);
        Tooltip.instance.Hide();
    }

    public void ShowInfluenceUpgradePanel()
    {
        influenceUpgradePanel.SetActive(true);
        Tooltip.instance.Hide();
    }

    private void ApplyUpgrade(Upgrade upgrade)
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
    }


    public void SelectUpgrade(Upgrade upgrade)
    {

    }
}