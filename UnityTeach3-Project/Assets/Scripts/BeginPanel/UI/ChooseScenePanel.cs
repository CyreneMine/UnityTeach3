using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChooseScenePanel : BasePanel
{
    public Button btnStart,btnBack,btnLeft,btnRight;
    public TMP_Text txtName, txtInfo;
    public Image imgScene;
    private int nowSceneId = 0;
    public override void Init()
    {
        UpdateInfo();
        btnLeft.onClick.AddListener(() =>
        {
            --nowSceneId;
            if (nowSceneId < 0)
                nowSceneId = GameDataMgr.Instance.sceneInfos.Count - 1;
            UpdateInfo();
        });
        btnRight.onClick.AddListener(() =>
        {
            ++nowSceneId;
            if (nowSceneId >= GameDataMgr.Instance.sceneInfos.Count)
                nowSceneId = 0;
            UpdateInfo();
        });
        btnStart.onClick.AddListener(() =>
        {
            UIManager.Instance.HidePanel<ChooseScenePanel>();
            AsyncOperation ao = SceneManager.LoadSceneAsync(GameDataMgr.Instance.sceneInfos[nowSceneId].sceneName);
            ao.completed += (obj) =>
            {
                GameLevelMgr.Instance.InitInfo();
            };
        });
        btnBack.onClick.AddListener(() =>
        {
            UIManager.Instance.HidePanel<ChooseScenePanel>();
            UIManager.Instance.ShowPanel<ChoosePanel>();
        });
    }
    public void UpdateInfo()
    {
        SceneInfo sceneInfo = GameDataMgr.Instance.sceneInfos[nowSceneId];
        txtInfo.text = sceneInfo.tips;
        txtName.text = sceneInfo.name;
        imgScene.sprite = Resources.Load<Sprite>(sceneInfo.imgRes);
    }
}
