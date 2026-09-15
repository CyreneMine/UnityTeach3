using System;
using UnityEngine;

public class Lesson59 : MonoBehaviour
{
    public Transform target;
    private Animator animator;
    private CharacterController cc;
    private float changeY;
    private float changeX;
    void Start()
    {
        animator = GetComponent<Animator>();
        cc = GetComponent<CharacterController>();
    }
    void Update()
    {
        changeX += Input.GetAxis("Mouse X");
        changeX = Mathf.Clamp(changeX, -30, 30);
        changeY += Input.GetAxis("Mouse Y");
        changeY = Mathf.Clamp(changeY, -30, 30);
        animator.SetFloat("Horizontal", Input.GetAxis("Horizontal"));
        animator.SetFloat("Vertical", Input.GetAxis("Vertical"));
        cc.SimpleMove((transform.forward * Input.GetAxis("Vertical") * 10));
        transform.Rotate(Vector3.up,Input.GetAxis("Horizontal") *Time.deltaTime* 100);
    }

    private void OnAnimatorIK(int layerIndex)
    {
        animator.SetLookAtWeight(1,1,1);
        Vector3 pos = Quaternion.AngleAxis(changeX, transform.up)*(target.forward*10);
        pos = Quaternion.AngleAxis(-changeY, transform.right) * pos;
        animator.SetLookAtPosition(pos+target.position);
    }
}
