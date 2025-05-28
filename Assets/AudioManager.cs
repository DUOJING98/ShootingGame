using UnityEngine;

public class AudioManager : persistentSingleton<AudioManager>
{
    [SerializeField] AudioSource sfxPlayer;
    const float minPitch = 0.9f;
    const float maxPitch = 1.1f;


    public void PlaySFX(AudioData audioData)
    {
        sfxPlayer.PlayOneShot(audioData.audioClip, audioData.volume);
    }
    public void PlayRandomSFX(AudioData audioData)
    {
        sfxPlayer.pitch = Random.Range(minPitch, maxPitch);
        PlaySFX(audioData);
    }

    public void PlayRandomSFX(AudioData[] audioDatas)
    {
        PlayRandomSFX(audioDatas[Random.Range(0, audioDatas.Length)]);
    }
}

[System.Serializable]
public class AudioData
{
    public AudioClip audioClip;
    public float volume;
}

