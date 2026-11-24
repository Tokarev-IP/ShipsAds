using UnityEngine;

public class DamageController : MonoBehaviour
{
    private void Awake()
    {
        gameObject.SetActive(false);
    }

    public void OnAnimationFinished()
    {
        gameObject.SetActive(false); 
    }

    public void PlayExplosion()
    {
        gameObject.SetActive(true);

        Animator animator = gameObject.GetComponent<Animator>();
        if (animator != null)
        {
            animator.Play("Damage Animation", 0, 0f);
        }
    }
}
