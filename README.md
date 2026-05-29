# Udon#: From Simple to Synced - Example Project

This repository is a collection of Unity + UdonSharp examples that accompany the **"Udon#: From Simple to Synced"** presentation for **Furality Ultra 2026**.

The project demonstrates how to move from purely local interactions to synchronized multiplayer behaviors in VRChat worlds.

## What This Repo Contains

- Local interaction patterns (single-player style behavior)
- Synced interaction patterns (state and events across players)
- Ownership transfer examples
- Late-joiner handling examples
- Persistence demos
- AudioSource usage
- Object Pooling
- Particles and Particle collisions

The main demo scene is:

- `Assets/Scenes/DemoScene.unity`

## Prerequisites

- Unity **2022.3.22f1**
- VRChat Creator Companion (VCC)
- VRChat Worlds SDK + UdonSharp packages

This project includes VRChat package references in `Packages/packages-lock.json` (for example `com.vrchat.worlds`, `com.vrchat.base`, and the VPM resolver).

## Getting Started

Choose one of the following setup options:

### Option 1: Clone the full project

1. Clone this repository.
2. Open it through **VRChat Creator Companion** as an existing project.
3. Ensure project packages resolve successfully.
4. Open `Assets/Scenes/DemoScene.unity`.
5. Enter Play Mode, or use ClientSim for local testing.

### Option 2: Download the `.unitypackage` from Releases

1. Download the latest `.unitypackage` from the repository **Releases** section.
2. Open your target VRChat world project in Unity.
3. Import the `.unitypackage` (`Assets` -> `Import Package` -> `Custom Package...`).
4. Resolve any package prompts, if shown.
5. Test the imported examples in your scene.

## Example Scripts (By Topic)

### Local vs Synced Basics

- `Assets/Scripts/Button/ButtonColorToggleLocal.cs`
  - Local-only button color toggle.
- `Assets/Scripts/Button/ButtonColorToggleSynced.cs`
  - Synced button state + press counter with serialization.

- `Assets/Scripts/Door/DoorToggleLocal.cs`
  - Local door rotation and interaction.
- `Assets/Scripts/Door/DoorToggleSynced.cs`
  - Network-called open/close door events across all players.

### Physics / Audio / Interaction

- `Assets/Scripts/MushroomDrum/MushroomDrum.cs`
  - PhysBone contact-driven audio with network event playback.
- `Assets/Scripts/MushroomDrum/PhysbonePoisson.cs`
  - Visual squash/stretch effect based on PhysBone stretch and squish.

### Movement and Persistence

- `Assets/Scripts/TeleportPad/TeleportPad.cs`
  - Trigger-based teleport between paired pads with cooldown.
- `Assets/Scripts/PlayerPositionPersistence/PositionPersistence.cs`
  - Restores and periodically saves local player position/rotation using persistent PlayerData.

### Object Pooling

- `Assets/Scripts/ObjectPoolingDemo/ObjectSpawner.cs`
  - Ownership-safe object spawning from a VRC object pool.
- `Assets/Scripts/ObjectPoolingDemo/ObjectReturn.cs`
  - Returns pooled objects on trigger.
- `Assets/Scripts/ObjectPoolingDemo/RandomizeColor.cs`
  - Network-call color randomization and late-join sync.

### Synced Pickup and Effects

- `Assets/Scripts/FireWand/FireWand.cs`
  - Synced pickup behavior with particle events and serialized gem color.
- `Assets/Scripts/FireWand/HeatableMetal.cs`
  - Continuously synced heating/cooling effect via emission color.

## Suggested Learning Path

1. Start with local button and door examples.
2. Compare with their synced counterparts.
3. Review ownership-sensitive examples (object pool, fire wand).
4. Explore late-join behavior (`OnPlayerJoined`, `OnDeserialization`, `OnPlayerRestored`).
5. Explore Persistent PlayerData and PlayerObjects
5. Apply the same patterns to your own world systems.

## Notes

- This is a teaching/demo repository intended for learning patterns, not a production framework.
- Some assets are third-party or package-provided examples (for example TextMesh Pro sample content).

## Presentation Context

Built for the **Furality Ultra 2026** session:

**Udon# Simple to Synced**

If you are attending the talk, this repo is meant to be your companion reference for the code patterns shown live.
