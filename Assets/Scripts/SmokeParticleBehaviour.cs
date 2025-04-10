using UnityEngine;

public class SmokeParticleBehaviour : MonoBehaviour
{
    private ParticleSystem smokeParticle;
    private ParticleSystem.EmissionModule smokeEmission;
    private Region region;
    [SerializeField] public float regionSizeCoefficient; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        region = GetComponentInParent<Region>();
        smokeParticle = GetComponent<ParticleSystem>();
        smokeEmission = smokeParticle.emission;
    }

    // Update is called once per frame
    void Update()
    {
        smokeEmission.rateOverTime = (2+(10*region.addictedPopulation)/region.GetMaxPopulation())*regionSizeCoefficient;
    }
}
