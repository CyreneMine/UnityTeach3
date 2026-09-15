using UnityEngine;

public class Lesson52 : MonoBehaviour
{
    private float mValue = 0.5f;
    private Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        animator.SetFloat("Speed", Input.GetAxis("Vertical")*mValue);
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            mValue = 1f;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            mValue = 0.5f;
        }
    }
}
