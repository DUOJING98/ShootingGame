using System.Collections;
using UnityEngine;

public class ScoreManager : persistentSingleton<ScoreManager>
{
    int score;
    int currentScore;

    public void ResetScore()
    {
        score = 0;
        currentScore = 0;
        Score.UpdateText(score);
    }

    public void AddScore(int scorePoint)
    {
        currentScore += scorePoint;
        Score.UpdateText(score);
        StartCoroutine(nameof(AddScoreCoroutine));
    }
    public int GetCurrentScore()
    {
        return currentScore;
    }

    IEnumerator AddScoreCoroutine()
    {
        while(score < currentScore)
        {
            score++;
            Score.UpdateText(score);

            yield return null;
        }
    }
}
