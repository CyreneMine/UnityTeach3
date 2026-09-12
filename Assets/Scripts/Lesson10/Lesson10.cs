using System;
using UnityEngine;

public class Lesson10 : MonoBehaviour
{
    private void Start()
    {
        GameObject temp = new GameObject();
        SpriteRenderer tempSprite = temp.AddComponent<SpriteRenderer>();
        tempSprite.sprite = MultipleMgr.Instance.GetSprite("RobotBoyCrouchSprite", "RobotBoyCrouch05");
    }
}

