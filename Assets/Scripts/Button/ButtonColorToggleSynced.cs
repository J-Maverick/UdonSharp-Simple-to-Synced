
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using TMPro;

[UdonBehaviourSyncMode(BehaviourSyncMode.Manual)]
public class ButtonColorToggleSynced : UdonSharpBehaviour
{
    public Renderer buttonRenderer;
    public TMP_Text buttonText;

    public Color colorA = Color.red;
    public Color colorB = Color.green;

    [UdonSynced] public bool isOn = false;
    [UdonSynced] public int nButtonPresses;

    private void Start()
    {
        ApplyColor();
        UpdateText();
    }

    public override void OnDeserialization()
    {
        ApplyColor();
        UpdateText();
    }

    public override void OnPlayerJoined(VRCPlayerApi player)
    {
        RequestSerialization();
    }

    public override void Interact()
    {
        Networking.SetOwner(Networking.LocalPlayer, gameObject);
        isOn = !isOn;
        nButtonPresses++;
        ApplyColor();
        UpdateText();
        RequestSerialization();
    }

    private void UpdateText()
    {
        if (buttonText != null)
        {
            buttonText.text = $"Presses: {nButtonPresses}";
        }
    }

    private void ApplyColor()
    {
        if (buttonRenderer == null) return;
        
        buttonRenderer.material.color = isOn ? colorA : colorB;
    }       
}
