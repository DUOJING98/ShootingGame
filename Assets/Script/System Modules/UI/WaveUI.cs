using UnityEngine;
using UnityEngine.UI;

public class WaveUI : MonoBehaviour
{
    Text waveUI;

    private void Awake()
    {
        GetComponent<Canvas>().worldCamera = Camera.main;
        waveUI = GetComponentInChildren<Text>();
    }

    private void OnEnable()
    {

            waveUI.text = "-WAVE " + EnemyManager.instance.WaveNumber + " -";
    }
}
