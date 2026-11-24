using UnityEngine;
using System.Collections;

public class GameOver : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject playerShipObject;
    [SerializeField] private GameObject pirateShipObject;
    [SerializeField] private GameObject playerShipBoxObject;
    [SerializeField] private GameObject pirateShipBoxObject;
    [SerializeField] private FireButtonController fireButtonController;
    [SerializeField] private HandCrookBoxController handCrookBoxController;

    [Header("Sounds")]
    [SerializeField] private AudioSource winningSound;
    [SerializeField] private float winningSoundDelay = 1.0f;

    public void SwitchOffGame()
    {
        SwitchOffFireForPirateShip();
        SwitchOffFireForPlayerShip();
        if (IsPlayerShipSunk()) 
            StopPlayerShip();
        if (IsPirateShipSunk()) 
            StopPirateShip();
    }

    public void FinishGame()
    {
        FadePirateShipBox();
        FadePlayerShipBox();
    }

    public void TurnOnWinningSound()
    {
        StartCoroutine(StartWinningSoundRoutine());
    }

    private IEnumerator StartWinningSoundRoutine()
    {
        yield return new WaitForSeconds(winningSoundDelay);
        StartWinningSound();
    }

    private void StartWinningSound()
    {
        if (winningSound == null)
            return;
        if (winningSound.isPlaying)
            winningSound.Stop();
        if (!winningSound.isPlaying)
            winningSound.Play();
    }

    private void SwitchOffFireForPirateShip()
    {
        if (pirateShipObject != null)
        {
            PirateFireController pirateFireController = pirateShipObject.GetComponent<PirateFireController>();
            if (pirateFireController != null) pirateFireController.StopShooting();

        }
    }

    private void SwitchOffFireForPlayerShip()
    {
        if (fireButtonController != null) fireButtonController.SwitchOffFireButton();
        if (handCrookBoxController != null) handCrookBoxController.SwitchOffHandCrook();
    }

    private void StopPirateShip()
    {
        if (pirateShipBoxObject != null)
        {
            PirateShipMover pirateShipMoverScript = pirateShipBoxObject.GetComponent<PirateShipMover>();
            if (pirateShipMoverScript != null) pirateShipMoverScript.StopPirateShip();
        }
        if (pirateShipObject != null) 
        { 
            ShipAnimatorController shipAnimatorController = pirateShipObject.GetComponent<ShipAnimatorController>();
            if (shipAnimatorController != null) shipAnimatorController.StopShipAnimator();

            PirateFireController pirateFireController = pirateShipObject.GetComponent<PirateFireController>();
            if (pirateFireController != null) pirateFireController.StopShooting();
        }
    }

    private void StopPlayerShip()
    {
        if (playerShipBoxObject != null)
        {
            ShipMover shipMoverScript = playerShipBoxObject.GetComponent<ShipMover>();
            if (shipMoverScript != null) shipMoverScript.StopShip();
        }
        if (playerShipObject != null)
        { 
            ShipAnimatorController shipAnimatorController = playerShipObject.GetComponent<ShipAnimatorController>();
            if (shipAnimatorController != null) shipAnimatorController.StopShipAnimator();
        }
    }

    private bool IsPlayerShipSunk()
    {
        if (playerShipObject == null)
            return false;

        ShipController playerShipController = playerShipObject.GetComponent<ShipController>();
        if (playerShipController == null)
            return false;

        return playerShipController.GetCurrentHealth() <= 0;
    }

    private bool IsPirateShipSunk()
    {
        if (pirateShipObject == null)
            return false;

        ShipController pirateShipController = pirateShipObject.GetComponent<ShipController>();
        if (pirateShipController == null)
            return false;

        return pirateShipController.GetCurrentHealth() <= 0;
    }

    private void FadePlayerShipBox()
    {
        if (playerShipBoxObject == null)
            return;

        FadeOutController fadeOutController = playerShipBoxObject.GetComponent<FadeOutController>();
        if (fadeOutController == null)
            return;

        fadeOutController.FadeOut();
    }

    private void FadePirateShipBox()
    {
        if (pirateShipBoxObject == null)
            return;

        FadeOutController fadeOutController = pirateShipBoxObject.GetComponent<FadeOutController>();
        if (fadeOutController == null)
            return;

        fadeOutController.FadeOut();
    }
}
