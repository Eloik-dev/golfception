using UnityEngine;

public class MusicController : MonoBehaviour
{
    public GameState gameState;
    [SerializeField] private AudioSource[] musicAudioSources;
    [SerializeField] private AudioSource[] effectAudioSources;


    void Awake()
    {
        ApplyMusicVolume(gameState.GetMusiqueVolume());
        ApplyEffectsVolume(gameState.GetEffectsVolume());

        gameState.OnMusicVolumeChanged.AddListener(ApplyMusicVolume);
        gameState.OnEffectsVolumeChanged.AddListener(ApplyEffectsVolume);
    }

    void ApplyMusicVolume(float volume)
    {
        Debug.Log("EventsTrigger");
        foreach (AudioSource source in musicAudioSources)
        {
            if (source != null)
                source.volume = volume;
        }
    }

    void ApplyEffectsVolume(float volume)
    {
        foreach (AudioSource source in effectAudioSources)
        {
            if (source != null)
                source.volume = volume;
        }
    }

    void OnDestroy()
    {
        if (gameState == null) return;

        gameState.OnMusicVolumeChanged.RemoveListener(ApplyMusicVolume);
        gameState.OnEffectsVolumeChanged.RemoveListener(ApplyEffectsVolume);
    }
}
