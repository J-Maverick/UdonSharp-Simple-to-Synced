
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.SDK3.Components;

public class ObjectReturn : UdonSharpBehaviour
{
    public VRCObjectPool pool;

    private void OnTriggerEnter(Collider other)
    {
        if (pool == null) return; 

        if (Networking.GetOwner(other.gameObject).isLocal)
        {
            Networking.SetOwner(Networking.LocalPlayer, pool.gameObject);
            pool.Return(other.gameObject);
        }
    }
}
