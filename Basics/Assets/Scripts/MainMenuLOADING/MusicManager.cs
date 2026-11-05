using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    [Header("Audio Sources")]
    public AudioSource musicSource;   // musica di sottofondo (loop)
    public AudioSource sfxSource;     // effetti vocali / suoni overlay

    [Header("Music Clips")]
    public AudioClip menuMusic;
    public AudioClip IntroMusic;
    public AudioClip level1Music;
    public AudioClip level2Music;
    public AudioClip level3Music;

    [Header("Victory Sound")]
    public AudioClip victorySoundClip;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        switch (scene.name)
        {
            case "MainMenu":
                PlayMusic(menuMusic);
                break;
            case "Intro":
                PlayMusic(IntroMusic);
                break;
            case "Level1":
                PlayMusic(level1Music);
                break;
            case "Level2":
                PlayMusic(level2Music);
                break;
            case "Level3":
                PlayMusic(level3Music);
                break;
            default:
                musicSource.Stop();
                break;
        }
    }

    public void PlayMusic(AudioClip clip)
    {
        if (clip == null) return;

        if (musicSource.clip == clip && musicSource.isPlaying)
            return;

        musicSource.clip = clip;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void PlayVoiceClip(AudioClip clip)
    {
        if (clip != null)
        {
            sfxSource.PlayOneShot(clip);
        }
    }

    public void PlayVictorySound()
    {
        if (victorySoundClip != null)
        {
            sfxSource.PlayOneShot(victorySoundClip);
        }
    }

    public void SetMusicVolume(float volume)
    {
        musicSource.volume = Mathf.Clamp01(volume);
    }

    public void SetSFXVolume(float volume)
    {
        sfxSource.volume = Mathf.Clamp01(volume);
    }

    // --- GETTER PER I VOLUME ---

    public float GetMusicVolume()
    {
        return musicSource != null ? musicSource.volume : 1f;
    }

    public float GetSFXVolume()
    {
        return sfxSource != null ? sfxSource.volume : 1f;
    }
}
