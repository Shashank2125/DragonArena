using UnityEngine;

public class TailAttack : MonoBehaviour
{
    [Header("Tail Attack")]
    [SerializeField] private float attackRange = 3f;
    [SerializeField] private float damage = 30f;
    [SerializeField] private float cooldown = 2f;

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

        lastAttackTime = Time.time;
        if(audioSource!=null){
            audioSource.Play();
        }

        animator.SetTrigger("TailAttack");

        target.TakeDamage(damage);
    }
}