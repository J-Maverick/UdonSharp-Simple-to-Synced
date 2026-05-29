
using UdonSharp;
using UnityEngine;
using VRC.SDK3.UdonNetworkCalling;
using VRC.SDKBase;

public class DoorToggleSynced : UdonSharpBehaviour
{
    public Transform doorTransform;
    public bool isOpen = false;
    public float swingSpeed = 90f; // Degrees per second
    public Vector3 closedEuler = Vector3.zero;
    public Vector3 openEuler = new Vector3(0f, 90f, 0f);
    private Quaternion targetRotation = Quaternion.identity;
    private Vector3 _initialEuler;
    private bool _isMoving = false;
    private bool startsOpen = false;

    private void Start()
    {
        _initialEuler = doorTransform.localEulerAngles;
        startsOpen = isOpen;
    }

    public override void Interact()
    {
        SendDoorEvent();
    }

    public override void OnPlayerJoined(VRCPlayerApi player)
    {
        if (Networking.GetOwner(gameObject).isLocal)
        {
            SendDoorEvent();
        }
    }

    public void SendDoorEvent()
    {
        if (isOpen)
        {
            SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, nameof(CloseDoor));
        }
        else
        {
            SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, nameof(OpenDoor));
        }
    }

    [NetworkCallable]
    public void OpenDoor()
    {
        isOpen = true;
        Vector3 targetEuler = startsOpen ? _initialEuler - openEuler : _initialEuler + openEuler;
        targetRotation = Quaternion.Euler(targetEuler);
        _isMoving = true;
    }

    [NetworkCallable]
    public void CloseDoor()
    {
        isOpen = false;
        Vector3 targetEuler = startsOpen ? _initialEuler - closedEuler : _initialEuler + closedEuler;
        targetRotation = Quaternion.Euler(targetEuler);
        _isMoving = true;
    }

    public void FixedUpdate()
    {
        if (_isMoving)
        {
            // Smoothly rotate towards the target
            doorTransform.localRotation = Quaternion.RotateTowards(doorTransform.localRotation, targetRotation, swingSpeed * Time.fixedDeltaTime);

            // Check if we've reached the target
            if (Quaternion.Angle(doorTransform.localRotation, targetRotation) < 0.1f)
            {
                doorTransform.localRotation = targetRotation;
                _isMoving = false;
            }
        }
    }
}
