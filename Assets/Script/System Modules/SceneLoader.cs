using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.InputSystem;

public class SceneLoader : persistentSingleton<SceneLoader>
{
    const string GAMESCENE = "GameScene";
    const string TITLE = "TitleScene";
    [SerializeField] Image Turnimage;
    [SerializeField] float fadeTime = 3f;
    Color color;
    void Load(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    IEnumerator LoadCoroutine(string sceneName)
    {
        var loading = SceneManager.LoadSceneAsync(sceneName);
        loading.allowSceneActivation = false;
        Turnimage.gameObject.SetActive(true);
        while (color.a < 1f)
        {
            color.a = Mathf.Clamp01(color.a + Time.unscaledDeltaTime / fadeTime);
            Turnimage.color = color;

            yield return null;
        }
        yield return new WaitUntil(()=>loading.progress>=0.9f); 
        loading.allowSceneActivation = true;

        while (color.a > 0f)
        {
            color.a = Mathf.Clamp01(color.a - Time.unscaledDeltaTime / fadeTime);
            Turnimage.color = color;

            yield return null;
        }
        Turnimage.gameObject.SetActive(false);
    }

    public void loadGameScene()
    {
        StartCoroutine(LoadCoroutine(GAMESCENE));
    }

    public void LoadTitleScene()
    {
        StartCoroutine(LoadCoroutine(TITLE));
    }
    private void Update()
    {
        QuitGame();
    }
    void QuitGame()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
        }
    }
}
