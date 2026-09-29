using UnityEngine;
using UnityEngine.UI;

public class HealthBarUi : MonoBehaviour
{
    [SerializeField] private DragonHealth target;
    [SerializeField] private Slider healthSlider;

    private void Update()
    {
        if (target == null || healthSlider == null)
            return;

        healthSlider.value =
            target.CurrentHealth / target.MaxHealth;
    }
}