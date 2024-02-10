using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RunnerAdd : MonoBehaviour
{
    public int Num;//自己的数字
    public char myTag;//与玩家识别自己是四则运算的哪一种
    public GameObject myTwin;//一起出现的教材，防止多次trigger
    public Text text;//自己的文本
    private void Start()
    {
        text = transform.GetComponentInChildren<Text>();
    }
    private void Update()
    {
        text.transform.position = Camera.main.WorldToScreenPoint(transform.position);//使text与自己一起移动，很重要
    }
}
