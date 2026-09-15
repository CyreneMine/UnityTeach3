using UnityEngine;

public class bulletObj : MonoBehaviour
{
    public Vector3 nowDic;
    public float moveSpeed = 10f;
    void Start()
    {
        Destroy(gameObject, 5f);
    }
    public void ChangeDic(Vector3 dic)
    {
        nowDic = dic;
    }
    // Update is called once per frame
    void Update()
    {
        transform.Translate(nowDic * moveSpeed * Time.deltaTime);
        
    }
}
