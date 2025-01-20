using UnityEngine;

public class Upgrade : MonoBehaviour
{
    public string upgradeName;
    [TextArea]
    public string description;
    
    public int actualLevel;
    public ulong[] upgradeCost;
    
    public ulong[] prices;
    
    [Tooltip("Multiplicator")]
    public float[] toxicities;
    
    [Tooltip("1 = maximum addiction, 2 = half addiction, 3 = third addiction, etc..."), Min(1)]
    public float[] addictions;
    
    [Tooltip("Multiplicator")]
    public float[] influences;
}
