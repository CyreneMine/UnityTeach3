using UnityEngine;
using UnityEngine.UI;

public class SettingPanel : BasePanel
{
    public Button btnClose;
    public Toggle togMusic, togSound;
    public Slider sliderMusic, sliderSound;
    public override void Init()
    {
        btnClose.onClick.AddListener(() =>
        {
            MusicData musicData = new MusicData();
            musicData.musicMute = !togMusic.isOn;
            musicData.musicVolume = sliderMusic.value;
            musicData.soundMute = !togSound.isOn;
            musicData.soundVolume = sliderSound.value;
            GameDataMgr.Instance.musicData = musicData;
            GameDataMgr.Instance.SaveMusicData();
            UIManager.Instance.HidePanel<SettingPanel>();
        });
        togMusic.onValueChanged.AddListener(arg0 =>
        {
            BKMusic.Instance.ChangeMusicMute(!arg0);
        });
        sliderMusic.onValueChanged.AddListener((value) =>
        {
            BKMusic.Instance.ChangeMusicVolume(value);
        });
        togSound.onValueChanged.AddListener(arg0 =>
        {
            
        });
        sliderSound.onValueChanged.AddListener(value =>
        {
            
        });
    }

    public override void ShowMe()
    {
        base.ShowMe();
        MusicData musicData = GameDataMgr.Instance.musicData;
        togMusic.isOn = !musicData.musicMute;
        togSound.isOn = !musicData.soundMute;
        sliderMusic.value = musicData.musicVolume;
        sliderSound.value = musicData.soundVolume;
    }
}
