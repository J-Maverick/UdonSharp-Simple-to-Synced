
using UdonSharp;
using UnityEngine;
using VRC.Dynamics;
using VRC.SDK3.UdonNetworkCalling;
using VRC.SDKBase;
using VRC.SDK3.Dynamics.PhysBone.Components;

public class MushroomDrum : UdonSharpBehaviour
{
    public AudioSource audioSource;

    public VRCPhysBone physBone;

    public float maxSpeed = 20f;
    public float minSpeed = 2f;

    public float minVolume = 0.1f;
    public float maxVolume = 3f;

    public float pitchVariation = 0.01f;
    public float cooldownTime = 0.05f;
    private float lastPlayTime = 0f;

    [NetworkCallable]
    public void PlayAudio(float pitch, float volume)
    {
        if (Time.realtimeSinceStartup - lastPlayTime < cooldownTime)
        {
            return;
        }
        lastPlayTime = Time.realtimeSinceStartup;

        Debug.LogFormat("{0}: Playing audio with pitch {1} and volume {2}", name, pitch, volume);

        // Add some random variation to the pitch
        audioSource.pitch = pitch + Random.Range(-pitchVariation, pitchVariation); 
        audioSource.enabled = true;
        audioSource.PlayOneShot(audioSource.clip, volume);

        // Disable the audio source after the clip has finished playing
        SendCustomEventDelayedSeconds(nameof(DisableAudioSource), audioSource.clip.length + 0.1f); 
    }

    public void DisableAudioSource()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.enabled = false;
        }
    }

    public override void OnContactEnter(ContactEnterInfo contactInfo)
    {
        VRCPlayerApi player = contactInfo.contactSender.player;
        if (player != null && !contactInfo.contactSender.player.isLocal)
        {
            return;
        }
        Debug.LogFormat("{0}: Contact entered with speed {1}", name, contactInfo.enterVelocity.magnitude);
        float speed = contactInfo.enterVelocity.magnitude;
        float clampedSpeed = Mathf.Clamp(speed, minSpeed, maxSpeed);
        float normalizedSpeed = (clampedSpeed - minSpeed) / (maxSpeed - minSpeed);
        float volume = Mathf.Lerp(minVolume, maxVolume, normalizedSpeed);
        SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, nameof(PlayAudio), physBone.Stretch + 1f, volume);
    }

}
