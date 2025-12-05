using UnityEngine;
using UnityEngine.UI;

public class TowerUI : MonoBehaviour
{
    [SerializeField] private Tower tower;
    [SerializeField] private GameObject healthbar;
    [SerializeField] private Image fillHealthImage;
    private float maxHealth;


    private void Start()
    {
        healthbar.SetActive(false);
        maxHealth = tower.Health.Max;
        tower.Health.UpdateHealth += UpdateHealthBar;
    }

    private void UpdateHealthBar(int currentValue)
    {
        healthbar.SetActive(true);
        fillHealthImage.fillAmount = currentValue / maxHealth;
    }

    private void OnDestroy()
    {
        tower.Health.UpdateHealth -= UpdateHealthBar;
    }
}
