using UnityEngine;
using System.Collections;

public class FireAttack : MonoBehaviour
{
    [Header("Fire Attack")]
    [SerializeField] private float attackRange = 7f;
    [SerializeField] private float damage = 20f;
    [SerializeField] private float cooldown = 3f;
    [SerializeField] private float fireballDelay = 0.35f;

    [Header("Projectile")]
    [SerializeField] private GameObject fireballPrefab;
    [SerializeField] private Transform mouthPoint;

    [SerializeField] private AudioSource audioSource;

    private Animator animator;
    private float lastAttackTime = -Mathf.Infinity;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    // Used by the ability cooldown UI
    public float CooldownRemaining =>
        Mathf.Max(0f, (lastAttackTime + cooldown) - Time.time);

    public float CooldownDuration => cooldown;

    public bool IsOnCooldown =>
        Time.time < lastAttackTime + cooldown;

    public bool CanAttack(DragonHealth target)
    {
        if (target == null)
            return false;

        if (Time.time < lastAttackTime + cooldown)
            return false;

        float distance = Vector3.Distance(
            transform.position,
            target.transform.position
        );

        return distance <= attackRange;
    }

    public void Attack(DragonHealth target)
    {
        if (!CanAttack(target))
            return;

        if (animator == null)
        {
            Debug.LogError($"{name}: Animator is missing.");
            return;
        }

        if (fireballPrefab == null)
        {
            Debug.LogError($"{name}: Fireball Prefab is missing.");
            return;
        }

        if (mouthPoint == null)
        {
            Debug.LogError($"{name}: Mouth Point is missing.");
            return;
        }

        lastAttackTime = Time.time;
        if(audioSource!=null){
            audioSource.Play();
        }

        // Start attack animation
        animator.SetTrigger("FireAttack");

        // Spawn fireball after a short delay
        StartCoroutine(SpawnFireballAfterDelay(target));
    }

    private IEnumerator SpawnFireballAfterDelay(DragonHealth target)
    {
        yield return new WaitForSeconds(fireballDelay);

        if (target == null)
            yield break;

        SpawnFireball(target);
    }

    private void SpawnFireball(DragonHealth target)
    {
        if (target == null)
        {
            Debug.LogError($"{name}: Fireball target is missing.");
            return;
        }

        GameObject fireball = Instantiate(
            fireballPrefab,
            mouthPoint.position,
            mouthPoint.rotation
        );

        if (fireball == null)
        {
            Debug.LogError($"{name}: Failed to create Fireball.");
            return;
        }

        FireballProjectile projectile =
            fireball.GetComponent<FireballProjectile>();

        if (projectile == null)
        {
            Debug.LogError(
                $"{name}: Fireball prefab does not have FireballProjectile component."
            );

            Destroy(fireball);
            return;
        }

        projectile.SetDamage(damage);
        projectile.Initialize(target.transform);
    }
}