using UnityEngine;
using System.Collections;

public class HandCrookBoxController : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float hideDuration = 0.2f;

    public void SwitchOffHandCrook()
    {
        if (gameObject.activeSelf)
        {
            StartCoroutine(HideRoutine());
        }
    }

    private IEnumerator HideRoutine()
    {
        Vector3 startScale = transform.localScale;
        float elapsed = 0f;

        if (hideDuration > 0f)
        {
            while (elapsed < hideDuration)
            {
                elapsed += Time.unscaledDeltaTime;
                float t = Mathf.Clamp01(elapsed / hideDuration);
                transform.localScale = Vector3.Lerp(startScale, Vector3.zero, t);
                yield return null;
            }
        }

        transform.localScale = Vector3.zero;
        gameObject.SetActive(false);
    }
}
