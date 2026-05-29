
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.SDK3.Persistence;

public class PositionPersistence : UdonSharpBehaviour
{
    public float updateInterval = 0.5f;
    VRCPlayerApi localPlayer;

    public override void OnPlayerRestored(VRCPlayerApi player)
    {
        if (player.isLocal)
        {
            localPlayer = player;

            Vector3 lastPosition;
            Quaternion lastRotation;

            bool hasSavedPosition = PlayerData.TryGetVector3(player, "LastPosition", out lastPosition);
            bool hasSavedRotation = PlayerData.TryGetQuaternion(player, "LastRotation", out lastRotation);

            if (hasSavedPosition && hasSavedRotation)
            {
                player.TeleportTo(lastPosition, lastRotation);
            }
            SendCustomEventDelayedSeconds(nameof(UpdatePlayerPosition), updateInterval);
        }
    }

    public void UpdatePlayerPosition()
    {
        if (localPlayer.IsPlayerGrounded())
        {
            PlayerData.SetVector3("LastPosition", localPlayer.GetPosition());
            PlayerData.SetQuaternion("LastRotation", localPlayer.GetRotation());
        }
        SendCustomEventDelayedSeconds(nameof(UpdatePlayerPosition), updateInterval);
    }
}
