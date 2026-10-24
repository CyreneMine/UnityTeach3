using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverPanel : BasePanel
{
    public TMP_Text txtMoney, txtResult, txtInfo;
    public Button btnSure;
    public override void Init()
    {
        btnSure.onClick.AddListener(() =>
        {
            UIManager.Instance.HidePanel<GameOverPanel>();
            UIManager.Instance.HidePanel<GamePanel>();
            GameLevelMgr.Instance.ClearInfo();
            SceneManager.LoadScene("BeginScene");
        });
    }
    public void InitInfo(int money,bool isWin)
    {
        txtResult.text = isWin ? "成功" : "失败";
        txtInfo.text = isWin ? "获得胜利奖励" : "获得失败奖励";
        txtMoney.text = money.ToString();
        GameDataMgr.Instance.playerData.money += money;
        GameDataMgr.Instance.SavePlayerData();
    }

    public override void ShowMe()
    {
        base.ShowMe();
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
    }
}
