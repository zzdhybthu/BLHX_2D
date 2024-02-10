using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class p_Ju87D4 : MonoBehaviour
{
    public float a_speed = 0;//射速
    public int a_aviation = 0;//航空
    public int a_injuryup = 0;//3%增伤，0/1/2/3

    public int a_torpedoHurt = 0;//鱼雷单发伤害
    public int a_torpedoNum = 0;//鱼雷数目
    public int a_torpedoSpeed = 0;//鱼雷速度
    public int[] a_amortype = { 0, 0, 0 };//对甲比例
}
