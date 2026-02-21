using UnityEngine;


public class PlayerStatsTest : MonoBehaviour
{
    public PlayerStats playerStats;


    private void Awake()
    {
        if (playerStats == null)
            playerStats = GetComponent<PlayerStats>();
    }


    private void OnEnable()
    {
        if (playerStats == null) return;

        playerStats.OnHealthChanged += HandleHealthChanged;
        playerStats.OnDeath += HandleDeath;
    }


    private void OnDisable()
    {
        if (playerStats == null) return;

        playerStats.OnHealthChanged -= HandleHealthChanged;
        playerStats.OnDeath -= HandleDeath;
    }


    private void Update()
    {
        if (playerStats == null) return;

        if (Input.GetKeyDown(KeyCode.H))
            playerStats.TakeDamage(10f);

        if (Input.GetKeyDown(KeyCode.J))
            playerStats.Heal(10f);
    }


    private void HandleHealthChanged(float current, float max)
    {
        Debug.Log($"Health changed: {current} / {max}");
    }

    private void HandleDeath()
    {
        Debug.Log("Player died!");
    }
}