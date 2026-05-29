using UdonSharp;
using UnityEngine;
using VRC.SDK3.UdonNetworkCalling;
using VRC.SDKBase;

public class RandomizeColor : UdonSharpBehaviour
{

    public Renderer targetRenderer;
    public Material targetMaterial;
    private Color currentColor;

    public void OnEnable() {
        if (targetRenderer != null && targetMaterial == null) {
                targetMaterial = targetRenderer.material;
        }
        if (Networking.IsOwner(gameObject))
        {
            Randomize();
        }
    }

    public override void OnPlayerJoined(VRCPlayerApi player)
    {
        if (Networking.IsOwner(gameObject)) {
            SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.Others, nameof(SetColor), currentColor);
        }
    }
    
    public void Randomize()
    {
        Color randomColor = new Color(Random.value, Random.value, Random.value);
        SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, nameof(SetColor), randomColor);
    }

    [NetworkCallable]
    public void SetColor(Color newColor)
    {
        targetMaterial.color = newColor;
        currentColor = newColor;
    }
}
