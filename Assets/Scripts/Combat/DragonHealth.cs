using UnityEngine;
using System.Collections;

public class DragonHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float hitFlashDuration = 0.15f;
    [SerializeField] private AudioSource hitAudioSource;

    private float currentHealth;

    private Renderer[] renderers;
    private Material[] materials;
    private Color[] originalColors;

    public float CurrentHealth => currentHealth;
    public float MaxHealth => maxHealth;

    private void Awake()
    {
        currentHealth = maxHealth;

        renderers = GetComponentsInChildren<Renderer>();

        materials = new Material[renderers.Length];
        originalColors = new Color[renderers.Length];

        for (int i = 0; i < renderers.Length; i++)
        {
            materials[i] = renderers[i].material;
            originalColors[i] = materials[i].color;
        }
    }

    public void TakeDamage(float damage)
    {
        if (currentHealth <= 0f)
            return;

        currentHealth -= damage;
        currentHealth = Mathf.Max(currentHealth, 0f);
        if(hitAudioSource!=null){
            hitAudioSource.Play();
        }

        Debug.Log(
            $"{gameObject.name} took {damage} damage. " +
            $"Health: {currentHealth}/{maxHealth}"
        );

        StartCoroutine(HitFlash());

        if (currentHealth <= 0f)
            Die();
    }

    private IEnumerator HitFlash()
    {
        // Flash red
        for (int i = 0; i < materials.Length; i++)
        {
            if (materials[i] != null)
                materials[i].color = Color.red;
        }

        yield return new WaitForSeconds(hitFlashDuration);

        // Restore original colors
        for (int i = 0; i < materials.Length; i++)
        {
            if (materials[i] != null)
                materials[i].color = originalColors[i];
        }
    }

    private void Die()
    {
        Debug.Log($"{gameObject.name} has been defeated!");
    }
}