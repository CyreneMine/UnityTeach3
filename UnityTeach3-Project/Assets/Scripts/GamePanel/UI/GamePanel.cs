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
    public override void Init()
    {
        nowMoney = GameDataMgr.Instance.playerData.money;
        nowHp = maxHp;
        sliderHp.maxValue = maxHp;
        sliderHp.value = nowHp;
        txtNowHp.text = $"{nowHp}/{maxHp}";
        btnQuit.onClick.AddListener(() =>
        {
            UIManager.Instance.HidePanel<GamePanel>();
            SceneManager.LoadScene("BeginScene");
        });
        choiceTowerArea.gameObject.SetActive(false);
    }
    public void UpdateHpBar(int hp)
    {
        nowHp -= hp;
        sliderHp.value = nowHp;
        txtNowHp.text = $"{nowHp}/{maxHp}";
    }
    public void UpdateWaveNumber(int nowWave, int maxWave)
    {
        txtWave.text = $"{nowWave}/{maxWave}";
    }
    public void UpdateMoney(int count)
    {
        nowMoney += count;
        txtMoney.text = $"{nowMoney}";
    }
}
