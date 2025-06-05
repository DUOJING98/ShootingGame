using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameUIController : MonoBehaviour
{
    [SerializeField] PlayerInput playerInput;
    [SerializeField] Canvas uiCanvas;
    [SerializeField] Canvas menuCanvas;

    [SerializeField] Button resumeButton;
    [SerializeField] Button mainMenuButton;
    [SerializeField] Button quitButton;

    private void OnEnable()
    {
        playerInput.onPause += Pause;
        playerInput.onUnPause += UnPause;
        resumeButton.onClick.AddListener(OnResumeButtonClick);
        mainMenuButton.onClick.AddListener(OnMainMenuButtonClick);
        quitButton.onClick.AddListener(OnQuitButtonClick);
    }

    private void OnDisable()
    {
        playerInput.onPause -= Pause;
        playerInput.onUnPause -= UnPause;

        resumeButton.onClick.RemoveAllListeners();
        mainMenuButton.onClick.RemoveAllListeners();
        quitButton.onClick.RemoveAllListeners();
    }

    private void Pause()
    {
        Time.timeScale = 0;
        uiCanvas.enabled = false;
        menuCanvas.enabled = true;
        playerInput.EnablePauseInput();
        playerInput.SwitchToDynamicUpdateMode();
    }

    public void UnPause()
    {
        OnResumeButtonClick();
    }

    void OnResumeButtonClick()
    {
        Time.timeScale = 1;
        uiCanvas.enabled = true;
        menuCanvas.enabled = false;
        playerInput.EnablePlayerInput();
        playerInput.SwitchToFixedUpdateMode();
    }

    void OnMainMenuButtonClick()
    {
        menuCanvas.enabled = false;
        SceneLoader.instance.LoadTitleScene();
    }

    void OnQuitButtonClick()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
