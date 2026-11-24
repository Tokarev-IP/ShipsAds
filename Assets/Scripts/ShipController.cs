using UnityEngine;

public class ShipController : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField] private int maxHealth = 3;
    private int currentHealth;

    [Header("Damage Settings")]
    [SerializeField] private int cannonBallDamage = 1;
    [SerializeField] private int shipCollisionDamage = 3;

    [Header("References")]
    [SerializeField] private GameManager gameManager;

    private ShipSpriteVisuals shipSpriteVisuals;
    private ShipDamageEffects shipDamageEffects;

    private void Awake()
    {
        shipSpriteVisuals = GetComponent<ShipSpriteVisuals>();
        shipDamageEffects = GetComponent<ShipDamageEffects>();
    }

    private void Start()
    {
        currentHealth = maxHealth;
        ActivateNewSprites(currentHealth);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Cannonball"))
        {
            Destroy(collider.gameObject);
            TakeDamage(cannonBallDamage);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ship"))
        {
            TakeDamage(shipCollisionDamage);
        }
    }

    private void TakeDamage(int damage)
    {
        if (currentHealth <= 0) return;
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            ShowDamageEffects(currentHealth);
            ActivateNewSprites(currentHealth);
            Sink();
        } else
        {
            ShowDamageEffects(currentHealth);
            ActivateNewSprites(currentHealth);
        }
    }

    private void ActivateNewSprites(int health)
    {
        shipSpriteVisuals.Initialize(health);
    }

    private void ShowDamageEffects(int currentHealth)
    {
        bool isShipSunk = false;
        if (currentHealth <= 0) 
            isShipSunk = true;
        shipDamageEffects.ShowDamage(isShipSunk);
    }

    private void Sink()
    {
        gameManager.GameOver();
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }
}
