using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.U2D;

/// <summary>
/// Informations about a region
/// </summary>
public class Region : MonoBehaviour
{
    private const float MAX_ALPHA = 0.80f;

    [Header("Region attibuts")]
    [Tooltip("The region's name")]
    [SerializeField] private string regionName;
    [Tooltip("The sprite shape of the region ")]
    [SerializeField] private SpriteShapeRenderer spriteShape;
    [Tooltip("Gamemanger's instance")]
    [SerializeField] private GameManager manager;


    [Header("Population in this region")]
    [Tooltip("Maximum population in this region")]
    [SerializeField] private long maxPopulation; // used only for the reset
    [Tooltip("Current healthy population in this region")] 
    public long healthyPopulation = 0;
    [Tooltip("Current addicted people in this region")]
    public long addictedPopulation = 0;
    [Tooltip("Current dead people in this region")]
    public long deadPopulation = 0;

    [Header("Boolean value of this region")]
    [Tooltip("Checked means this region can buy cigarettes")]
    public bool isBuyingCigarettes;
    [Tooltip("Checked means this region is selected")]
    public bool isSelected;

    private void Awake()
    {
        manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    private void Start()
    {
        healthyPopulation = maxPopulation - addictedPopulation;
    }

    public void UpdateVisuals()
    {
        float percentage = (float)addictedPopulation / maxPopulation;
        Color addictColor = Color.red;
        Color selectColor = Color.white; // Color on click
        selectColor.a = 0.25f;
        addictColor.a = percentage * MAX_ALPHA;
        if (isSelected)
        {
            spriteShape.color = selectColor;
        }
        else
        {
            spriteShape.color = addictColor;
        }
    }
    

    public void OnMouseUpAsButton() 
    {
        Tooltip.instance.UpdateRegion(this);
        Tooltip.instance.Show();
        // Disable blink on other regions
        for (int i = 0; i < manager.regions.Length; ++i)
        {
            manager.regions[i].isSelected = false;
        }
        isSelected = true;
    }

    #region Population methods
    /// <summary>
    /// Keeps track of the regional population
    /// </summary>
    /// <param name="deaths">People who die today</param>
    /// <param name="lostPeople">People who stop smoking</param>
    /// <param name="newUsers">People who start smoking</param>
    public void ApplyEvolution(long deaths, long lostPeople, long newUsers)
    {
        // Evolution of population
        this.healthyPopulation -= newUsers;
        this.addictedPopulation += newUsers - deaths - lostPeople;
        this.deadPopulation += deaths;

        // Clamp pour éviter les valeurs négatives
        this.healthyPopulation = (long)Mathf.Max(this.healthyPopulation, 0);
        this.addictedPopulation = (long)Mathf.Max(this.addictedPopulation, 0);
        this.deadPopulation = (long)Mathf.Max(this.deadPopulation, 0);
    }

    /// <summary>
    /// Reset all the population value
    /// </summary>
    public void ResetPopulation()
    {
        healthyPopulation = maxPopulation;
        addictedPopulation = 0;
        deadPopulation = 0;
    }
    #endregion

    #region Getter / Setter 
    public string GetRegionName()
    {
        return this.regionName;
    }
    public long GetMaxPopulation()
    {
        return this.maxPopulation;
    }
    #endregion
}