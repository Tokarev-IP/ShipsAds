using UnityEngine;
using System.Collections;

public class GameLogoController : MonoBehaviour
{
    [SerializeField] private float duration = 0.5f;
    private Vector3 zeroScale = Vector3.zero;
    [SerializeField] private Vector3 targetScale = Vector3.one;

    private Coroutine appearRoutine;

    public void Show()
    {
        if (!gameObject.activeInHierarchy)
            gameObject.SetActive(true);

        if (appearRoutine != null)
            StopCoroutine(appearRoutine);

        appearRoutine = StartCoroutine(AppearRoutine());
    }

    private IEnumerator AppearRoutine()
    {
        transform.localScale = zeroScale;

        if (duration <= 0f)
        {
            transform.localScale = targetScale;
            appearRoutine = null;
            yield break;
        }

        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.unscaledDeltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            transform.localScale = Vector3.Lerp(zeroScale, targetScale, t);
            yield return null;
        }

        transform.localScale = targetScale;
        appearRoutine = null;
    }

}
