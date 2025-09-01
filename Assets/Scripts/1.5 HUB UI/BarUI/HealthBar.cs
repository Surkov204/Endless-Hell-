using UnityEngine;
using UnityEngine.UI;
public class HealthBar : MonoBehaviour
{
    [SerializeField] private Health creatureHealth;
    [SerializeField] private Image totalhealthBar;
    [SerializeField] private Image currenthealthBar;

    private void Start()
    {
        totalhealthBar.fillAmount = creatureHealth.StartingHealth / creatureHealth.StartingHealth;
    }
    private void Update()
    {
        currenthealthBar.fillAmount = creatureHealth.CurrentHealth / creatureHealth.StartingHealth;
    }
}
