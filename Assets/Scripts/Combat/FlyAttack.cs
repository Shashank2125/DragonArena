using UnityEngine;
using System.Collections;

public class FlyAttack : MonoBehaviour
{
    [Header("Fly Attack")]
    [SerializeField] private float attackRange = 8f;
    [SerializeField] private float damage = 40f;
    [SerializeField] private float cooldown = 6f;

    [Header("Flight")]
    [SerializeField] private float flightHeight = 3f;
    [SerializeField] private float flightDuration = 1.5f;

    [Header("Projectile")]
    [SerializeField] private GameObject fireballPrefab;
    [SerializeField] private Transform mouthPoint;

    [SerializeField] private AudioSource audioSource;

    private Animator animator;
    private float lastAttackTime = -Mathf.Infinity;
    private bool isFlying;

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

    public bool IsFlying => isFlying;

    public bool CanAttack(DragonHealth target)
    {
        if (target == null || isFlying)
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

        lastAttackTime = Time.time;
        if (audioSource != null){
            audioSource.Play();
        }

        StartCoroutine(PerformFlyAttack(target));
    }

    private IEnumerator PerformFlyAttack(DragonHealth target)
    {
        isFlying = true;

        animator.SetTrigger("FlyAttack");

        Vector3 startPosition = transform.position;
        Vector3 flyingPosition =
            startPosition + Vector3.up * flightHeight;

        // 1. Take off
        float elapsed = 0f;

        while (elapsed < 0.5f)
        {
            elapsed += Time.deltaTime;

            transform.position = Vector3.Lerp(
                startPosition,
                flyingPosition,
                elapsed / 0.5f
            );

            yield return null;
        }

        // 2. Wait half a second
        yield return new WaitForSeconds(0.5f);

        // 3. Fireball
        if (target != null)
        {
            SpawnFireball(target);
        }

        // 4. Stay in the air for 0.5 seconds
        yield return new WaitForSeconds(0.5f);

        // 5. Land
        elapsed = 0f;

        Vector3 groundPosition = new Vector3(
            transform.position.x,
            startPosition.y,
            transform.position.z
        );

        while (elapsed < 0.5f)
        {
            elapsed += Time.deltaTime;

            transform.position = Vector3.Lerp(
                flyingPosition,
                groundPosition,
                elapsed / 0.5f
            );

            yield return null;
        }

        transform.position = groundPosition;

        isFlying = false;
    }

    private void SpawnFireball(DragonHealth target)
    {
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

        GameObject fireball = Instantiate(
            fireballPrefab,
            mouthPoint.position,
            mouthPoint.rotation
        );

        FireballProjectile projectile =
            fireball.GetComponent<FireballProjectile>();

        if (projectile == null)
        {
            Debug.LogError(
                $"{name}: Fireball prefab does not have FireballProjectile."
            );

            Destroy(fireball);
            return;
        }

        projectile.SetDamage(damage);
        projectile.Initialize(target.transform);
    }
}