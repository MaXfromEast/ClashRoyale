using UnityEngine;
using UnityEngine.UI;

public class UnitUI : MonoBehaviour
{
    [SerializeField] private Unit unit;
    [SerializeField] private GameObject healthbar;
    [SerializeField] private Image fillHealthImage;
    private float maxHealth;


    private void Start()
    {
        healthbar.SetActive(false);
        maxHealth = unit.Health.Max;
        unit.Health.UpdateHealth += UpdateHealthBar;
    }

    private void UpdateHealthBar(int currentValue)
    {
        healthbar.SetActive(true);
        fillHealthImage.fillAmount = currentValue / maxHealth;
    }

    private void OnDestroy()
    {
        unit.Health.UpdateHealth -= UpdateHealthBar;
    }
}
