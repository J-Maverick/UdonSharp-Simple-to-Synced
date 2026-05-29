using UdonSharp;
using UnityEngine;

public class DoorToggleLocal : UdonSharpBehaviour
{
    public Transform doorTransform;
    public Vector3 closedRotation;
    public Vector3 openRotation = new Vector3(0f, 90f, 0f);
    public float swingSpeed = 90f; // Degrees per second

    private bool isOpen;
    private Quaternion targetRotation;

    private void Start()
    {
        targetRotation = Quaternion.Euler(closedRotation);

        if (doorTransform != null) {
            doorTransform.localRotation = targetRotation;
        }
    }

    private void FixedUpdate()
    {
        if (doorTransform == null) return;

        doorTransform.localRotation = Quaternion.RotateTowards(
            doorTransform.localRotation,
            targetRotation,
            Time.fixedDeltaTime * swingSpeed
        );
    }

    public override void Interact()
    {
        isOpen = !isOpen;

        if (isOpen) {
            targetRotation = Quaternion.Euler(openRotation);
        }
        else {
            targetRotation = Quaternion.Euler(closedRotation);
        }
    }
}
