using System;
using UnityEngine;
using UnityEngine.AI;

public class MonsterObject : MonoBehaviour
{
    private Animator _animator;
    private NavMeshAgent _agent;
    private MonsterInfo  _monsterInfo;
    private int _hp;
    private bool _isDead;
    private float frontTime;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _agent = GetComponent<NavMeshAgent>();
    }

    public void InitInfo(MonsterInfo monsterInfo)
    {
        _monsterInfo = monsterInfo;
        _animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>(_monsterInfo.animator);
        _hp = monsterInfo.hp;
        _agent.speed = _agent.acceleration = _monsterInfo.moveSpeed;
        _agent.angularSpeed = _monsterInfo.roundSpeed;
    }
    public void Wound(int dmg)
    {
        if (_isDead)
            return;
        _hp -= dmg;
        if (_hp <= 0)
        {
            Dead();
        }
        else
        {//TODO 后续播放音效
            
        }
        _animator.SetTrigger("Wound");
    }

    public void Dead()
    {
        _isDead = true;
        _agent.isStopped = true;
        _animator.SetBool("Dead",true);
        //TODO 加钱  后续在关卡管理器完善
        
    }
    public void BornOver()
    {
        _agent.SetDestination(MainTowerObject.Instance.transform.position);
    }
    public void DeadEvent()
    {
        //TODO 死亡后倒数然后销毁 后续在关卡管理器完成
        GameLevelMgr.Instance.ChangeMonsterCount(-1);
        GameLevelMgr.Instance.CheckOver();
        Destroy(gameObject);
    }

    public void AtkEvent()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position+transform.forward+transform.up,1,1<<LayerMask.NameToLayer("MainTower"));
        foreach (Collider i in colliders)
        {
            if (i.gameObject == MainTowerObject.Instance.gameObject)
            {
                MainTowerObject.Instance.Wound(_monsterInfo.atk);
            }
        }
    }
    private void Update()
    {
        if (_isDead)
            return;
        _animator.SetBool("Run",_agent.velocity!=Vector3.zero);
        if (!_isDead && (Vector3.Distance(transform.position,MainTowerObject.Instance.transform.position)<=5)&&
            Time.time > frontTime+_monsterInfo.atkOffset)
        {
            frontTime = Time.time;
            _animator.SetTrigger("Atk");
        }
    }
}
