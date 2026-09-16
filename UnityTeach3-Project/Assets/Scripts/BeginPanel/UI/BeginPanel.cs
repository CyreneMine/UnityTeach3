using UnityEngine;
using UnityEngine.UI;

public class BeginPanel : BasePanel
{
    public Button btnStart,btnSetting,btnAbout,btnQuit;
    public override void Init()
    {
        btnStart.onClick.AddListener((() =>
        {
            
        }));
        btnSetting.onClick.AddListener((() =>
        {
            
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
