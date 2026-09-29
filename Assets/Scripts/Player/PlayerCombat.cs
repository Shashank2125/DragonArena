using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private Key fireKey = Key.Q;
    [SerializeField] private Key tailKey = Key.E;
    [SerializeField] private Key flyKey = Key.R;

    private FireAttack fireAttack;
    private TailAttack tailAttack;
    private FlyAttack flyAttack;

    private DragonHealth target;

    private void Awake()
    {
        fireAttack = GetComponent<FireAttack>();
        tailAttack = GetComponent<TailAttack>();
        flyAttack = GetComponent<FlyAttack>();
    }

    private void Update()
    {
        if (Keyboard.current == null)
            return;

        FindTarget();

        // Tail Attack - E
        if (Keyboard.current[tailKey].wasPressedThisFrame)
        {
            if (target != null && tailAttack != null)
            {
                tailAttack.Attack(target);
            }
        }

        // Fire Attack - Q
        if (Keyboard.current[fireKey].wasPressedThisFrame)
        {
            if (target != null && fireAttack != null)
            {
                fireAttack.Attack(target);
            }
        }

        // Fly Attack - R
        if (Keyboard.current[flyKey].wasPressedThisFrame)
        {
            if (target != null && flyAttack != null)
            {
                flyAttack.Attack(target);
            }
        }
    }

    private void FindTarget()
    {
        DragonHealth[] dragons =
            FindObjectsByType<DragonHealth>(
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
}