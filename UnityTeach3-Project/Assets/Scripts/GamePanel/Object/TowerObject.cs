using System;
using UnityEngine;

public class TowerObject : MonoBehaviour
{
    public Transform gunPoint, head;
    private Transform _target;
    private Vector3 _realTargetPoint;
    private TowerInfo _towerInfo;
    private float _frontAtkTime;
    private Collider[] _colliders;
    void Update()
    {
        if (_towerInfo.type == 1 && CheckEnemy())
        {
            if (_target == null)
                _target = _colliders[0].transform;
            
            foreach (Collider collider in _colliders)
            {
                
                if (Vector3.Distance(collider.GetComponent<MonsterObject>().transform.position,transform.position)
                    <
                    Vector3.Distance(_target.position,transform.position))
                {
                    _target = collider.transform;
                    
                }
            }
            _realTargetPoint = _target.position;
            _realTargetPoint.y = head.position.y;
            head.rotation = Quaternion.Slerp(
                head.rotation,
                Quaternion.LookRotation(_realTargetPoint - transform.position)
                ,20*Time.deltaTime);
            if (Quaternion.Angle(Quaternion.LookRotation(_realTargetPoint - transform.position),head.rotation)<5f)
            {
                Atk(_towerInfo.type);
            }
        }
        else if (_towerInfo.type == 2 && CheckEnemy())
        {
            Atk(_towerInfo.type);
        }
    }
    public void InitInfo(TowerInfo towerInfo)
    {
        _towerInfo = towerInfo;
    }

    public void Atk(int atkType)
    {
        
        // if (_colliders ==null)
        //     return;
            if (atkType == 1)
            {
                if (Time.time > _frontAtkTime + _towerInfo.offsetTime)
                {
                    _frontAtkTime = Time.time;
                    MonsterObject monster = _target.GetComponent<MonsterObject>();
                    monster.Wound(_towerInfo.atk);
                    GameObject effObj = GameObject.Instantiate(Resources.Load<GameObject>(_towerInfo.eff)
                        ,gunPoint.position,Quaternion.AngleAxis(90,gunPoint.right));
                    effObj.transform.SetParent(gunPoint);
                    Destroy(effObj,_towerInfo.offsetTime);
                }
            }
            else
            {
                if (Time.time > _frontAtkTime + _towerInfo.offsetTime)
                {
                    foreach (Collider col in _colliders)
                    {
                        _frontAtkTime = Time.time;
                        MonsterObject monster =  col.GetComponent<MonsterObject>();
                        monster.Wound(_towerInfo.atk);
                        GameObject effObj = GameObject.Instantiate(Resources.Load<GameObject>(_towerInfo.eff)
                            ,gunPoint.position,gunPoint.rotation);
                        Destroy(effObj,_towerInfo.offsetTime);
                    }
                }
            }
    }
    public bool CheckEnemy()
    {
        _colliders = 
            Physics.OverlapSphere(
            transform.position,
            _towerInfo.atkRange,
            1<<LayerMask.NameToLayer("Monster"));
        if (_colliders.Length <= 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}
