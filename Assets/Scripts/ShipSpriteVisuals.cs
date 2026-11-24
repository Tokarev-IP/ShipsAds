using UnityEngine;

public class ShipSpriteVisuals : MonoBehaviour
{
    [Header("Sprite Renderers")]
    [SerializeField] private SpriteRenderer hullRendered;
    [SerializeField] private SpriteRenderer bigSailRendered;

    [Header("Sprites Stages")]
    [SerializeField] private Sprite[] hullSprites;
    [SerializeField] private Sprite[] bigSailsSprites;

    [Header("Parts of the Ship")]
    [SerializeField] private GameObject stickAndBucketObject;

    public void Initialize(int currentHealth)
    {
        UpdateHullVisuals(currentHealth);
        UpdateBigSailsVisuals(currentHealth);
        TurnOffShipPost(currentHealth);
    }

    private void UpdateHullVisuals(int currentHealth)
    {
        if (hullRendered == null || hullSprites == null || hullSprites.Length == 0) return;

        int index = Mathf.Min(currentHealth, hullSprites.Length - 1);

        if (index < 0) index = 0;

        hullRendered.sprite = hullSprites[index];
    }

    private void UpdateBigSailsVisuals(int currentHealth)
    {
        if (bigSailRendered == null || bigSailsSprites == null || bigSailsSprites.Length == 0) return;

        int index = Mathf.Min(currentHealth, bigSailsSprites.Length - 1);

        if (index < 0) index = 0;

        bigSailRendered.sprite = bigSailsSprites[index];
    }

    private void TurnOffShipPost(int currentHealth)
    {
        if (currentHealth > 0) return;
        if (stickAndBucketObject != null && stickAndBucketObject.activeSelf)
        {
            stickAndBucketObject.SetActive(false);
        }
    }
}
