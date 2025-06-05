using UnityEngine;
using UnityEngine.UI;

public class WaveUI : MonoBehaviour
{
    Text waveUI;

    private void Awake()
    {
        if (TryGetComponent<Canvas>(out Canvas canvas))
        {
            canvas.worldCamera = Camera.main;
        }
        waveUI = GetComponentInChildren<Text>();
    }

    private void OnEnable()
    {

            waveUI.text = "-WAVE " + EnemyManager.instance.WaveNumber + " -";
    }
}
