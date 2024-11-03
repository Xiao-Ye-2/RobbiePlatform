using UnityEngine;

public class AudioManager : MonoBehaviour
{
    static AudioManager instance;
    [Header("Environment Sounds")]
    public AudioClip ambientClip;
    public AudioClip musicClip;
    [Header("FX Sounds")]
    public AudioClip deathFXClip;
    [Header("Player Sounds")]
    public AudioClip[] walkStepClips;
    public AudioClip[] crouchStepClips;
    public AudioClip jumpClip;
    public AudioClip deathClip;
    [Header("Voice Sounds")]
    public AudioClip jumpVoiceClip;
    public AudioClip deathVoiceClip;

    private AudioSource ambientSource;
    private AudioSource musicSource;
    private AudioSource playerSource;
    private AudioSource voiceSource;
    private AudioSource fxSource;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        ambientSource = gameObject.AddComponent<AudioSource>();
        musicSource = gameObject.AddComponent<AudioSource>();
        playerSource = gameObject.AddComponent<AudioSource>();
        voiceSource = gameObject.AddComponent<AudioSource>();
        fxSource = gameObject.AddComponent<AudioSource>();

        PlayConsistentSounds();
    }

    private static void PlayConsistentSounds()
    {
        instance.ambientSource.clip = instance.ambientClip;
        instance.ambientSource.loop = true;
        instance.ambientSource.Play();

        instance.musicSource.clip = instance.musicClip;
        instance.musicSource.loop = true;
        instance.musicSource.Play();
    }

    public static void PlayFootStepAudio()
    {
        int index = Random.Range(0, instance.walkStepClips.Length);
        instance.playerSource.clip = instance.walkStepClips[index];
        instance.playerSource.Play();
    }

    public static void PlayCrouchFootStepAudio()
    {
        int index = Random.Range(0, instance.crouchStepClips.Length);
        instance.playerSource.clip = instance.crouchStepClips[index];
        instance.playerSource.Play();
    }

    public static void PlayJumpAudio()
    {
        instance.playerSource.clip = instance.jumpClip;
        instance.playerSource.Play();

        instance.voiceSource.clip = instance.jumpVoiceClip;
        instance.voiceSource.Play();
    }

    public static void PlayDeathAudio()
    {
        instance.playerSource.clip = instance.deathClip;
        instance.playerSource.Play();

        instance.voiceSource.clip = instance.deathVoiceClip;
        instance.voiceSource.Play();

        instance.fxSource.clip = instance.deathFXClip;
        instance.fxSource.Play();
    }
}
