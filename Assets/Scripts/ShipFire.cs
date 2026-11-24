using UnityEngine;

public class ShipFire : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject cannonballPrefab;
    [SerializeField] private float shootForce = 15f;

    [Header("Combat")]
    [SerializeField] private bool isFacingRight = true;

    [Header("Effects")]
    [SerializeField] private AudioSource cannonFireSound;
    [SerializeField] private ParticleSystem cannonFireParticles;

    public void Fire()
    {
        if (cannonballPrefab == null || firePoint == null) return;

        StartFireSound();
        StartFireParticles();

        GameObject ball = Instantiate(cannonballPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = ball.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            float directionMultiplier = isFacingRight ? 1f : -1f;

            Vector2 shootDirection = firePoint.right * directionMultiplier;

            rb.linearVelocity = shootDirection * shootForce;
        }
    }

    private void StartFireSound()
    {
        if (cannonFireSound != null && cannonFireSound.isPlaying)
        {
            cannonFireSound.Stop();
        }
        if (cannonFireSound != null && !cannonFireSound.isPlaying)
        {
            cannonFireSound.Play();
        }
    }

    private void StartFireParticles()
    {
        if (cannonFireParticles != null && cannonFireParticles.isPlaying)
        {
            cannonFireParticles.Stop();
        }
        if (cannonFireParticles != null && !cannonFireParticles.isPlaying)
        {
            cannonFireParticles.Play();
        }
    }
}
