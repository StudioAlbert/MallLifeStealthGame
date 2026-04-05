# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

**MallLife** is a Unity 6 prototype (v6000.3.10f1). It uses the Universal Render Pipeline (URP) and the new Input System.

## Development Environment

This is a Unity project — there are no CLI build/test commands. All building, running, and testing is done through the **Unity Editor** (open `MallLife-UnityProject/` as a Unity project).

- **Unity version:** 6000.3.10f1
- **Render pipeline:** URP 17.3.0 (PC and Mobile render asset variants in `Assets/08 - Settings/`)
- **Input system:** Unity Input System 1.18.0 (new input system, legacy disabled)
- **Navigation:** Unity AI Navigation 2.0.10

## Asset Folder Structure

```
Assets/
  00 - Imports_Packs/   # Third-party assets and packages
  01 - Scenes/          # Unity scenes
  02 - Prefabs/         # Prefabs
  03 - Scripts/         # C# source code
  04 - Animations/      # Animation clips and controllers
  05 - Behaviors/       # Behavior graphs / visual scripting
  06 - Rendering/       # Shaders, materials, VFX
  07 - Audio/           # Audio clips and mixers
  08 - Settings/        # Project settings assets (URP configs, Input actions)
```

## Input System Architecture

Input is handled via two layered classes in `Assets/03 - Scripts/Inputs/`:

- **`GameControls.cs`** — Auto-generated from `Assets/08 - Settings/Inputs/GameControls.inputactions`. Do **not** edit manually; regenerate via Unity when the `.inputactions` asset changes.
- **`GameInputs.cs` (`GameInput` MonoBehaviour)** — Wraps `GameControls`, registers callbacks in `Awake`, and exposes current input state as plain public fields (`Vector2 Move`, `bool Interact`, etc.). Add this to a persistent GameObject (e.g., GameManager). Other scripts read from this component rather than subscribing to the Input System directly.

**Control schemes:** `Keyboard&Mouse` and `Gamepad`. All actions are in the `Player` action map.

| Action | KB&M | Gamepad |
|---|---|---|
| Move | WASD | Left Stick |
| CameraOrbit | Mouse Delta | Right Stick |
| UseObject | F | Button South |
| Interact | E | Button East |
| ChangeVehicle | V | Button West |
| ChangeObject | Q | Left Shoulder |
| MenuEquipment | I | D-Pad Up |
| MenuObjectives | J | D-Pad Down |
| MenuStealthView | M | D-Pad Right |

## Key Conventions

- Scripts that need input should reference the `GameInput` component, not instantiate `GameControls` directly.
- URP render pipeline assets are split into PC (`PC_RPAsset.asset`, `PC_Renderer.asset`) and Mobile variants (`Mobile_RPAsset.asset`, `Mobile_Renderer.asset`).
- Visual Scripting (`com.unity.visualscripting`) is included — behavior graphs live in `Assets/05 - Behaviors/`.
