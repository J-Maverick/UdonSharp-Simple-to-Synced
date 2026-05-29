
using UdonSharp;
using UnityEngine;
using VRC.SDK3.Components;
using VRC.SDKBase;

public class ObjectSpawner : UdonSharpBehaviour
{
    public VRCObjectPool pool;
    public Transform spawnPoint;

    // This method can be called from a UI button or any other event to spawn an object from the pool, it assumes it's called by the local player
    public void SpawnObject()
    {
        if (pool == null) return;

        Networking.SetOwner(Networking.LocalPlayer, pool.gameObject); // Ensure the local player is the owner of the pool before spawning
        GameObject spawnedObject = pool.TryToSpawn();

        if (spawnedObject != null)
        {
            Networking.SetOwner(Networking.LocalPlayer, spawnedObject); // Set the local player as the owner of the spawned object
            spawnedObject.transform.SetPositionAndRotation(spawnPoint.position, spawnPoint.rotation);
            Rigidbody rb = spawnedObject.GetComponent<Rigidbody>();
            if (rb != null) {
                rb.velocity = Vector3.zero; // Reset velocity
                rb.angularVelocity = Vector3.zero; // Reset angular velocity
            }

        }
    }
}
