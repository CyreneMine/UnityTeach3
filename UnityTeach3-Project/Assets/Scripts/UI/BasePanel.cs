using System;
using UnityEngine;
using UnityEngine.Events;

public abstract class BasePanel : MonoBehaviour
{
    private CanvasGroup _canvasGroup;
    private float _alphaSpeed = 10f;
    private bool _isShow;
    private UnityAction _hideCallBack;
    protected virtual void Start()
    {
        Init();
    }

    protected virtual void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        if (_canvasGroup == null)
            _canvasGroup = gameObject.AddComponent<CanvasGroup>();
    }

    public abstract void Init();
    void Update()
    {
        if (_isShow && _canvasGroup.alpha < 1)
        {
            _canvasGroup.alpha += _alphaSpeed * Time.deltaTime;
            if (_canvasGroup.alpha >= 1)
                _canvasGroup.alpha = 1;
        }else if (!_isShow && _canvasGroup.alpha > 0)
        {
            _canvasGroup.alpha -= _alphaSpeed * Time.deltaTime;
            if (_canvasGroup.alpha <= 0)
            {
                _canvasGroup.alpha = 0;
                _hideCallBack?.Invoke();
            }
        }
    }

    public virtual void ShowMe()
    {
        _canvasGroup.alpha = 0;
        _isShow = true;
    }
    public virtual void HideMe(UnityAction callBack)
    {
        _canvasGroup.alpha = 1;
        _isShow = false;
        _hideCallBack = callBack;
    }
}
