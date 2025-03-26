using System;
using TMPro;
using UnityEngine;

/// <summary>
/// Game Manager
/// </summary>
public class GameManager : MonoBehaviour
{
    public Cigarette cigarette;

    [SerializeField] public Region[] regions;

    [SerializeField] private TMP_Text deathsText;
    [SerializeField] private TMP_Text addictedText;
    [SerializeField] private TMP_Text moneyText;

    [SerializeField] private TMP_Text nameText;

    [SerializeField] private GameObject chooseName;
    [SerializeField] private TMP_Text nameField;
    
    [SerializeField] private Tooltip regionTooltip;

    [SerializeField] private GameObject inGameUI;
    [SerializeField] private GameObject particleClick;
    
    public ulong money;
    
    private ulong _totalDeaths;
    private ulong _totalAddicted;

    private string _name;

    //Game speed
    public int speedValue = 1;
    public static GameManager Instance { get; private set; }


    private void Start()
    {
        inGameUI.SetActive(false);
    }

    public void StartGame()
    {
        Instance = this;
        chooseName.SetActive(false);
        inGameUI.SetActive(true);
        _name = nameField.text;
        nameText.text = _name;

        InvokeRepeating(nameof(GameLoop), 0, 0.5f);
    }

    private void GameLoop()
    {
        _totalAddicted = 0;
        foreach (var region in regions)
        {
            if (!region.isBuyingCigarettes) continue;

            // When speedValue is equal to zero, the game is on pause
            if (speedValue != 0)
            {
                var userLoss = (int)(region.addictedPopulation * (1 - cigarette.addiction));

                var newUsers = Mathf.Ceil(
                    Mathf.Sqrt(region.addictedPopulation) * cigarette.influence
                );

                var deaths = Mathf.Ceil(region.addictedPopulation * cigarette.toxicity);
                _totalDeaths += (ulong)deaths;

                // Evolution of population
                region.addictedPopulation -= (ulong)deaths;
                region.addictedPopulation -= (ulong)userLoss;
                region.population -= (ulong)deaths;
                region.addictedPopulation += (ulong)newUsers;
                if (region.addictedPopulation > region.population)
                    region.addictedPopulation = region.population;

                money += (ulong)(userLoss * cigarette.price);
            }
            

            region.UpdateVisuals();

            _totalAddicted += region.addictedPopulation;
        }

        // parse Millions/Billions
        deathsText.text = $"Morts: {ParseNumber(_totalDeaths)}";
        addictedText.text = $"Accros: {ParseNumber(_totalAddicted)}";
        moneyText.text = $"Argent: {ParseNumber(money)}€";   
    }

    /// <summary>
    /// Parse the number to Millions/Billions keeping 2 decimal places
    /// </summary>
    /// <param name="number"></param>
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
}