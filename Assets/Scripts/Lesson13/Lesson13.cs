using UnityEngine;
using UnityEngine.U2D;

public class Lesson13 : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject testObj = new GameObject();
        SpriteRenderer sr = testObj.AddComponent<SpriteRenderer>();
        SpriteAtlas spriteAtlas = Resources.Load<SpriteAtlas>("MyAtlas");
        sr.sprite = spriteAtlas.GetSprite("Down1");
    }
    
}
