using UnityEngine;

public class FireballProjectile : MonoBehaviour
{
    [SerializeField] private float speed = 12f;
    [SerializeField] private float damage = 20f;
    [SerializeField] private float lifetime = 5f;

    private Transform target;

    public void SetDamage(float newDamage)
    {
        damage = newDamage;
    }

    public void Initialize(Transform targetTransform)
    {
        target = targetTransform;

        Destroy(gameObject, lifetime);
    }

   private void Update()
{
    if (target == null)
    {
        Destroy(gameObject);
        return;
    }

    Vector3 targetPosition = target.position;

    // Keep the fireball at its current height.
    targetPosition.y = transform.position.y;

    Vector3 direction = targetPosition - transform.position;

    if (direction.sqrMagnitude < 0.25f)
    {
        DragonHealth health = target.GetComponent<DragonHealth>();

        if (health != null)
        {
            health.TakeDamage(damage);
        }

        Destroy(gameObject);
        return;
    }

    direction.Normalize();

    transform.position += direction * speed * Time.deltaTime;

    transform.rotation = Quaternion.LookRotation(direction);
}

    private void OnTriggerEnter(Collider other)
    {
        DragonHealth health = other.GetComponent<DragonHealth>();

        if (health == null)
            return;

        health.TakeDamage(damage);

        Destroy(gameObject);
    }
}