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

    [SerializeField] private TMP_Text addictedPopulation;
    [SerializeField] private TMP_Text deadPopulation;
    [SerializeField] private TMP_Text healthyPopulation;

    [SerializeField] private TMP_Text regionName;
    public static Tooltip instance;

    private Region region;

    private void Awake()
    {
        instance = this;
        Hide();
    }

    private void Update()
    {
        if (region == null) return;

        UpdateAddictedPopulation();

        UpdateDeadPopulation();

        UpdateHealthyPopulation();
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
        if (newRegion.healthyPopulation == 0)
            return;
        
        region = newRegion;

        // When the region isn't bought
        if (region.addictedPopulation == 0)
        {
            price.gameObject.SetActive(true);
            unlockButton.gameObject.SetActive(true);
            addictedPopulation.gameObject.SetActive(false);
            deadPopulation.gameObject.SetActive(false);
            healthyPopulation.gameObject.SetActive(false);
            regionName.gameObject.SetActive(false);
            price.text = $"{region.GetRegionName()} : Débloquer pour {GameManager.ParseNumber(region.GetMaxPopulation() / 1000)} €";
            return;
        }

        // When the region is bought
        price.gameObject.SetActive(false);
        unlockButton.gameObject.SetActive(false);
        addictedPopulation.gameObject.SetActive(true);
        deadPopulation.gameObject.SetActive(true);
        healthyPopulation.gameObject.SetActive(true);
        regionName.gameObject.SetActive(true);
        regionName.text = region.GetRegionName();

        UpdateAddictedPopulation();
        UpdateDeadPopulation();
        UpdateHealthyPopulation();
    }

    private void UpdateDeadPopulation()
    {
        deadPopulation.text =
            $"Population morte: {GameManager.ParseNumber(region.deadPopulation)}/{GameManager.ParseNumber(region.GetMaxPopulation())}";

    }

    private void UpdateHealthyPopulation()
    {
        healthyPopulation.text =
            $"Population saine: {GameManager.ParseNumber(region.healthyPopulation)}/{GameManager.ParseNumber(region.GetMaxPopulation())}";

    }

    private void UpdateAddictedPopulation()
    {
        addictedPopulation.text =
            $"Population Addicte: {GameManager.ParseNumber(region.addictedPopulation)}/{GameManager.ParseNumber(region.GetMaxPopulation())}";
    }
    
    public void UnlockRegion()
    {
        ulong cost = (ulong)region.GetMaxPopulation() / 1000;

        // The user can't unlock a region if he don't have enough money
        if (GameManager.Instance.moneyValue < cost)
        {
            region.isBuyingCigarettes = false;
            price.text = "Pas assez d'argent !";
            return;
        }

        region.isBuyingCigarettes = true;
        GameManager.Instance.moneyValue -= cost;
        
        region.addictedPopulation = 1;
        UpdateRegion(region);
    }
    
}