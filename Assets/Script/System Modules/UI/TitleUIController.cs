using UnityEngine;
using UnityEngine.UI;

public class TitleUIController : MonoBehaviour
{
    [SerializeField] Button startGame;
    [SerializeField] Button quitGame;
    [SerializeField] Canvas mainMenu;


    private void OnEnable()
    {
        startGame.onClick.AddListener(OnStartGameClick);
        quitGame.onClick.AddListener(OnQuitGameClick);
    }
    private void OnDisable()
    {
        startGame.onClick.RemoveAllListeners();
        quitGame.onClick.RemoveAllListeners();
    }
    private void Awake()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    private void Start()
    {
        Time.timeScale = 1.0f;
    }

    void OnStartGameClick()
    {
        mainMenu.enabled = false;
        SceneLoader.instance.loadGameScene();
    }

    void OnQuitGameClick()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
