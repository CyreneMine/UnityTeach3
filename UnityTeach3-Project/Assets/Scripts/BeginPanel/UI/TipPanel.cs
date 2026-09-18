using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TipPanel : BasePanel
{
    public Button btnSure;
    public TMP_Text txtInfo;
    public override void Init()
    {
        btnSure.onClick.AddListener(() =>
        {
            UIManager.Instance.HidePanel<TipPanel>();
        });
    }

    public void ChangeInfo(string info)
    {
        txtInfo.text = info;
    }
}
