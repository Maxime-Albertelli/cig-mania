using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Game Manager
/// </summary>
public class GameManager : MonoBehaviour
{
    [Header("Cigarette Stats")]
    [Tooltip("All the attributs of a base cigarette")]
    public Cigarette cigarette;
    [Space(10)]

    [Header("Regions")]
    [Tooltip("List of all regions on the map")]
    [SerializeField] public Region[] regions;
    [Space(10)]

    [Header("Slider")]
    [Tooltip("The slider used for the trust bar")]
    [SerializeField] private Slider trustBar;
    [Space(10)]

    [Header("UI Infos Texts")]
    [Tooltip("Number of death text")]
    [SerializeField] private TMP_Text deathsText; 
    [Tooltip("Number of addicted text")]
    [SerializeField] private TMP_Text addictedText;
    [Tooltip("Money text")]
    [SerializeField] private TMP_Text moneyText;
    [Tooltip("Company's name Text")]
    [SerializeField] private TMP_Text nameText;
    [Tooltip("Trust text")]
    [SerializeField] private TMP_Text TrustText;
    [Space(10)]

    [Header("Choose a Name box")]
    [SerializeField] private GameObject chooseName;
    [SerializeField] private TMP_Text nameField;
    [Space(10)]

    [SerializeField] private Tooltip regionTooltip;
    [Space(10)]

    [SerializeField] private GameObject inGameUI;
    [SerializeField] private GameObject particleClick;
    [Space(10)]

    [Header("Panels")]
    [Tooltip("The game end screen panel")]
    [SerializeField] private GameObject gameEndPanel;

    [Header("Game Values")]
    [Tooltip("The trust rate per week")]
    public float trustRate = -0.1f;
    [Tooltip("Game speed value, 0 is paused game, 1 is normal speed, 2 is fast speed")]
    public int speedValue = 1;
    [Tooltip("Keep tracks of the current money value")]
    public ulong moneyValue;

    [Header("Script instance")]
    [Tooltip("The GameEndScreen script")]
    [SerializeField] private GameEndScreen gameEndScreen;

    [Tooltip("Total of all the dead people")]
    public long totalDeaths = 0;
    [Tooltip("Total of all the addicted people")]
    public long totalAddicted = 0;
    [Tooltip("Total of all the healthy people")]
    public long totalHealthy = 0;
    [Tooltip("Total of all people")]
    public long globalPopulation = 0;

    private string name;

    public static GameManager Instance { get; private set; }


    private void Start()
    {
        inGameUI.SetActive(false);
        gameEndPanel.SetActive(false);

        foreach(Region region in regions)
        {
            totalHealthy += region.healthyPopulation;
            globalPopulation = totalHealthy;
        }
    }

    public void StartGame()
    {
        Instance = this;
        chooseName.SetActive(false);
        inGameUI.SetActive(true);
        this.name = nameField.text;
        nameText.text = this.name;

        InvokeRepeating(nameof(GameLoop), 0, 0.5f);
    }

    // The Gameloop, due to time we have to keep it.
    // Hard to maintain because of the time
    // Could be more optimised

    /// <summary>
    /// The Gameloop, for now, check every 0.5 second if a region is buying cigarette
    /// if true, check speed value, and then apply population evolution
    /// </summary>
    private void GameLoop()
    {
        foreach (Region region in regions)
        {
            if (region.isBuyingCigarettes)
            {
                // When speedValue is equal to zero, the game is on pause
                if (speedValue != 0)
                {
                    int newAddicts = (int)Mathf.Min(region.healthyPopulation, Mathf.FloorToInt(region.addictedPopulation * cigarette.influence));
                    int deaths = Mathf.FloorToInt(region.addictedPopulation * cigarette.toxicite);

                    // evolution in global population
                    ApplyGlobalEvolution(deaths, newAddicts);
                    

                    // Evolution in local population
                    region.ApplyEvolution(deaths, newAddicts);                    

                    moneyValue += (ulong)(region.addictedPopulation * cigarette.price);
                }

                region.UpdateVisuals();
            }
        }

        // Evolution of trust
        if(speedValue != 0)
        {
            UpdateTrustLoss(trustRate);
        }

        // parse Millions/Billions
        deathsText.text = $"Morts: {ParseNumber(this.totalDeaths)}";
        addictedText.text = $"Accros: {ParseNumber(this.totalAddicted)}";
        moneyText.text = $"Argent: {ParseNumber(moneyValue)}€";

        CheckHealthyPeople();
    }

