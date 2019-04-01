using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayJokstick : MonoBehaviour
{
    private string currentButton;//当前按下的按键

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        var values = Enum.GetValues(typeof(KeyCode));//存储所有的按键
        for (int x = 0; x < values.Length; x++)
        {
            if (Input.GetKeyDown((KeyCode)values.GetValue(x)))
            {
                currentButton = values.GetValue(x).ToString();//遍历并获取当前按下的按键
            }
        }
    }
    // Show some data 
    void OnGUI()
    {
        GUI.TextArea(new Rect(0, 0, 250, 40), "Current Button : " + currentButton);//使用GUI在屏幕上面实时打印当前按下的按键
    }
}
