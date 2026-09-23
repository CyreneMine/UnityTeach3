using System;
using UnityEngine;

public class PlayerObject : MonoBehaviour
{
    public Transform gunPoint;
    private float _roundSpeed = 50f;
    private Animator _animator;
    private int atk,money;
    private void Start()
    {
        _animator =  GetComponent<Animator>();
    }

    private void Update()
    {
        _animator.SetFloat("VSpeed", Input.GetAxis("Vertical"));
        _animator.SetFloat("HSpeed", Input.GetAxis("Horizontal"));
        transform.Rotate(Vector3.up, Input.GetAxis("Mouse X") * _roundSpeed * Time.deltaTime);
        if (Input.GetKeyDown(KeyCode.R))
            _animator.SetTrigger("Roll");
        if (Input.GetMouseButtonDown(0))
            _animator.SetTrigger("Fire");
        if (Input.GetKeyDown(KeyCode.C))
        {
            _animator.SetLayerWeight(1,1);
        }
        else if (Input.GetKeyUp(KeyCode.C))
        {
            _animator.SetLayerWeight(1,0);
        }
    }
    //初始化属性
    public void InitPlayerInfo(int atk,int money)
    {
        this.atk = atk;
        this.money = money;
        UpdateMoney();
    }
    //更新面板金币显示
    public void UpdateMoney()
    {
        UIManager.Instance.GetPanel<GamePanel>().UpdateMoney(money);
    }
    //留给外部用来更新金币的方法
    public void AddMoney(int count)
    {
        money += count;
        UpdateMoney();
    }

    public void KnifeEvent()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position + transform.forward +transform.up,1,1 << LayerMask.NameToLayer("Monster"));
        //TODO 处理攻击到的怪物 让他们受伤
        foreach (Collider i in colliders)
        {
            
        }
    }

    public void ShootEvent()
    {
        RaycastHit[] raycastHits = Physics.RaycastAll(new Ray(gunPoint.position,gunPoint.forward),1000,1<<LayerMask.NameToLayer("Monster"));
        //TODO 处理攻击到的怪物 让他们受伤
        foreach (RaycastHit hit in raycastHits)
        {
            
        }
    }
}
