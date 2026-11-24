using UnityEngine;

public class ShipDamageEffects : MonoBehaviour
{
    [Header("Damage References")]
    [SerializeField] private AudioSource damageSound;
    [SerializeField] private GameObject damageObject;

    private DamageController damageController;

    private void Awake()
    {
        damageController = damageObject.GetComponent<DamageController>();
    }

    public void ShowDamage(bool isShipSunk)
    {
        StartDamageSound();
        StartDamageVisualEffect();
    }

    private void StartDamageVisualEffect()
    {
        if (damageObject != null)
        {
            damageController.PlayExplosion();
        }
    }

    private void StartDamageSound()
    {
        if (damageSound != null && damageSound.isPlaying)
        {
            damageSound.Stop();
        }
        if (damageSound != null && !damageSound.isPlaying)
        {
            damageSound.Play();
        }
    }
}
