using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    public static GameOverManager Instance { get; private set; }
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] CanvasGroup gameOverGroup;
    [SerializeField] Canvas UI;
    [SerializeField] Text gameOverText;
    [SerializeField] Text scoreText;
    bool isGameOver = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void ShowGameOver()
    {
        isGameOver = true;
        Time.timeScale = 0f;
        UI.gameObject.SetActive(false);
        if (gameOverPanel != null) gameOverPanel.SetActive(true);
        if (gameOverText != null) gameOverText.text = "GameOver";
        if (scoreText != null)
        {
            scoreText.text = "Score: " + ScoreManager.instance.GetCurrentScore();
        }
        StartCoroutine(FadeInGameOver());
        StartCoroutine(WaitForAnyKeyCoroutine());
    }
    IEnumerator FadeInGameOver()
    {
        gameOverGroup.alpha = 0;
        float duration = 1f; 
        float time = 0f;

        while (time < duration)
        {
            time += Time.unscaledDeltaTime;
            gameOverGroup.alpha = Mathf.Lerp(0, 1, time / duration);
            yield return null;
        }
        gameOverGroup.alpha = 1;
        StartCoroutine(BlinkText(gameOverText));
    }
    IEnumerator BlinkText(Text text)
    {
        float duration = 1f; 
        Color baseColor = text.color;

        while (true)
        {
            
            for (float t = 0; t < duration; t += Time.unscaledDeltaTime)
            {
                float alpha = Mathf.Lerp(1f, 0f, t / duration);
                text.color = new Color(baseColor.r, baseColor.g, baseColor.b, alpha);
                yield return null;
            }

            
            for (float t = 0; t < duration; t += Time.unscaledDeltaTime)
            {
                float alpha = Mathf.Lerp(0f, 1f, t / duration);
                text.color = new Color(baseColor.r, baseColor.g, baseColor.b, alpha);
                yield return null;
            }
        }
    }
    IEnumerator WaitForAnyKeyCoroutine()
    {
        
        yield return null;

       
        yield return new WaitUntil(() => Input.anyKeyDown);

        Time.timeScale = 1f;
        SceneLoader.instance.LoadTitleScene();
    }
}
