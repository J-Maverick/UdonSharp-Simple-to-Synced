using UdonSharp;
using UnityEngine;

public class ButtonColorToggleLocal : UdonSharpBehaviour
{
    public Renderer buttonRenderer;
    public Color offColor = Color.red;
    public Color onColor = Color.green;

    private bool isOn;

    void Start()
    {
        UpdateColor();
    }

    public override void Interact()
    {
        isOn = !isOn;
        UpdateColor();
    }

    private void UpdateColor()
    {
        if (buttonRenderer == null) 
        {
            return;
        }
        if (isOn) 
        {
            buttonRenderer.material.color = onColor;
        } 
        else 
        {
            buttonRenderer.material.color = offColor;
        }
    }
}