    /// <summary>
    /// Keeps track of the global population
    /// </summary>
    /// <param name="deaths">All the people who die today</param>
    /// <param name="newAddicted">All the people who start smoking</param>
    private void ApplyGlobalEvolution(long deaths, long newAddicted)
    {
        // Evolution of population
        this.totalHealthy -= newAddicted;
        this.totalAddicted += newAddicted - deaths;
        this.totalDeaths += deaths;

        // Clamp pour éviter les valeurs négatives
        this.totalHealthy = (long)Mathf.Max(this.totalHealthy, 0);
        this.totalAddicted = (long)Mathf.Max(this.totalAddicted, 0);
        this.totalDeaths = (long)Mathf.Max(this.totalDeaths, 0);
    }

    /// <summary>
    /// Check if there are no more healthy people
    /// When true, the game ends and show the victory screen
    /// </summary>
    private void CheckHealthyPeople()
    {
        if(totalHealthy < 1)
        {
            speedValue = 0;
            gameEndScreen.SetVictory(true);
            gameEndScreen.ApplyDescription();
            gameEndPanel.SetActive(true);
        }
    }

    /// <summary>
    /// Parse the number to Millions/Billions keeping 2 decimal places
    /// </summary>
    /// <param name="number">in ulong</param>
    /// <returns></returns>
    public static string ParseNumber(ulong number)
    {
        return number switch
        {
            < 1000 => number.ToString(),
            < 1000000 => $"{number / 1000f:F2}K",
            < 1000000000 => $"{number / 1000000f:F2}M",
            _ => $"{number / 1000000000f:F2}B"
        };
    }

    /// <summary>
    /// Parse the number to Millions/Billions keeping 2 decimal places
    /// </summary>
    /// <param name="number">in long</param>
    /// <returns></returns>
    public static string ParseNumber(long number)
    {
        return number switch
        {
            < 1000 => number.ToString(),
            < 1000000 => $"{number / 1000f:F2}K",
            < 1000000000 => $"{number / 1000000f:F2}M",
            _ => $"{number / 1000000000f:F2}B"
        };
    }

    void OnMouseUpAsButton()
    {
        regionTooltip.Hide();
        for (int i = 0; i < regions.Length; i++)
        {
            regions[i].isSelected = false;
        }
    }

    private void Update()
    {
        // On Click
        if (Input.GetMouseButtonDown(0))
        {
            var position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            position.z = 0;
            Instantiate(particleClick, position, Quaternion.identity);
        }
    }

    /// <summary>
    /// Apply the trust Rate on the trust Slider
    /// </summary>
    /// <param name="trustRate">The trust rate to apply</param>
    private void UpdateTrustLoss(float trustRate)
    {
        trustBar.value += trustRate;
        TrustText.text = trustBar.value.ToString("0");

        CheckTrust();
    }

    /// <summary>
    /// Check the trust value, if it's under 0, the game is lost
    /// Show the game over screen
    /// </summary>
    private void CheckTrust()
    {
        if(trustBar.value <= 0)
        {
            speedValue = 0;
            trustBar.value = 0;
            TrustText.text = trustBar.value.ToString("0");
            gameEndScreen.SetVictory(false);
            gameEndScreen.ApplyDescription();  
            gameEndPanel.SetActive(true);

        }
    }

    /// <summary>
    /// Add trust by flat point
    /// Example : A +20 in trust or a -3.5
    /// </summary>
    /// <param name="trust">The trust value to add</param>
    private void UpdateTrust(float trust)
    {
        trustBar.value += trust;
        TrustText.text = trustBar.value.ToString("00");
    }

    #region Reset Methods
    /// <summary>
    /// Restart game by setting all values by default
    /// </summary>
    public void RestartGame()
    {
        ResetPopulation();

        ResetCigarette();

        ResetUpgrade();
    }

    /// <summary>
    /// Lock all the upgrades
    /// </summary>
    private void ResetUpgrade()
    {
        UpgradeManager.instance.ResetUpgradeList();
    }

    /// <summary>
    /// Reset the cigarette coeff
    /// </summary>
    private void ResetCigarette()
    {
        cigarette.price = 1f;
        cigarette.toxicite = 0.1f;
        cigarette.addiction = 0.9f;
        cigarette.influence = 0.5f;
}

    /// <summary>
    /// Reset the population to all Regions
    /// </summary>
    private void ResetPopulation()
    {
        foreach (Region region in regions) 
        { 
            region.ResetPopulation();
        }
    }
    #endregion
}