
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using VRC.SDK3.Dynamics.PhysBone.Components;

public class PhysbonePoisson : UdonSharpBehaviour
{
    public VRCPhysBone physBone;
    public float poissonRatio = 0.5f;
    public float stretch = 0f;
    public float squish = 0f;

    private Vector3 initialScale;

    void Start()
    {
        if (physBone == null)
        {
            Debug.LogError("PhysbonePoisson: No VRCPhysBone assigned!");
            return;
        }
        initialScale = physBone.transform.localScale;
    }

    void Update()
    {
        if (physBone == null) return;


        if (stretch != physBone.Stretch || squish != physBone.Squish)
        {
            ApplyPoissonEffect();
        }
    }

    private void ApplyPoissonEffect()
    {
        // Calculate the stretch factor based on the current length of the physbone
        stretch = physBone.Stretch;
        squish = physBone.Squish * physBone.maxSquish;

        // Apply the Poisson effect by adjusting the scale of the physbone
        float scaleFactor = 1 - (stretch * poissonRatio) + (squish * poissonRatio);
        physBone.transform.localScale = new Vector3(initialScale.x * scaleFactor, initialScale.y, initialScale.z * scaleFactor);
    }
}
