using System;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Transform target;
    public Vector3 offsetXYZ;
    public float bodyHeight;
    public float moveSpeed,rotationSpeed;
    private Vector3 _targetPos;
    private Quaternion _targetRot;
    private void Update()
    {
        if (target == null)
            return;
        //offsetXYZ的z固定传入负数
        _targetPos = target.position + target.forward * offsetXYZ.z;
        _targetPos += target.right * offsetXYZ.x;
        _targetPos += Vector3.up * offsetXYZ.y;
        _targetRot = Quaternion.LookRotation(target.position +Vector3.up*bodyHeight - transform.position);

        transform.position = Vector3.Lerp(transform.position,_targetPos, moveSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Slerp(transform.rotation, _targetRot, rotationSpeed * Time.deltaTime);
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }
}
