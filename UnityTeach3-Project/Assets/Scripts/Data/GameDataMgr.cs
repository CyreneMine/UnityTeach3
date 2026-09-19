
using System.Collections.Generic;
using UnityEngine;

public class GameDataMgr
{
    public static GameDataMgr Instance { get; private set; } = new();
    public MusicData musicData;
    public List<RoleInfo> roleInfos;
    public PlayerData playerData;
    public List<SceneInfo> sceneInfos;
    public int nowSelRoleId;
    public int nowSelSceneId;
    private GameDataMgr()
    {
        musicData = JsonMgr.Instance.LoadData<MusicData>("MusicData");
        roleInfos = JsonMgr.Instance.LoadData<List<RoleInfo>>("RoleInfo");
        playerData = JsonMgr.Instance.LoadData<PlayerData>("PlayerData");
        sceneInfos = JsonMgr.Instance.LoadData<List<SceneInfo>>("SceneInfo");
    }
    public void SaveMusicData()
    {
        JsonMgr.Instance.SaveData(musicData, "MusicData");
    }
    public void SaveRoleInfo()
    {
        JsonMgr.Instance.SaveData(roleInfos, "RoleInfo");
    }
    public void SavePlayerData()
    {
        JsonMgr.Instance.SaveData(playerData, "PlayerData");
    }
}
