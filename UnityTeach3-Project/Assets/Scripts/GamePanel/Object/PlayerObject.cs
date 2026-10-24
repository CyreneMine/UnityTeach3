using System;
using UnityEngine;

public class PlayerObject : MonoBehaviour
{
    public Transform gunPoint;
    private float _roundSpeed = 50f;
    private Animator _animator;
    public int atk,money;
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
        print($"传入的:{money}");
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
        GameDataMgr.Instance.PlaySound("Music/Knife");
        Collider[] colliders = Physics.OverlapSphere(transform.position + transform.forward+transform.up,1,1 << LayerMask.NameToLayer("Monster"));
        foreach (Collider i in colliders)
        {
            MonsterObject enemy =  i.gameObject.GetComponent<MonsterObject>();
            if (enemy != null && !enemy._isDead)
            {
                enemy.Wound(atk);
                break;
            }
        }
    }
    public void ShootEvent()
    {
        GameDataMgr.Instance.PlaySound("Music/Gun");
        RaycastHit[] raycastHits = Physics.RaycastAll(new Ray(gunPoint.position,gunPoint.forward),1000,1<<LayerMask.NameToLayer("Monster"));
        foreach (RaycastHit hit in raycastHits)
        {
            MonsterObject enemy = hit.transform.gameObject.GetComponent<MonsterObject>();
            if (enemy != null && !enemy._isDead)
            {
                enemy.Wound(atk);
                GameObject effObj = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>(GameDataMgr.Instance.roleInfos[GameDataMgr.Instance.nowSelRoleId].hitEff));
                effObj.transform.position = hit.point;
                break;
            }
        }
    }
}
