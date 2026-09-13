using System;
using UnityEngine;

public class Lesson34 : MonoBehaviour
{
    private Animator animator;
    public float moveSpeed = 5f;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            animator.SetBool("isWalk", true);
        }else if (Input.GetKeyUp(KeyCode.W))
        {
            animator.SetBool("isWalk", false);
        }

        if (animator.GetBool("isWalk"))
        {
            transform.Translate(Vector3.forward * (Time.deltaTime * moveSpeed));
        }
    }
}
