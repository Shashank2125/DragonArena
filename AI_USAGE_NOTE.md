# AI Usage Note

I used ChatGPT during development as a coding assistant and for debugging different Unity issues.

## Tools Used

- ChatGPT

## How AI Was Used

I mainly used AI to:

- Debug Unity C# scripts and runtime errors
- Understand Unity Input System issues
- Design and improve the enemy AI state machine
- Implement and refine the three combat abilities
- Troubleshoot the fireball projectile
- Set up health bars and ability cooldown UI
- Review code and find possible bugs
- Understand Unity concepts and find possible solutions when I got stuck

## Example of AI Being Wrong

One issue I faced was with the fireball projectile. Initially, the fireball was spawning from the dragon's mouth, but it was not moving toward the enemy correctly.

I tested the suggested implementation in Unity and found that it was not behaving as expected. I then changed the implementation by creating a separate `FireballProjectile` component. It takes the target, calculates the direction toward it, moves the fireball toward the target, and applies damage when it reaches the enemy.

I tested the change in Unity and adjusted it until the fireball was spawning, moving, and dealing damage correctly.

Another issue came up with the enemy's Fly Attack. The normal AI movement was still running while the Fly Attack was controlling the dragon's position. This caused the enemy to move incorrectly when it was close to the player.

I found the issue during testing and changed the AI so that normal movement and attack decisions are paused while `FlyAttack.IsFlying` is active. This lets the Fly Attack control the dragon until it lands, after which the normal AI resumes.

I also found a physics issue where the enemy could land directly on the player after the Fly Attack. Since their colliders were overlapping, Unity's physics system could push the enemy away unexpectedly.

I fixed this by disabling physical collision between the Player and Enemy dragon layers using Unity's Layer Collision Matrix. The dragons can still attack and damage each other, but they no longer physically push each other around.

## Developer Verification

I did not use AI-generated code without testing it. I reviewed the suggestions, made changes where necessary, and tested the implementations directly in Unity.

The final gameplay systems, including movement, combat, enemy AI, health, abilities, cooldowns, UI, audio, VFX, victory conditions, and restart functionality were tested in the Unity Editor.
