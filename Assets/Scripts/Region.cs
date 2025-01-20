using TMPro;
using UnityEngine;
using UnityEngine.U2D;

/// <summary>
/// Informations about a region
/// </summary>
public class Region : MonoBehaviour
{
    [field:SerializeField] public string regionName { get; private set; }
    [SerializeField] private SpriteShapeRenderer spriteShape;
    [SerializeField] private GameManager manager;

    
    private const float MaxAlpha = 0.80f;

    public ulong population;
    public ulong addictedPopulation;

    public bool isBuyingCigarettes;
    public bool isSelected;

    private void Awake()
    {
        manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    public void UpdateVisuals()
    {
        var percentage = (float)addictedPopulation / population;
        var addictColor = Color.red;
        var selectColor = Color.white; // Color on click
        selectColor.a = 0.25f;
        addictColor.a = percentage * MaxAlpha;
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
}