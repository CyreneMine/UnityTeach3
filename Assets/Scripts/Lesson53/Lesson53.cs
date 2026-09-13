using Unity.VisualScripting;
using UnityEngine;

public class Lesson53 : MonoBehaviour
{
    private Animator animator;
    public Transform target;
    private float changeX;
    private float changeY;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        animator.SetFloat("Horizontal", Input.GetAxis("Horizontal"));
        animator.SetFloat("Vertical", Input.GetAxis("Vertical"));
        changeX += Input.GetAxis("Mouse X");
        changeX = Mathf.Clamp(changeX, -30, 30);
        changeY -= Input.GetAxis("Mouse Y");
        changeY = Mathf.Clamp(changeY, -30, 30);
    }

    private void OnAnimatorIK(int layerIndex)
    {
        animator.SetLookAtWeight(1,1,1);
        Vector3 pos = Quaternion.AngleAxis(changeX, transform.up) * (target.position + transform.forward * 10);
        pos = Quaternion.AngleAxis(changeY, transform.right) * pos;
        animator.SetLookAtPosition(pos);
    }
}
