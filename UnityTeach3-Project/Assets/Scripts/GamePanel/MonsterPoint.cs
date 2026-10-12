using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MonsterPoint : MonoBehaviour
{
    private int _waitSpawnCount,_monsterId;
    private float _remainingWave;
    public int maxSpawnCount,totalWave;
    public float startTime,waveWaitTime,spawnWaitTime;
    public List<int> spawnMonsterIdList = new List<int>();
    private void Start()
    {
        _remainingWave = totalWave;
        Invoke("CreateWave",startTime);
        GameLevelMgr.Instance.InitMonsterPoint(this);
        GameLevelMgr.Instance.UpdateMaxCount(totalWave);
    }

    private void CreateWave()
    {
        _monsterId = spawnMonsterIdList[Random.Range(0,spawnMonsterIdList.Count)];
        _waitSpawnCount = maxSpawnCount;
        --_remainingWave;
        GameLevelMgr.Instance.ChangeNowWaveCount(1);
        CreateMonster();
    }
    private void CreateMonster()
    {
        MonsterInfo monsterInfo = GameDataMgr.Instance.monsterInfos[_monsterId-1];
        GameObject obj = Instantiate(Resources.Load<GameObject>(monsterInfo.res),transform.position,Quaternion.identity);
        MonsterObject monsterScript = obj.AddComponent<MonsterObject>();
        monsterScript.InitInfo(monsterInfo);
        --_waitSpawnCount;
        GameLevelMgr.Instance.ChangeMonsterCount(1);
        if (_waitSpawnCount >0)
        {
            Invoke("CreateMonster",spawnWaitTime);
        }
        else if(_waitSpawnCount ==0 && _remainingWave >0)
        {
            Invoke("CreateWave",waveWaitTime);
        }
    }

    public bool CheckOver()
    {
        return _waitSpawnCount == 0 && _remainingWave == 0;
    }
}
