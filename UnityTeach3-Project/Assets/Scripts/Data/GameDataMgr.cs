
public class GameDataMgr
{
    public static GameDataMgr Instance { get; private set; } = new();
    public MusicData musicData;
    
    private GameDataMgr()
    {
        musicData = JsonMgr.Instance.LoadData<MusicData>("MusicData");
    }
    public void SaveMusicData()
    {
        JsonMgr.Instance.SaveData(musicData, "MusicData");
    }
}
