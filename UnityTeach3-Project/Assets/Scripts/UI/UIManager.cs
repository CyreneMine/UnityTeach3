using System.Collections.Generic;
using UnityEngine;

public class UIManager
{
    public static UIManager Instance { get; } = new();
    private Dictionary<string, BasePanel> _panels = new Dictionary<string, BasePanel>();
    private Transform _canvas;
    private UIManager()
    {
        GameObject canvasObj = GameObject.Instantiate(Resources.Load<GameObject>("UI/Canvas"));
        _canvas = canvasObj.transform;
        GameObject.DontDestroyOnLoad(canvasObj);
    }
    public T ShowPanel<T>() where T:BasePanel
    {
        string panelName = typeof(T).Name;
        if (_panels.ContainsKey(panelName))
        {
            return _panels[panelName] as T;
        }
        GameObject panelObj = GameObject.Instantiate(Resources.Load<GameObject>("UI/" +panelName));
        Debug.Log(panelObj);
        panelObj.transform.SetParent(_canvas,false);
        T panelScript = panelObj.GetComponent<T>();
        panelScript.ShowMe();
        _panels.Add(panelName, panelScript);
        return panelScript;
    }

    //隐藏 直接把对象Destroy然后从字典移出 可以选择是否需要淡入淡出
    public void HidePanel<T>(bool isFade = true) where T : BasePanel
    {
        string panelName = typeof(T).Name;
        if (_panels.ContainsKey(panelName))
        {
            if (isFade)
            {
                _panels[panelName].HideMe((() =>
                {
                    GameObject.Destroy(_panels[panelName].gameObject);
                    _panels.Remove(panelName);
                }));
            }
            else
            {
                GameObject.Destroy(_panels[panelName].gameObject);
                _panels.Remove(panelName);
            }
        }
    }

    public T GetPanel<T>() where T : BasePanel
    {
        string panelName = typeof(T).Name;
        if (_panels.ContainsKey(panelName))
        {
            return _panels[panelName] as T;
        }
        return null;
    }
}
