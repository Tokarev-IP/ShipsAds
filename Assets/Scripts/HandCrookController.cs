using UnityEngine;

public class HandCrookController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private FireButtonController fireButtonController;

    public void StartFireButtonScaleAnimationEvent()
    {
        if (fireButtonController != null)
            fireButtonController.PlayPressEffectAnimation();
    }
}
