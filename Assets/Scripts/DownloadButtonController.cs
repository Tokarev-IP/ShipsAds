using UnityEngine;
using System.Collections;

public class DownloadButtonController : MonoBehaviour
{
    [SerializeField] private float duration = 0.5f;
    private Vector3 zeroScale = Vector3.zero;
    private Vector3 oneScale = Vector3.one;

    private Vector3 minScale = Vector3.one;
    [SerializeField] private Vector3 maxScale = new Vector3(1.2f, 1.2f, 1f);
    [SerializeField] private float pulseDuration = 0.5f;

    private Coroutine appearThenPulseRoutine;

    public void Show()
    {
        if (!gameObject.activeInHierarchy)
            gameObject.SetActive(true);

        if (appearThenPulseRoutine != null)
            StopCoroutine(appearThenPulseRoutine);

        appearThenPulseRoutine = StartCoroutine(ApperThenPulseRoutine());
    }

    public void Cover()
    {
        if (appearThenPulseRoutine != null)
            appearThenPulseRoutine = null;

        gameObject.SetActive(false);
    }

    private IEnumerator ApperThenPulseRoutine()
    {
        yield return StartCoroutine(AppearRoutine());
        yield return StartCoroutine(PulseRoutine());
    }

    private IEnumerator AppearRoutine()
    {
        transform.localScale = zeroScale;

        if (duration <= 0f)
        {
            transform.localScale = oneScale;
            yield break;
        }

        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.unscaledDeltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            transform.localScale = Vector3.Lerp(zeroScale, oneScale, t);
            yield return null;
        }

        transform.localScale = oneScale;
    }

    private IEnumerator PulseRoutine()
    {
        while (true)
        {
            yield return LerpScale(minScale, maxScale, pulseDuration);
            yield return LerpScale(maxScale, minScale, pulseDuration);
        }
    }

    private IEnumerator LerpScale(Vector3 from, Vector3 to, float duration)
    {
        if (duration <= 0f)
        {
            transform.localScale = to;
            yield break;
        }

        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.unscaledDeltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            transform.localScale = Vector3.Lerp(from, to, t);
            yield return null;
        }

        transform.localScale = to;
    }
}
