using UdonSharp;
using UnityEngine;
using VRC.SDKBase;

public class TeleportPad : UdonSharpBehaviour
{
    public TeleportPad targetPad;
    public float cooldownSeconds = 1f;

    public float nextTeleportTime;
    
    public void TriggerCooldown(float currentTime)
    {
        nextTeleportTime = currentTime + cooldownSeconds;
    }

    public override void OnPlayerTriggerEnter(VRCPlayerApi player)
    {
        if (player == null || !player.isLocal) return;
        if (targetPad == null) return;

        float currentTime = Time.timeSinceLevelLoad;
        if (currentTime < nextTeleportTime) return;

        TriggerCooldown(currentTime);

        if (targetPad != null) {
            targetPad.TriggerCooldown(currentTime);
        }
        Vector3 playerPosition = player.GetPosition();
        Vector3 localOffset = transform.InverseTransformPoint(playerPosition);
        Vector3 targetPosition = targetPad.transform.TransformPoint(localOffset);

        player.TeleportTo(targetPosition, player.GetRotation());
    }
}
