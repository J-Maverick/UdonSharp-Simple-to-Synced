
using UdonSharp;
using UnityEngine;

[UdonBehaviourSyncMode(BehaviourSyncMode.Continuous)]
public class HeatableMetal : UdonSharpBehaviour
{
    public Renderer targetRenderer;
    public Material targetMaterial;
    
    [ColorUsage(true, true)]    
    public Color initialColor = Color.black;

    [ColorUsage(true, true)]    
    public Color heatedColor = Color.red;

    public LayerMask heatSourceLayer;

    public float temperatureIncreaseRate = 0.1f;
    public float coolingRate = 0.05f;
    public float maxTemperature = 1000f;

    [UdonSynced(UdonSyncMode.Linear)] public float currentTemperature = 0f;

    void Start()
    {
        targetMaterial = targetRenderer.material;
        initialColor = targetMaterial.GetColor("_EmissionColor");
    }

    public void OnParticleCollision(GameObject other)
    {
        if ((1 << other.layer & heatSourceLayer) != 0)
        {
            currentTemperature += temperatureIncreaseRate;
            if (currentTemperature > maxTemperature)
            {
                currentTemperature = maxTemperature;
            }
            UpdateColor();
        }
    }

    private void UpdateColor()
    {
        targetMaterial.SetColor("_EmissionColor", Color.Lerp(initialColor, heatedColor, currentTemperature / maxTemperature));
    }

    private void Update()
    {
        if (currentTemperature > 0)
        {
            currentTemperature -= coolingRate * Time.deltaTime;
            if (currentTemperature < 0)
            {
                currentTemperature = 0;
            }
            UpdateColor();
        }
    }
}
