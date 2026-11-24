using UnityEngine;
using System.Collections;

public class PirateFireController : MonoBehaviour
{
    private ShipFire shipFire;

    [Header("Shooting Settings")]
    [SerializeField] private float shootInterval = 1.5f;

    private Coroutine shootingCoroutine;
    private bool canFire = true;

    private void Awake()
    {
        shipFire = GetComponent<ShipFire>();
    }

    private void StartShooting()
    {
        if (shootingCoroutine == null && canFire)
        {
            shootingCoroutine = StartCoroutine(ShootRoutine());
        }
    }

    public void StopShooting()
    {
        if (shootingCoroutine != null)
        {
            StopCoroutine(shootingCoroutine);
            shootingCoroutine = null;
        }
        canFire = false;
    }

    private IEnumerator ShootRoutine()
    {
        yield return new WaitForSeconds(shootInterval);
        if (canFire)
        {
            shipFire.Fire();
            shootingCoroutine = null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Cannonball"))
        {
            StartShooting();
        }
    }

}
