using UnityEngine;

public class DownloadScreenController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameLogoController gameLogoController;
    [SerializeField] private DownloadButtonController downloadButtonController;
    
    public void SwitchOnDownloadScreen()
    {
        SwitchOnDownloadButton();
        SwitchOnGameLogo();
    }

    private void SwitchOnGameLogo()
    {
        if (gameLogoController != null)
            gameLogoController.Show();
    }

    private void SwitchOnDownloadButton()
    {
        if (downloadButtonController != null)
            downloadButtonController.Show();
    }
}
