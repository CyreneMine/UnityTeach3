using System;
using UnityEngine;

public class MainTowerObject : MonoBehaviour
{
    private static MainTowerObject instance;
    public static MainTowerObject Instance => instance;
    private int _hp, _maxHp;
    private bool _isDead;
    private void Awake()
    {
        instance = this;
    }

    public void UpdateHp(int hp, int maxHp)
    {
        _hp = hp;
        _maxHp = maxHp;
        UIManager.Instance.GetPanel<GamePanel>().UpdateHpBar(_hp,_maxHp);
    }
    public void Wound(int dmg)
    {
        if(_isDead)
            return;
        _hp -= dmg;
        if (_hp <= 0)
        {
            _hp = 0;
            _isDead = true;
        }
        UpdateHp(_hp,_maxHp);
    }
    private void OnDestroy()
    {
        instance = null;
    }
}
