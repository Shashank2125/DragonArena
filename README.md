# Dragon Arena

A small 2.5D top-down dragon battle game developed in Unity as a technical assessment.

## Overview

The game features a player-controlled dragon fighting an AI-controlled enemy dragon inside a bounded arena.

The player can move around the arena, use three different abilities, and defeat the enemy dragon. The AI dragon chases the player, maintains combat distance, and uses abilities based on the player's distance.

## Engine

- Unity 6000.3.8f1
- 3D URP
- C#
- Unity Input System

## Controls

| Key | Action        |
| --- | ------------- |
| W   | Move Forward  |
| A   | Move Left     |
| S   | Move Backward |
| D   | Move Right    |
| Q   | Fire Attack   |
| E   | Tail Attack   |
| R   | Fly Attack    |

## Abilities

### Fire Attack — Q

A ranged fire attack that launches a fireball toward the enemy.

- Damage: 20
- Cooldown: 3 seconds

### Tail Attack — E

A close-range melee attack using the dragon's tail.

- Damage: 30
- Cooldown: 2 seconds

### Fly Attack — R

The dragon takes off, attacks from the air with a fireball, and lands.

- Damage: 40
- Cooldown: 6 seconds

## AI

The enemy dragon uses a simple state-based AI.

The AI can:

- Chase the player when outside combat range
- Face and attack the player when within range
- Reposition when too close
- Select attacks based on distance
- Respect ability cooldowns

## UI

The game includes:

- Player health bar
- Enemy health bar
- Ability icons
- Ability cooldown indicators
- Ability key labels
- Hit feedback
- Winner screen
- Restart button

## Assets

Dragon models and animations were sourced from free Unity Asset Store content.

### Dragon Asset

Dragon the Soul Eater and Dragon Boar  
Created by Dungeon Mason.

Additional visual effects, UI elements, and audio were created using available free assets and Unity's built-in systems.

## Audio

Combat audio was added for:

- Fire Attack
- Tail Attack
- Fly Attack
- Dragon hit feedback

## Project Structure

The main battle scene is:

`BattleArena`

Main gameplay systems include:

- PlayerMovement
- PlayerCombat
- DragonAI
- DragonHealth
- FireAttack
- TailAttack
- FlyAttack
- FireballProjectile
- HealthBarUI
- AbilityCooldownUI
- BattleManager

## How to Run

1. Open the project using Unity 6.
2. Open the `BattleArena` scene.
3. Press Play.
4. Use WASD to move.
5. Use Q, E and R to use abilities.
6. Defeat the enemy dragon.

## AI Usage Note

AI tools were used during development as a development assistant.

AI was used for:

- Debugging Unity and C# issues
- Reviewing and improving gameplay scripts
- Helping structure the enemy AI state machine
- Troubleshooting Unity Input System issues
- Assisting with ability cooldown UI
- Suggesting implementation approaches for combat, VFX and audio
- Explaining Unity errors and possible fixes

AI-generated suggestions were tested and adapted manually in Unity.

One example where AI output required correction was the dragon's Fly Attack. The initial timing approach did not produce the desired delay between the flight animation and fireball launch. The implementation was adjusted manually using a coroutine with explicit takeoff, delay, projectile, and landing phases.

The final implementation was tested in Unity and adjusted to match the intended gameplay behavior.

## Development Notes

The project prioritizes a complete playable battle loop:

1. Player and enemy spawn in the arena.
2. Player can move and attack.
3. Enemy AI detects and approaches the player.
4. Both dragons can use combat abilities.
5. Health decreases when attacks connect.
6. Hit feedback and audio provide combat feedback.
7. When a dragon reaches zero health, the winner screen is displayed.
8. The player can restart the battle.

## Author

Shashank Shekhar Singh
