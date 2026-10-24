using System;
using System.Collections.Generic;
using UnityEngine;

public class TowerPoint : MonoBehaviour
{
    private GameObject towerObj;
    public TowerInfo nowSelTowerInfo;
    public List<int> towerIds = new List<int>();

    public void CreateTower(int id)
    {
        TowerInfo info = GameDataMgr.Instance.towerInfos[id - 1];
        if (GameLevelMgr.Instance.player.money <info.money)
        {
            return;
        }
        GameLevelMgr.Instance.player.AddMoney(-info.money);
        if (towerObj !=null)
        {
            Destroy(towerObj);
            towerObj = null;
        }

        towerObj = Instantiate(Resources.Load<GameObject>(info.res),transform.position,Quaternion.identity);
        towerObj.GetComponent<TowerObject>().InitInfo(info);
        nowSelTowerInfo = info;
        if (info.next == 0)
        {
            UIManager.Instance.GetPanel<GamePanel>().UpdateSelTower(null);
        }
        else
        {
            UIManager.Instance.GetPanel<GamePanel>().UpdateSelTower(this);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer !=LayerMask.NameToLayer("Player"))
            return;
        if (nowSelTowerInfo != null && nowSelTowerInfo.next == 0)
            return;
        UIManager.Instance.GetPanel<GamePanel>().UpdateSelTower(this);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer !=LayerMask.NameToLayer("Player"))
            return;
        UIManager.Instance.GetPanel<GamePanel>().UpdateSelTower(null);
    }
}
