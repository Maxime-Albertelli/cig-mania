using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour
{
    public static Tooltip instance;
    private Region region;
    [SerializeField] private Button unlockButton;
    [SerializeField] private TMP_Text price;
    public TMP_Text addictedPopulation;
    public TMP_Text regionName;


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

        if (region.addictedPopulation == 0)
        {
            price.gameObject.SetActive(true);
            unlockButton.gameObject.SetActive(true);
            addictedPopulation.gameObject.SetActive(false);
            regionName.gameObject.SetActive(false);
            price.text = $"{region.regionName} : Unlock for {GameManager.ParseNumber(region.population / 1000)} €";
            return;
        }

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
            $"Addicted Population: {GameManager.ParseNumber(region.addictedPopulation)}/{GameManager.ParseNumber(region.population)}";
    }
    
    public void UnlockRegion()
    {
        var cost = region.population / 1000;
        
        if (GameManager.Instance.money < cost)
        {
            price.text = "Not enough money !";
            return;
        }

        GameManager.Instance.money -= cost;
        
        region.addictedPopulation = 1;
        UpdateRegion(region);
    }
    
}