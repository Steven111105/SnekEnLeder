using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioSource masterSource;
    public AudioSource musicSource;
    public AudioSource sfxSource;
    public AudioMixer mixer;
    void Awake()
    {
        if (instance != null && instance != this) { Destroy(gameObject); return; }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void PlayMusic(AudioClip clip, float fade = 0.5f)
    {
        StopAllCoroutines();
        StartCoroutine(FadeInMusic(clip, fade));
    }

    IEnumerator FadeInMusic(AudioClip clip, float fade)
    {
        float startVol = musicSource.volume;
        for (float t = 0; t < fade; t += Time.unscaledDeltaTime)
        {
            musicSource.volume = Mathf.Lerp(startVol, 0f, t / fade);
            yield return null;
        }
        musicSource.volume = 0f;

        musicSource.clip = clip;
        musicSource.loop = true;
        musicSource.Play();

        for (float t = 0; t < fade; t += Time.unscaledDeltaTime)
        {
            musicSource.volume = Mathf.Lerp(0f, 1f, t / fade);
            yield return null;
        }
        musicSource.volume = 1f;
    }

    public void StopMusic(float fade = 0.5f)
    {
        StartCoroutine(FadeOutMusic(fade));
    }

    IEnumerator FadeOutMusic(float fade)
    {
        float startVol = musicSource.volume;
        for (float t = 0; t < fade; t += Time.unscaledDeltaTime)
        {
            musicSource.volume = Mathf.Lerp(startVol, 0f, t / fade);
            yield return null;
        }
        musicSource.Stop();
        musicSource.volume = startVol;
    }

    public void PlaySFX(AudioClip clip, float volume = 1f)
    {
        if (clip == null) return;
        sfxSource.PlayOneShot(clip, volume);
    }

    public void SetMasterVolume(float v01)
    {
        v01 = Mathf.Clamp01(v01);
        if (mixer == null) { masterSource.volume = v01; return; }
        const float MIN_DB = -80f;
        mixer.SetFloat("MasterVol", Mathf.Lerp(MIN_DB, 0f, v01));
    }
    public void SetMusicVolume(float v01)
    {
        v01 = Mathf.Clamp01(v01);
        if (mixer == null) { musicSource.volume = v01; return; }
        const float MIN_DB = -80f;
        mixer.SetFloat("MusicVol", Mathf.Lerp(MIN_DB, 0f, v01));
    }

    public void SetSFXVolume(float v01)
    {
        v01 = Mathf.Clamp01(v01);
        if (mixer == null) { sfxSource.volume = v01; return; }
        const float MIN_DB = -80f;
        mixer.SetFloat("SFXVol", Mathf.Lerp(MIN_DB, 0f, v01));
    }
}
