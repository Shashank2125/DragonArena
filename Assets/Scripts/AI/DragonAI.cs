using UnityEngine;

public class DragonAI : MonoBehaviour
{
    private enum AIState
    {
        Chase,
        Attack,
        Reposition
    }

    private Animator animator;
    private FireAttack fireAttack;
    private TailAttack tailAttack;
    private FlyAttack flyAttack;
    private DragonHealth playerHealth;

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 3.5f;
    [SerializeField] private float rotationSpeed = 8f;

    [Header("Combat Ranges")]
    [SerializeField] private float fireAttackRange = 7f;
    [SerializeField] private float tailAttackRange = 3f;
    [SerializeField] private float minimumDistance = 2f;

    [Header("Fly Attack")]
    [SerializeField] private float flyAttackRange = 8f;

    [Header("AI Attack Timing")]
    [SerializeField] private float attackDecisionInterval = 1.5f;

    private Rigidbody rb;
    private Transform player;
    private AIState currentState;

    private float nextAttackTime;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();

        fireAttack = GetComponent<FireAttack>();
        tailAttack = GetComponent<TailAttack>();
        flyAttack = GetComponent<FlyAttack>();
    }

    private void Start()
    {
        GameObject playerObject = GameObject.Find("Player");

        if (playerObject != null)
        {
            player = playerObject.transform;
            playerHealth = playerObject.GetComponent<DragonHealth>();
        }
    }

    private void Update()
    {
        if (player == null || playerHealth == null)
            return;

        // Don't make new attack decisions while flying
        if (flyAttack != null && flyAttack.IsFlying)
            return;

        if (currentState != AIState.Attack)
            return;

        // Prevent the AI from making attack decisions too frequently
        if (Time.time < nextAttackTime)
            return;

        float distance = Vector3.Distance(
            transform.position,
            player.position
        );

        // Fly Attack
        if (distance <= flyAttackRange)
        {
            if (flyAttack != null &&
                flyAttack.CanAttack(playerHealth))
            {
                flyAttack.Attack(playerHealth);
                nextAttackTime = Time.time + attackDecisionInterval;
                return;
            }
        }

        // Close range → Tail Attack
        if (distance <= tailAttackRange)
        {
            if (tailAttack != null &&
                tailAttack.CanAttack(playerHealth))
            {
                tailAttack.Attack(playerHealth);
                nextAttackTime = Time.time + attackDecisionInterval;
            }

            return;
        }

        // Medium range → Fire Attack
        if (fireAttack != null &&
            fireAttack.CanAttack(playerHealth))
        {
            fireAttack.Attack(playerHealth);
            nextAttackTime = Time.time + attackDecisionInterval;
        }
    }

    private void FixedUpdate()
    {
        if (player == null)
            return;

        // IMPORTANT:
        // FlyAttack controls the dragon's position while flying.
        // Stop normal AI movement from fighting against it.
        if (flyAttack != null && flyAttack.IsFlying)
        {
            animator.SetFloat("Speed", 0f);
            return;
        }

        DecideState();

        switch (currentState)
        {
            case AIState.Chase:
                ChasePlayer();
                animator.SetFloat("Speed", 1f);
                break;

            case AIState.Attack:
                FacePlayer();
                animator.SetFloat("Speed", 0f);
                break;

            case AIState.Reposition:
                MoveAwayFromPlayer();
                animator.SetFloat("Speed", 1f);
                break;
        }
    }

    private void DecideState()
    {
        float distance = Vector3.Distance(
            rb.position,
            player.position
        );

        // Outside Fly Attack range → Chase
        if (distance > flyAttackRange)
        {
            currentState = AIState.Chase;
        }
        // Too close → Reposition
        else if (distance < minimumDistance)
        {
            currentState = AIState.Reposition;
        }
        // Inside attack range → Attack
        else
        {
            currentState = AIState.Attack;
        }
    }

    private void ChasePlayer()
    {
        Vector3 direction = player.position - rb.position;
        direction.y = 0f;

        if (direction.sqrMagnitude < 0.01f)
            return;

        direction.Normalize();

        Move(direction);
    }

    private void MoveAwayFromPlayer()
    {
        Vector3 direction = rb.position - player.position;
        direction.y = 0f;

        if (direction.sqrMagnitude < 0.01f)
            return;

        direction.Normalize();

        Move(direction);
    }

    private void FacePlayer()
    {
        Vector3 direction = player.position - rb.position;
        direction.y = 0f;

        if (direction.sqrMagnitude < 0.01f)
            return;

        Quaternion targetRotation =
            Quaternion.LookRotation(direction);

        Quaternion smoothRotation =
            Quaternion.Slerp(
                rb.rotation,
                targetRotation,
                rotationSpeed * Time.fixedDeltaTime
            );

        rb.MoveRotation(smoothRotation);
    }

    private void Move(Vector3 direction)
    {
        Vector3 targetPosition =
            rb.position +
            direction * moveSpeed * Time.fixedDeltaTime;

        rb.MovePosition(targetPosition);

        Quaternion targetRotation =
            Quaternion.LookRotation(direction);

        Quaternion smoothRotation =
            Quaternion.Slerp(
                rb.rotation,
                targetRotation,
                rotationSpeed * Time.fixedDeltaTime
            );

        rb.MoveRotation(smoothRotation);
    }
}