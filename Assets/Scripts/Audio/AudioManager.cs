using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [System.Serializable]
    class SFX
    {
        public string name;
        public AudioClip clip;
        public float volume = 1f;
    }
    public static AudioManager instance;
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] Slider masterSlider;
    [SerializeField] Slider bgmSlider;
    [SerializeField] Slider sfxSlider;
    [SerializeField] AudioMixerGroup bgmMixerGroup;
    [SerializeField] AudioMixerGroup sfxMixerGroup;
    [SerializeField] List<SFX> bgmList = new List<SFX>();
    [SerializeField] List<SFX> sfxList = new List<SFX>();

    public  bool isTest = false;
    private Transform sfxObject;

    private void Awake()
    {
        if (instance == null)
        {
            Debug.Log("only one AudioManager instance");
            instance = this;
            DontDestroyOnLoad(gameObject);
            sfxObject = transform.GetChild(0);
            SetSlidersListeners();
        }
        else
        {
            Debug.Log("AudioManager instance already exists, destroying object!");
            // there is already an instance of AudioManager
            if(masterSlider == null || bgmSlider == null || sfxSlider == null)
            {
                Destroy(gameObject);
                return;
            }
            // Clear the old listeners
            instance.masterSlider?.onValueChanged.RemoveAllListeners();
            instance.bgmSlider?.onValueChanged.RemoveAllListeners();
            instance.sfxSlider?.onValueChanged.RemoveAllListeners();
            
            instance.masterSlider = masterSlider;
            instance.bgmSlider = bgmSlider;
            instance.sfxSlider = sfxSlider;

            // assign the new sliders to the existing instance


            // set the listeners to the new sliders
            instance.SetSlidersListeners();
            LoadVolumeSettings();
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        LoadVolumeSettings();
    }

    void SetSlidersListeners()
    {
        masterSlider.minValue = 0.1f;
        bgmSlider.minValue = 0.1f;
        sfxSlider.minValue = 0.1f;

        masterSlider.onValueChanged.AddListener(instance.SetMasterVolume);
        bgmSlider.onValueChanged.AddListener(instance.SetBGMVolume);
        sfxSlider.onValueChanged.AddListener(instance.SetSfxVolume);
    }

    void LoadVolumeSettings()
    {
        float masterVolume = PlayerPrefs.GetFloat("MasterVolume", 1f);
        float bgmVolume = PlayerPrefs.GetFloat("BGMVolume", 1f);
        float sfxVolume = PlayerPrefs.GetFloat("SFXVolume", 1f);

        masterSlider.value = masterVolume;
        bgmSlider.value = bgmVolume;
        sfxSlider.value = sfxVolume;
    }

    void SetMasterVolume(float volume)
    {
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20);
        // Debug.Log("Master Volume set to: " + volume);
        PlayerPrefs.SetFloat("MasterVolume", volume);
    }
    void SetBGMVolume(float volume)
    {
        audioMixer.SetFloat("BGMVolume", Mathf.Log10(volume) * 20);
        // Debug.Log("BGM Volume set to: " + volume);
        PlayerPrefs.SetFloat("BGMVolume", volume);
    }

    void SetSfxVolume(float volume)
    {
        audioMixer.SetFloat("SFXVolume", Mathf.Log10(volume) * 20);
        // Debug.Log("SFX Volume set to: " + volume);
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }

    public void PlaySFX(string sfxName)
    {
        SFX sfx = sfxList.Find(s => s.name == sfxName);
        if (sfx != null)
        {
            AudioSource sfxSource = sfxObject.gameObject.AddComponent<AudioSource>();
            sfxSource.clip = sfx.clip;
            sfxSource.volume = sfx.volume;
            sfxSource.outputAudioMixerGroup = sfxMixerGroup;
            sfxSource.Play();
            Destroy(sfxSource, sfx.clip.length);
        }
    }

    public void PlayBGM(string bgmName)
    {
        if(bgmName == null)
        {
            GetComponent<AudioSource>().Stop();
            return;
        }
        SFX bgm = bgmList.Find(b => b.name == bgmName);
        if (bgm != null)
        {
            StartCoroutine(FadeBGMIn(bgm));
        }
    }
    
    public void StopBGM()
    {
        GetComponent<AudioSource>().Stop();
    }

    IEnumerator FadeBGMOut(SFX targetBGM)
    {
        float startVolume = targetBGM.volume;
        float t = 0f;
        float duration = 0.5f;
        while (targetBGM.volume > 0)
        {
            targetBGM.volume = Mathf.Lerp(startVolume, 0f, t / duration);
            t += Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator FadeBGMIn(SFX targetBGM)
    {
        float targetVolume = targetBGM.volume;
        AudioSource bgmSource = gameObject.GetComponent<AudioSource>();
        bgmSource.volume = 1f;
        float t = 0f;
        float duration = 0.5f;

        bgmSource.clip = targetBGM.clip;
        bgmSource.volume = targetBGM.volume;
        bgmSource.outputAudioMixerGroup = bgmMixerGroup;
        bgmSource.loop = true;
        bgmSource.Play();

        while (bgmSource.volume < targetVolume)
        {
            bgmSource.volume = Mathf.Lerp(0f, targetVolume, t / duration);
            t += Time.deltaTime;
            yield return null;
        }
        bgmSource.volume = targetVolume;
    }
}