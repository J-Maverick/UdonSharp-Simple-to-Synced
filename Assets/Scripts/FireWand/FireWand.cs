
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;

public class FireWand : UdonSharpBehaviour
{
    public ParticleSystem fireParticles;
    public VRC_Pickup pickup;
    public Renderer gemRenderer;
    [UdonSynced] public Color gemColor = Color.black;

    public void Start()
    {
        if (pickup != null)
        {
            if (!Networking.IsOwner(gameObject))
            {
                pickup.pickupable = false;
            }
        }
    }

    public override void OnPlayerJoined(VRCPlayerApi player)
    {
        if (Networking.IsOwner(gameObject))
        {
            RequestSerialization();
        }
    }

    public override void OnDeserialization()
    {
        if (gemRenderer != null)
        {
            gemRenderer.material.color = gemColor;
        }
    }

    public override void OnPickupUseDown()
    {
        SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, nameof(StartParticles));
    }

    public override void OnPickupUseUp()
    {
        SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, nameof(StopParticles));
    }

    public override void OnDrop()
    {
        SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, nameof(StopParticles));
    }


    public void StartParticles()
    {
        fireParticles.Play();
    }

    public void StopParticles()
    {
        fireParticles.Stop();
    }

    public override void OnPlayerRestored(VRCPlayerApi player)
    {
        if (Networking.IsOwner(gameObject))
        {
            if (gemColor == Color.black)
            {
                gemColor = Random.ColorHSV();
            }
            gemRenderer.material.color = gemColor;
            RequestSerialization();
        }
    }
}
