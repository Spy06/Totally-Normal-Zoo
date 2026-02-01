using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField] private string BGMOnStart;
    [SerializeField] private AudioSource BGMSource, SFXSource;
    [SerializeField] private BGM[] BGMSounds;
    [SerializeField] private SFX[] SFXSounds;

    private BGM currentBGM;

    private void OnEnable()
    {
        GameEventManager.Instance.OnFinishChangingScene += ChangeBGMAfterChangeScene;
    }

    private void OnDisable()
    {
        GameEventManager.Instance.OnFinishChangingScene -= ChangeBGMAfterChangeScene;
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(gameObject); Udah di Core Manager
        }
        else Destroy(gameObject);
    }

    private void Start()
    {
        PlayBGM(BGMOnStart);
    }

    private void Update()
    {
        if (currentBGM != null && !BGMSource.isPlaying && !BGMSource.loop) PlayNextBGMClip();
    }

    private void ChangeBGMAfterChangeScene()
    {
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            AudioManager.instance.PlayBGM("BGM Main Menu");
        }
        else if (SceneManager.GetActiveScene().name == "Gameplay Fix")
        {
            AudioManager.instance.PlayBGM("BGM 1");
        }
    }

    #region BGM Method
    public void PlayBGM(string name)
    {
        if (BGMSounds.Length <= 0)
        {
            Debug.LogWarning("BGM is empty");
            return;
        }

        currentBGM = Array.Find(BGMSounds, s => s.name == name);

        if (currentBGM != null)
        {
            BGMSource.loop = false;
            PlayNextBGMClip();
        }
        else Debug.LogWarning($"BGM: {name} Not Found");
    }

    private void PlayNextBGMClip()
    {
        if (currentBGM == null || currentBGM.clip.Length == 0) return;

        AudioClip nextClip = currentBGM.clip.Length == 1
                             ? currentBGM.clip[0]
                             : currentBGM.clip[Random.Range(0, currentBGM.clip.Length)];

        BGMSource.clip = nextClip;
        BGMSource.loop = currentBGM.clip.Length == 1;
        BGMSource.Play();
    }

    public void ToggleMuteBGM() => BGMSource.mute = !BGMSource.mute;
    public void SetBGMVolume(float volume) => BGMSource.volume = volume;
    #endregion

    #region SFX Method
    public void PlaySFX(string name, bool randomPitch = false, float volume = 1)
    {
        if (SFXSounds.Length <= 0)
        {
            Debug.LogWarning("SFX is empty");
            return;
        }

        SFX sound = Array.Find(SFXSounds, s => s.name == name);

        if (sound != null)
        {
            AudioClip clip = sound.clip.Length > 1 ? sound.clip[Random.Range(0, sound.clip.Length)] : sound.clip[0];
            SFXSource.pitch = randomPitch ? Random.Range(0.875f, 1.125f) : 1;
            SFXSource.PlayOneShot(clip, volume);
        }
        else Debug.LogWarning($"SFX: {name} Not Found");
    }

    public void ToggleMuteSFX() => SFXSource.mute = !SFXSource.mute;
    public void SetSFXVolume(float volume) => SFXSource.volume = volume;
    #endregion
}

[Serializable]
public class BGM
{
    public string name;
    public AudioClip[] clip;
}

[Serializable]
public class SFX
{
    public string name;
    public AudioClip[] clip;
}