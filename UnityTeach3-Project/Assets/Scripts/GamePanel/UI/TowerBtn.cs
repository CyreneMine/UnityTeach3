using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TowerBtn : MonoBehaviour
{
    public TMP_Text txtPrice,txtTips;
    public Button btn;

    public void InitInfo(int id, string inputStr)
    {
        TowerInfo info = GameDataMgr.Instance.towerInfos[id - 1];
        btn.image.sprite = Resources.Load<Sprite>(info.imgRes);
        txtTips.text = inputStr;
        txtPrice.text = $"$:{info.money}";
        if (GameLevelMgr.Instance.player.money < info.money)
        {
            txtPrice.text = $"$:{info.money}(金钱不足)";
        }
    }
}
