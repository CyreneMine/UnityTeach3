using System.Collections.Generic;
using UnityEngine;

public class GameLevelMgr
{
    public static GameLevelMgr Instance { get; private set; } = new();
    public PlayerObject player;
    public SceneInfo sceneInfo;
    public List<MonsterPoint> points = new List<MonsterPoint>();
    public int aliveMonsterCount,maxWaveCount,nowWaveCount;
    public void InitInfo()
    {
        sceneInfo = GameDataMgr.Instance.sceneInfos[GameDataMgr.Instance.nowSelSceneId];
        UIManager.Instance.ShowPanel<GamePanel>();
        RoleInfo roleInfo = GameDataMgr.Instance.roleInfos[GameDataMgr.Instance.nowSelRoleId];
        Transform roleBornPos = GameObject.Find("HeroBornPos").transform;
        GameObject roleObj = GameObject.Instantiate(Resources.Load<GameObject>(roleInfo.res),roleBornPos.position,roleBornPos.rotation);
        player = roleObj.gameObject.GetComponent<PlayerObject>();
        player.InitPlayerInfo(roleInfo.atk,sceneInfo.money);
        Camera.main.GetComponent<CameraMove>().SetTarget(roleObj.transform);
        MainTowerObject.Instance.UpdateHp(sceneInfo.towerHp,sceneInfo.towerHp);
    }

    public void InitMonsterPoint(MonsterPoint point)
    {
        points.Add(point);
    }

    public void UpdateMaxCount(int num)
    {
        maxWaveCount += num;
        nowWaveCount = maxWaveCount;
        UIManager.Instance.GetPanel<GamePanel>().UpdateWaveNumber(nowWaveCount,maxWaveCount);
    }

    public void ChangeNowWaveCount(int num)
    {
        nowWaveCount -= num;
        UIManager.Instance.GetPanel<GamePanel>().UpdateWaveNumber(nowWaveCount,maxWaveCount);
    }

    public void ChangeMonsterCount(int num)
    {
        aliveMonsterCount += num;
    }
    public bool CheckOver()
    {
        Debug.Log($"剩下:{aliveMonsterCount}");
        foreach (MonsterPoint point in points)
        {
            if (!point.CheckOver())
                return false;
        }
        if (aliveMonsterCount >0)
            return false;
        Debug.Log("游戏胜利");
        return true;
    }

    public void ClearInfo()
    {
        points.Clear();
        maxWaveCount = nowWaveCount = aliveMonsterCount = 0;
    }
}
