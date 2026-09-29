using UnityEngine;

public class DragonCombat : MonoBehaviour
{
    [Header("Combat")]
    [SerializeField] private float attackRange = 4f;
    [SerializeField] private float attackDamage = 20f;
    [SerializeField] private float attackCooldown = 2f;

    private float lastAttackTime = -Mathf.Infinity;

    private DragonHealth target;

    private void Update()
    {
        FindTarget();
    }

    private void FindTarget()
    {
        DragonHealth[] dragons = FindObjectsByType<DragonHealth>(
            FindObjectsSortMode.None
        );

        float closestDistance = Mathf.Infinity;
        DragonHealth closestDragon = null;

        foreach (DragonHealth dragon in dragons)
        {
            if (dragon == GetComponent<DragonHealth>())
                continue;

            float distance = Vector3.Distance(
                transform.position,
                dragon.transform.position
            );

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestDragon = dragon;
            }
        }

        target = closestDragon;
    }

    public void PerformBasicAttack()
    {
        if (target == null)
            return;

        if (Time.time < lastAttackTime + attackCooldown)
            return;

        float distance = Vector3.Distance(
            transform.position,
            target.transform.position
        );

        if (distance > attackRange)
            return;

        lastAttackTime = Time.time;

        target.TakeDamage(attackDamage);
    }
}