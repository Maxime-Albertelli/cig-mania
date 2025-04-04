using System;
using UnityEngine;

/// <summary>
/// Informations about cigarette
/// </summary>
[Serializable]
public class Cigarette
{
    [Tooltip("Price per cigarette")]
    public float price = 1f;
    [Tooltip("Toxicity coeff, the higher the value is, more people will die")]
    public float toxicite = 0.1f;
    [Tooltip("Addiction coeff, the lower the value is, less people will be addicted")]
    public float addiction = 0.1f;
    [Tooltip("influence coeff, the higher the value is, more people will become addicted")]
    public float influence = 0.5f;
}