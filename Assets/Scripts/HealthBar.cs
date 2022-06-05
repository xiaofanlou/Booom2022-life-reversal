using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public static int currentHealth;
    public static int maxHealth;
    public Text healthText;
    private Image healthBar;


    private void Start()
    {
        healthBar = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.fillAmount = (float) currentHealth / (float) maxHealth;
        healthText.text = currentHealth + "/" + maxHealth;

    }
}
