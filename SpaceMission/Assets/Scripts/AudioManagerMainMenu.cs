using UnityEngine;

public class AudioManagerMainMenu : MonoBehaviour
{
    public static AudioManagerMainMenu Instance { get; private set; }

    [Header("Audio Source")]
    [SerializeField] public AudioSource musicSource;
    [SerializeField] public AudioSource ambientSource;
    [SerializeField] AudioSource SFXSource;

    [Header("Audio Clip")]
    [SerializeField] public AudioClip click;
    [SerializeField] public AudioClip menuMusic;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void PlayMusic(AudioClip clip, float volume = 1f, bool loop = true)
    {
        if (clip == null) return;
        musicSource.clip = clip;
        musicSource.volume = Mathf.Clamp01(volume);
        musicSource.loop = loop;
        musicSource.Play();
    }

    public void PlayAmbietSFX(AudioClip clip, float volume = 1f, bool loop = true)
    {
        if (clip == null) return;
        ambientSource.clip = clip;
        ambientSource.volume = Mathf.Clamp01(volume);
        ambientSource.loop = loop;
        ambientSource.Play();

    }

    public void PlaySFX(AudioClip clip, float volume = 1f)
    {
        if (clip == null) return;
        SFXSource.PlayOneShot(clip, volume);
    }

    public void StopAmbientSFX()
    {
        if (ambientSource.isPlaying)
            ambientSource.Stop();
    }

    public void StopSFX()
    {
        if (SFXSource.isPlaying)
            SFXSource.Stop();
    }

    public void StopMusic()
    {
        if (musicSource.isPlaying)
            musicSource.Stop();
    }
}

