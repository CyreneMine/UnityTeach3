using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GamePanel : BasePanel
{
    public bool isShow;
    public Button btnQuit;
    public Slider sliderHp;
    public TMP_Text txtNowHp, txtWave, txtMoney;
    public Transform choiceTowerArea;
    public List<TowerBtn>  towerBtns = new List<TowerBtn>();
    public float maxHp,nowHp;
    private int nowMoney;
    private TowerPoint nowSelTowerPoint;
    private bool canCreateTower = false;
    public override void Init()
    {
        
        nowHp = maxHp;
        sliderHp.maxValue = maxHp;
        sliderHp.value = nowHp;
        txtNowHp.text = $"{nowHp}/{maxHp}";
        btnQuit.onClick.AddListener(() =>
        {
            UIManager.Instance.HidePanel<GamePanel>();
            GameLevelMgr.Instance.ClearInfo();
            SceneManager.LoadScene("BeginScene");
        });
        choiceTowerArea.gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.Confined;
    }
    public void UpdateHpBar(int hp,int maxHP)
    {
        nowHp = hp;
        maxHp = maxHP;
        sliderHp.value = nowHp;
        txtNowHp.text = $"{nowHp}/{maxHp}";
    }
    public void UpdateWaveNumber(int nowWave, int maxWave)
    {
        txtWave.text = $"{nowWave}/{maxWave}";
    }
    public void UpdateMoney(int count)
    {
        print($"接受:{count}");
        print($"现在有:{nowMoney}");
        nowMoney = count;
        print($"加完后有:{nowMoney}");
        txtMoney.text = $"{nowMoney}";
    }

    public void UpdateSelTower(TowerPoint point)
    {
        nowSelTowerPoint = point;
        if (nowSelTowerPoint !=null)
        {
            choiceTowerArea.gameObject.SetActive(true);
            canCreateTower = true;
            if (nowSelTowerPoint.nowSelTowerInfo == null)
            {
                int i = 0;
                foreach (TowerBtn towerBtn in towerBtns)
                {
                    towerBtn.gameObject.SetActive(true);
                    towerBtn.InitInfo(nowSelTowerPoint.towerIds[i++],$"数字键{i}");
                }
            }
            else
            {
                int i = 0;
                foreach (TowerBtn towerBtn in towerBtns)
                {
                    towerBtn.gameObject.SetActive(false);
                }
                towerBtns[0].InitInfo(nowSelTowerPoint.nowSelTowerInfo.next,"空格键");
                towerBtns[0].gameObject.SetActive(true);
            }
        }
        else
        {
            choiceTowerArea.gameObject.SetActive(false);
            canCreateTower = false;
        }
    }

    protected override void Update()
    {
        base.Update();
        if (!canCreateTower)
            return;
        if (nowSelTowerPoint.nowSelTowerInfo == null)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                nowSelTowerPoint.CreateTower(nowSelTowerPoint.towerIds[0]);
            }else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                nowSelTowerPoint.CreateTower(nowSelTowerPoint.towerIds[1]);
            }else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                nowSelTowerPoint.CreateTower(nowSelTowerPoint.towerIds[2]);
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                nowSelTowerPoint.CreateTower(nowSelTowerPoint.nowSelTowerInfo.next);
            }
        }
    }
}
