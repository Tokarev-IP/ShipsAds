using UnityEngine;

public class ShipAnimatorController : MonoBehaviour
{
    public void StopShipAnimator()
    {
        Animator animator = gameObject.GetComponent<Animator>();
        if (animator != null) animator.enabled = false;
    }
}
