using UnityEngine;

public class Lesson50 : MonoBehaviour
{
    private Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        animator.SetFloat("Speed",Input.GetAxis("Vertical"));
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetTrigger("jump");
        }
        // transform.Translate(Vector3.forward * (Time.deltaTime * moveSpeed * Input.GetAxis("Vertical")));
        animator.SetFloat("horizontalOffset",Input.GetAxis("Horizontal"));
        if (Input.GetKeyDown(KeyCode.J))
        {
            animator.SetTrigger("OtherAnimation");
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            animator.SetLayerWeight(animator.GetLayerIndex("IsHurt"),1);
        }
    }

    public void Jump()
    {
        
    }
}
