using UnityEngine;

public class BKMusic : MonoBehaviour
{
    public static BKMusic Instance { get; private set; }
    private AudioSource _audioSource;
    void Awake()
    {
        Instance = this;
        _audioSource = GetComponent<AudioSource>();
        /*if (_audioSource ==null)
            _audioSource = gameObject.AddComponent<AudioSource>();*/
        MusicData musicData = GameDataMgr.Instance.musicData;
        ChangeMusicMute(musicData.musicMute);
        ChangeMusicVolume(musicData.musicVolume);
    }
    
    public void ChangeMusicVolume(float volume)
    {
        _audioSource.volume = volume;
    }
    public void ChangeMusicMute(bool mute)
    {
        _audioSource.mute = mute;
    }
}
