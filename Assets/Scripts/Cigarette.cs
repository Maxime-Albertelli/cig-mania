using System;

/// <summary>
/// Informations about cigarette
/// </summary>
[Serializable]
public class Cigarette
{
    public float price = 1f;
    public float toxicity = 0.01f;
    public float addiction = 0.9f;
    public float influence = 1.5f;
}