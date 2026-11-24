using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FireButtonController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private ShipFire shipFire;     
    [SerializeField] private Image reloadCircle;
    [SerializeField] private HandCrookBoxController handCrookBoxController;

    [Header("Settings")]
    [SerializeField] private float reloadTime = 3f;
    [SerializeField] private float pressedScale = 0.8f;
    [SerializeField] private float downDuration = 0.5f;
    [SerializeField] private float upDuration = 0.5f;
    [SerializeField] private float hideDuration = 0.2f;

    private Coroutine animRoutine;
    private Button shootButton;
    private bool isReloading = false;
    private Vector3 defaultScale;
    private Coroutine reloadRoutine;
    private Coroutine hideRoutine;
    private bool isHiding = false;

    private void Awake()
    {
        shootButton = GetComponent<Button>();
        defaultScale = transform.localScale;
    }

    private void Start()
    {
        if (reloadCircle != null) reloadCircle.fillAmount = 0f;
    }

    public void OnShootButtonPressed()
    {
        if (isReloading) return;

        if (handCrookBoxController != null)
        {
            handCrookBoxController.SwitchOffHandCrook();
        }

        if (shipFire != null)
        {
            shipFire.Fire();
        }

        StartCoroutine(ReloadRoutine());
    }

    private IEnumerator ReloadRoutine()
    {
        isReloading = true;

        if (shootButton != null) shootButton.interactable = false;

        if (reloadCircle != null) reloadCircle.fillAmount = 1f;

        float elapsedTime = 0f;

        while (elapsedTime < reloadTime)
        {
            elapsedTime += Time.deltaTime;

            if (reloadCircle != null)
            {
                reloadCircle.fillAmount = 1f - Mathf.Clamp01(elapsedTime / reloadTime);
            }

            yield return null;
        }

        if (reloadCircle != null) reloadCircle.fillAmount = 0f;

        if (shootButton != null) shootButton.interactable = true;

        isReloading = false;
    }

    public void PlayPressEffectAnimation()
    {
        if (!gameObject.activeInHierarchy)
            return;

        if (animRoutine != null)
            StopCoroutine(animRoutine);

        animRoutine = StartCoroutine(PressRoutineEffectAnimation());
    }

    private IEnumerator PressRoutineEffectAnimation()
    {
        Vector3 targetDown = defaultScale * pressedScale;

        float t = 0f;
        Vector3 startScale = transform.localScale;
        while (t < downDuration)
        {
            t += Time.deltaTime;
            float k = Mathf.Clamp01(t / downDuration);
            transform.localScale = Vector3.Lerp(startScale, targetDown, k);
            yield return null;
        }
        transform.localScale = targetDown;

        t = 0f;
        while (t < upDuration)
        {
            t += Time.deltaTime;
            float k = Mathf.Clamp01(t / upDuration);
            transform.localScale = Vector3.Lerp(targetDown, defaultScale, k);
            yield return null;
        }
        transform.localScale = defaultScale;

        animRoutine = null;
    }

    public void SwitchOffFireButton()
    {
        if (isHiding) return;

        if (shootButton != null)
            shootButton.interactable = false;

        if (reloadRoutine != null)
        {
            StopCoroutine(reloadRoutine);
            reloadRoutine = null;
            isReloading = false;
        }

        if (animRoutine != null)
        {
            StopCoroutine(animRoutine);
            animRoutine = null;
        }

        if (!gameObject.activeInHierarchy)
        {
            gameObject.SetActive(false);
            return;
        }

        isHiding = true;

        if (hideRoutine != null)
            StopCoroutine(hideRoutine);

        hideRoutine = StartCoroutine(HideRoutine());
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
        isHiding = false;
        hideRoutine = null;
        gameObject.SetActive(false);
    }
}
