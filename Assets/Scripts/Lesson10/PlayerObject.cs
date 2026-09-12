using System;
using UnityEngine;

public class PlayerObject : MonoBehaviour
{
    public float moveSpeed = 5f;
    private SpriteRenderer sr; 
    private float h;
    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        h = Input.GetAxis("Horizontal");
        transform.Translate(moveSpeed * h * Time.deltaTime * Vector3.right);
        transform.Translate(moveSpeed * Input.GetAxis("Vertical") * Time.deltaTime * Vector3.up);
        if (h < 0)
            sr.flipX = true;
        else if (h > 0)
            sr.flipX = false;
    }
}
