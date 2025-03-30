using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Information about a region on click
/// </summary>
public class Tooltip : MonoBehaviour
{
    [SerializeField] private Button unlockButton;
    [SerializeField] private TMP_Text price;

    public TMP_Text addictedPopulation;
    public TMP_Text regionName;
    public static Tooltip instance;

    private Region region;

    private void Awake()
    {
        instance = this;
        Hide();
    }

    private void Update()
    {
        if (region == null)
            return;

        if (region.addictedPopulation == 0)
            return;
        
        UpdateAddictedPopulation();
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void UpdateRegion(Region newRegion)
    {
        if (newRegion.population == 0)
            return;
        
        region = newRegion;

        // When the region isn't buy
        if (region.addictedPopulation == 0)
        {
            price.gameObject.SetActive(true);
            unlockButton.gameObject.SetActive(true);
            addictedPopulation.gameObject.SetActive(false);
            regionName.gameObject.SetActive(false);
            price.text = $"{region.regionName} : Débloquer pour {GameManager.ParseNumber(region.population / 1000)} €";
            return;
        }

        // When the region is buy
        price.gameObject.SetActive(false);
        unlockButton.gameObject.SetActive(false);
        addictedPopulation.gameObject.SetActive(true);
        regionName.gameObject.SetActive(true);
        regionName.text = region.regionName;
        UpdateAddictedPopulation();
    }

    private void UpdateAddictedPopulation()
    {
        addictedPopulation.text =
            $"Population Addicte: {GameManager.ParseNumber(region.addictedPopulation)}/{GameManager.ParseNumber(region.population)}";
    }
    
    public void UnlockRegion()
    {
        var cost = region.population / 1000;

        // The user can't unlock a region if he don't have enough money
        if (GameManager.Instance.money < cost)
        {
            price.text = "Pas assez d'argent !";
            return;
        }

        GameManager.Instance.money -= cost;
        
        region.addictedPopulation = 1;
        UpdateRegion(region);
    }
    
}