using UnityEngine;

public class Lesson50 : MonoBehaviour
{
    private Animator animator;
    public float horizontalOffset;
    public float moveSpeed;
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
    }

    public void Jump()
    {
        
    }
}
