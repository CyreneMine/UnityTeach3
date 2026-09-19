using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderFillMinToHide : MonoBehaviour
{
    private Slider slider;
    public TMP_Text txtHp;
    public GameObject fill;
    public void Awake()
    {
        slider = GetComponent<Slider>();
    }

    void Start()
    {
        slider.onValueChanged.AddListener(arg0 =>
        {
            if (arg0 <=0)
            {
                fill.SetActive(false);
            }
            else
            {
                fill.SetActive(true);
            }

            txtHp.text = $"{(int)slider.value}/100";
        });
    }
    
}
