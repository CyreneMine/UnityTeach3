using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ChoosePanel : BasePanel
{
    public TMP_Text txtName, txtMoney,txtUnLockMoney;
    public Button btnLeft, btnRight, btnStart, btnBack, btnUnLock;
    private Transform heroPos;
    private GameObject nowRoleObj;
    private int nowRoleId = 0;
    public override void Init()
    {
        heroPos = GameObject.Find("HeroPos").transform;
        UpdateRoleInfoToScene();
        List<RoleInfo> roles = GameDataMgr.Instance.roleInfos;
        PlayerData playerData = GameDataMgr.Instance.playerData;
        btnLeft.onClick.AddListener(() =>
        {
            --nowRoleId;
            if (nowRoleId <0)
            {
                nowRoleId = roles.Count - 1;
            }
            UpdateRoleInfoToScene();
        });
        btnRight.onClick.AddListener(() =>
        {
            ++nowRoleId;
            if (nowRoleId >roles.Count - 1)
            {
                nowRoleId = 0;
            }
            UpdateRoleInfoToScene();
        });
        btnStart.onClick.AddListener(() =>
        {
            GameDataMgr.Instance.nowSelRoleId = nowRoleId;
            UIManager.Instance.HidePanel<ChoosePanel>();
            UIManager.Instance.ShowPanel<ChooseScenePanel>();
        });
        btnUnLock.onClick.AddListener(() =>
        {
            if (playerData.money >= roles[nowRoleId].lockMoney)
            {
                playerData.money -= roles[nowRoleId].lockMoney;
                playerData.unLockRoleIds.Add(nowRoleId);
                btnUnLock.gameObject.SetActive(false);
                GameDataMgr.Instance.playerData = playerData;
                GameDataMgr.Instance.SavePlayerData();
                UpdateRoleInfoToScene();
            }
            else
            {   
                UIManager.Instance.ShowPanel<TipPanel>().ChangeInfo("金币不足！");
            }
        });
        btnBack.onClick.AddListener(() =>
        {
            UIManager.Instance.HidePanel<ChoosePanel>();
            Camera.main.GetComponent<CameraAnimator>().TurnRight(() =>
            {
                UIManager.Instance.ShowPanel<BeginPanel>();
            });
        });
    }

    public void UpdateRoleInfoToScene()
    {
        if (nowRoleObj !=null)
        {
            Destroy(nowRoleObj);
            nowRoleObj = null;
        }
        nowRoleObj = GameObject.Instantiate(Resources.Load<GameObject>(GameDataMgr.Instance.roleInfos[nowRoleId].res), heroPos.position,heroPos.rotation);
        RoleInfo nowRole = GameDataMgr.Instance.roleInfos[nowRoleId];
        if (nowRole.lockMoney >= 0 && !GameDataMgr.Instance.playerData.unLockRoleIds.Contains(nowRoleId))
        {
            btnUnLock.gameObject.SetActive(true);
            txtUnLockMoney.text = "$:"+nowRole.lockMoney;
        }
        else
        {
            btnUnLock.gameObject.SetActive(false);
        }

        if (!GameDataMgr.Instance.playerData.unLockRoleIds.Contains(nowRoleId))
        {
            btnStart.gameObject.SetActive(false);
        }
        else
        {
            btnStart.gameObject.SetActive(true);
        }
        txtMoney.text = GameDataMgr.Instance.playerData.money.ToString();
        txtName.text = nowRole.tips;
    }

    public override void HideMe(UnityAction callBack)
    {
        base.HideMe(callBack);
        if (nowRoleObj!=null)
        {
            Destroy(nowRoleObj);
            nowRoleObj = null;
        }
    }
}
