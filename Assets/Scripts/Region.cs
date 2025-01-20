using UnityEngine;
using UnityEngine.U2D;

public class Region : MonoBehaviour
{
    [field:SerializeField] public string regionName { get; private set; }
    [SerializeField] private SpriteShapeRenderer spriteShape;

    
    private const float MaxAlpha = 0.80f;

    public ulong population;
    public ulong addictedPopulation;

    public bool isBuyingCigarettes;

    public void UpdateVisuals()
    {
        var percentage = (float)addictedPopulation / population;
        var color = Color.red;
        color.a = percentage * MaxAlpha;
        spriteShape.color = color;
    }

    public void OnMouseUpAsButton()
    {
        Tooltip.instance.UpdateRegion(this);
        Tooltip.instance.Show();
    }
}