using UnityEngine;
using UnityEngine.UI;

public class AbilityCooldownUI : MonoBehaviour
{
    [Header("Attacks")]
    [SerializeField] private FireAttack fireAttack;
    [SerializeField] private TailAttack tailAttack;
    [SerializeField] private FlyAttack flyAttack;

    [Header("Cooldown Overlays")]
    [SerializeField] private Image fireCooldown;
    [SerializeField] private Image tailCooldown;
    [SerializeField] private Image flyCooldown;

    private void Update()
    {
        UpdateCooldown(
            fireAttack,
            fireCooldown
        );

        UpdateCooldown(
            tailAttack,
            tailCooldown
        );

        UpdateCooldown(
            flyAttack,
            flyCooldown
        );
    }

    private void UpdateCooldown(
        FireAttack attack,
        Image cooldownImage)
    {
        if (attack == null || cooldownImage == null)
            return;

        cooldownImage.fillAmount =
            attack.CooldownRemaining / attack.CooldownDuration;
    }

    private void UpdateCooldown(
        TailAttack attack,
        Image cooldownImage)
    {
        if (attack == null || cooldownImage == null)
            return;

        cooldownImage.fillAmount =
            attack.CooldownRemaining / attack.CooldownDuration;
    }

    private void UpdateCooldown(
        FlyAttack attack,
        Image cooldownImage)
    {
        if (attack == null || cooldownImage == null)
            return;

        cooldownImage.fillAmount =
            attack.CooldownRemaining / attack.CooldownDuration;
    }
}