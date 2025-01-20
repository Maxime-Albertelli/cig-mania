using System;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Cigarette cigarette;

    [SerializeField] private Region[] regions;

    [SerializeField] private TMP_Text deathsText;
    [SerializeField] private TMP_Text addictedText;
    [SerializeField] private TMP_Text moneyText;

    [SerializeField] private TMP_Text nameText;

    [SerializeField] private GameObject chooseName;
    [SerializeField] private TMP_Text nameField;
    
    [SerializeField] private Tooltip regionTooltip;

    [SerializeField] private GameObject upgradePanel;
    [SerializeField] private GameObject inGameUI;
    [SerializeField] private GameObject particleClick;

    [SerializeField] private GameObject prixUpgradePanel;
    [SerializeField] private GameObject addictionUpgradePanel;
    [SerializeField] private GameObject taxesUpgradePanel;
    [SerializeField] private GameObject influenceUpgradePanel;

    public ulong money;
    
    private ulong _totalDeaths;
    private ulong _totalAddicted;

    private string _name;
    public static GameManager Instance { get; private set; }


    private void Start()
    {
        inGameUI.SetActive(false);
    }

    public void ShowUpgradePanel()
    {
        upgradePanel.SetActive(true);
        prixUpgradePanel.SetActive(false);
        Tooltip.instance.Hide();
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

            var userLoss = (int)(region.addictedPopulation * (1 - cigarette.addiction));

            var newUsers = Mathf.Ceil(
                Mathf.Sqrt(region.addictedPopulation) * cigarette.influence
            );

            var deaths = Mathf.Ceil(region.addictedPopulation * cigarette.toxicity);
            _totalDeaths += (ulong)deaths;

            region.addictedPopulation -= (ulong)deaths;
            region.addictedPopulation -= (ulong)userLoss;
            region.population -= (ulong)deaths;
            region.addictedPopulation += (ulong)newUsers;
            if (region.addictedPopulation > region.population)
                region.addictedPopulation = region.population;

            money += (ulong)(userLoss * cigarette.price);

            region.UpdateVisuals();

            _totalAddicted += region.addictedPopulation;
        }

        // parse Millions/Billions
        deathsText.text = $"Deaths: {ParseNumber(_totalDeaths)}";
        addictedText.text = $"Addicted: {ParseNumber(_totalAddicted)}";
        moneyText.text = $"Money: {ParseNumber(money)}$";   
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
            < 1000000 => $"{number / 1000f}K",
            < 1000000000 => $"{number / 1000000f}M",
            _ => $"{number / 1000000000f}B"
        };
    }
    
    
    void OnMouseUpAsButton()
    {
        regionTooltip.Hide();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            position.z = 0;
            Instantiate(particleClick, position, Quaternion.identity);
        }
    }
}