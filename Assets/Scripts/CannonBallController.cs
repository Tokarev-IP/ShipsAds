using UnityEngine;

public class CannonBallController : MonoBehaviour
{
    [SerializeField] private AudioSource traceSound;

    private float minX = -5;
    private float maxX = 5;

    private void Start()
    {
        StartTraceSound();
    }

    private void Update()
    {
        float positionX = transform.position.x;
        if (positionX > maxX || positionX < minX)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Cannonball"))
        {
            Destroy(collider.gameObject);
            Destroy(gameObject);
        }
    }

    private void StartTraceSound()
    {
        if (traceSound != null && traceSound.isPlaying)
        {
            traceSound.Stop();
        }
        if (traceSound != null && !traceSound.isPlaying)
        {
            traceSound.Play();
        }
    }
}
