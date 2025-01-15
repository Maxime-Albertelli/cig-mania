using System;
using TMPro;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    [SerializeField] private TMP_Text upgradeName;
    [SerializeField] private TMP_Text description;
    [SerializeField] private TMP_Text price;
    [SerializeField] private TMP_Text preview;

    private Upgrade _selectedUpgrade;


    private void Start()
    {
        SelectUpgrade(FindAnyObjectByType<Upgrade>());
    }

    public void BuyUpgrade()
    {
        if (GameManager.Instance.money < _selectedUpgrade.upgradeCost[_selectedUpgrade.actualLevel])
            return;

        GameManager.Instance.money -= _selectedUpgrade.upgradeCost[_selectedUpgrade.actualLevel];
        _selectedUpgrade.actualLevel++;
        ApplyUpgrade(_selectedUpgrade);
        SelectUpgrade(_selectedUpgrade);
    }

    private void ApplyUpgrade(Upgrade upgrade)
    {
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
    }


    public void SelectUpgrade(Upgrade upgrade)
    {
        _selectedUpgrade = upgrade;
        upgradeName.text = upgrade.upgradeName;
        description.text = upgrade.description;
        price.text = $"Upgrade\ncost : {GameManager.ParseNumber(upgrade.upgradeCost[upgrade.actualLevel])}$";

        var cigarette = GameManager.Instance.cigarette;
        var text = "";

        if (upgrade.prices.Length > upgrade.actualLevel)
            text +=
                $"Price: {cigarette.price} -> {cigarette.price + upgrade.prices[upgrade.actualLevel]}\n";

        if (upgrade.toxicities.Length > upgrade.actualLevel)
            text +=
                $"Toxicity: {cigarette.toxicity} -> {cigarette.toxicity * upgrade.toxicities[upgrade.actualLevel]}\n";

        if (upgrade.addictions.Length > upgrade.actualLevel)
            text +=
                $"Addiction: {cigarette.addiction} -> {cigarette.addiction + (1 - cigarette.addiction) / upgrade.addictions[upgrade.actualLevel]}\n";

        if (upgrade.influences.Length > upgrade.actualLevel)
            text +=
                $"Influence: {cigarette.influence} -> {cigarette.influence * upgrade.influences[upgrade.actualLevel]}\n";

        preview.text = text;
    }
}