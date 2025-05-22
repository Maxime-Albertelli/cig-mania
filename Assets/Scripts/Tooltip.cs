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
    [Tooltip("The country's cost")]
    [SerializeField] private TMP_Text price;

    [Tooltip("The country's number of smoker")]
    [SerializeField] private TMP_Text addictedPopulation;
    [SerializeField] private Slider addictedPopulationSlider;
    [Tooltip("The country's number of dead people")]
    [SerializeField] private TMP_Text deadPopulation;
    [SerializeField] private Slider deadPopulationSlider;
    [Tooltip("The country's number of healthy people")]
    [SerializeField] private TMP_Text healthyPopulation;
    [SerializeField] private Slider healthyPopulationSlider;

    [Tooltip("The country's number of healthy people")]
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

    /// <summary>
    /// Activate the gameobject
    /// </summary>
    public void Show()
    {
        gameObject.SetActive(true);
    }
    
    /// <summary>
    /// Hide the gameobject
    /// </summary>
    public void Hide()
    {
        gameObject.SetActive(false);
    }

    /// <summary>
    /// Update region's population in the UI
    /// </summary>
    /// <param name="region">region to set</param>
    public void UpdateRegion(Region region)
    {
        this.region = region;

        // When the region isn't bought
        if (!this.region.isBuyingCigarettes)
        {
            ShowRegionInfo(false);
            return;
        }

        // When the region is bought
        ShowRegionInfo(true);

        UpdateAddictedPopulation();
        UpdateDeadPopulation();
        UpdateHealthyPopulation();
    }

    /// <summary>
    /// Shows the infos of the region 
    /// </summary>
    /// <param name="bought">True if the region has been bought</param>
    private void ShowRegionInfo(bool bought)
    {
        price.gameObject.SetActive(!bought);
        unlockButton.gameObject.SetActive(!bought);

        addictedPopulation.gameObject.SetActive(bought);
        addictedPopulationSlider.gameObject.SetActive(bought);

        deadPopulation.gameObject.SetActive(bought);
        deadPopulationSlider.gameObject.SetActive(bought);

        healthyPopulation.gameObject.SetActive(bought);
        healthyPopulationSlider.gameObject.SetActive(bought);

        if (bought)
        {
            regionName.gameObject.SetActive(true);
            regionName.text = region.GetRegionName();
        }
        else
        {
            regionName.gameObject.SetActive(false);
            price.text = $"{region.GetRegionName()} : Débloquer pour {GameManager.ParseNumber(region.GetMaxPopulation() / 1000)} €";
        }
    }

    /// <summary>
    /// Update the dead population value in the UI
    /// </summary>
    private void UpdateDeadPopulation()
    {
        deadPopulation.text =
            $"Morts: {GameManager.ParseNumber(region.deadPopulation)}/{GameManager.ParseNumber(region.GetMaxPopulation())}";
        deadPopulationSlider.value = region.deadPopulation;
        deadPopulationSlider.maxValue = region.GetMaxPopulation();
    }

    /// <summary>
    /// Update the healthy population value in the UI
    /// </summary>
    private void UpdateHealthyPopulation()
    {
        healthyPopulation.text =
            $"Sains: {GameManager.ParseNumber(region.healthyPopulation)}/{GameManager.ParseNumber(region.GetMaxPopulation())}";
        healthyPopulationSlider.value = region.healthyPopulation;
        healthyPopulationSlider.maxValue = region.GetMaxPopulation();
    }


    /// <summary>
    /// Update the addict population value in the UI
    /// </summary>
    private void UpdateAddictedPopulation()
    {
        addictedPopulation.text =
            $"Addictes: {GameManager.ParseNumber(region.addictedPopulation)}/{GameManager.ParseNumber(region.GetMaxPopulation())}";
        addictedPopulationSlider.value = region.addictedPopulation;
        addictedPopulationSlider.maxValue = region.GetMaxPopulation();
    }

    /// <summary>
    /// Check if the user can unlock the region,
    /// if yes, buy the region and start with a single addict.
    /// need refactor
    /// </summary>    
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
        ParticleSystem[] particles = region.GetComponentsInChildren<ParticleSystem>();
        foreach (ParticleSystem particle in particles)
        {
            particle.GetComponentInChildren<ParticleSystem>().Play();
        }
        region.addictedPopulation = 1;
        region.healthyPopulation = region.healthyPopulation - region.addictedPopulation;
        UpdateRegion(region);
    }
    
}