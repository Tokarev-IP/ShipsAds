using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private AudioSource gameSound;

    [Header("Sound Settings")]
    [SerializeField] private float gameOverSoundVolume = 0.2f;

    [Header("Settings")]
    [SerializeField] private float timeUntilDownloadScreen = 2f;

    private GameOver gameOverScript;
    private DownloadScreenController downloadScreenController;

    private void Awake()
    {
        gameOverScript = gameObject.GetComponent<GameOver>();
        downloadScreenController = gameObject.GetComponent<DownloadScreenController>();
    }

    void Start()
    {
        if (gameSound != null && !gameSound.isPlaying) gameSound.Play();
    }

    private void TurnDownGameSound()
    {
        if (gameSound != null && gameSound.isPlaying)
        {
            gameSound.volume = gameOverSoundVolume;
        }
    }

    public void GameOver()
    {
        TurnDownGameSound();
        gameOverScript.SwitchOffGame();
        StartCoroutine(WaitUntilDownloadScreenRoutine());
    }

    private void ShowDownloadScene()
    {
        if (downloadScreenController != null)
            downloadScreenController.SwitchOnDownloadScreen();
    }

    private IEnumerator WaitUntilDownloadScreenRoutine()
    {
        yield return new WaitForSeconds(timeUntilDownloadScreen);
        gameOverScript.FinishGame();
        ShowDownloadScene();
    }

}
