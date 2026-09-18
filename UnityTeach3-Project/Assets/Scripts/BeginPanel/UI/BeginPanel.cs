using UnityEngine;
using UnityEngine.UI;

public class BeginPanel : BasePanel
{
    public Button btnStart,btnSetting,btnAbout,btnQuit;
    public override void Init()
    {
        btnStart.onClick.AddListener((() =>
        {
            Camera.main.GetComponent<CameraAnimator>().TurnLeft(() =>
            {
                UIManager.Instance.ShowPanel<ChoosePanel>();
            });
            UIManager.Instance.HidePanel<BeginPanel>();
        }));
        btnSetting.onClick.AddListener((() =>
        {
            UIManager.Instance.ShowPanel<SettingPanel>();
        }));
        btnAbout.onClick.AddListener((() =>
        {
            
        }));
        btnQuit.onClick.AddListener((() =>
        {
            Application.Quit();
        }));
        
    }
}
