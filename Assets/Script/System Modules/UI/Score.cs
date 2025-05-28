using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    static Text scoreText;

    private void Awake()
    {
        scoreText = GetComponent<Text>();
    }

    private void Start()
    {
        ScoreManager.instance.ResetScore();
    }

    public static void UpdateText(int score)
    {
        scoreText.text = score.ToString();
    }
}
