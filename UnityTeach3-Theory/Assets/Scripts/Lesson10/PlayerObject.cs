using System;
using UnityEngine;

public class PlayerObject : MonoBehaviour
{
    public Sprite bulletSprite;
    public float moveSpeed = 5f;
    private SpriteRenderer sr;
    private Rigidbody2D rigidbody2D;
    private float h;
    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        h = Input.GetAxis("Horizontal");
        // transform.Translate(moveSpeed * h * Time.deltaTime * Vector3.right);
        // transform.Translate(moveSpeed * Input.GetAxis("Vertical") * Time.deltaTime * Vector3.up);
        if (h != 0)
            rigidbody2D.linearVelocity = new Vector2(h * moveSpeed, rigidbody2D.linearVelocity.y);
        if (Input.GetKeyDown(KeyCode.K))
        {
            rigidbody2D.AddForce(Vector2.up*300);
        }
        if (h < 0)
            sr.flipX = true;
        else if (h > 0)
            sr.flipX = false;
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject bullet = new GameObject();
            bullet.transform.position = transform.position+new Vector3(sr.flipX?-0.5f:0.5f,-0.3f,0);
            bullet.AddComponent<SpriteRenderer>().sprite = bulletSprite;
            bullet.AddComponent<bulletObj>().ChangeDic(sr.flipX ? Vector3.left : Vector3.right);
        }
    }
}
