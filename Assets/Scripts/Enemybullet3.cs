using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemybullet3 : MonoBehaviour
{
    public GameObject p_bullet31;//引用bullet31的prefab
    private int countTask = 0;//计算task2被调用的次数，以抵消延迟

    private void OnEnable()
    {
        countTask = 0;//计算task2被调用的次数，以抵消延迟
        //task1
        p_bullet31.GetComponent<Enemybullet31>().taskNum = 1;//设置taskNum为1
        InvokeRepeating(nameof(Task1), 0f, 3.5f);//0,3.5,7,10.5s时进行task1
        Invoke(nameof(StopTask1), 11f);//停止task1
        //task2
        Invoke(nameof(SwitchTaskNum), 11f);//设置taskNum为2
        InvokeRepeating(nameof(Task2), 15f, 0.5f);//15,15.5,16,16.5s时进行task2
        Invoke(nameof(StopTask2), 16.7f);//停止task2
    }

    private void Task1()
    {
        Object.Instantiate(p_bullet31, transform);//创建子弹31
    }
    private void Task2()
    {
        p_bullet31.GetComponent<Enemybullet31>().delateCreateBullet301 = countTask * 1.5f;//设置延迟时间
        Object.Instantiate(p_bullet31, transform);//创建子弹31
        ++countTask;//增加下一次的延迟时间
    }
    private void StopTask1()
    {
        CancelInvoke(nameof(Task1));//停止task1
    }
    private void StopTask2()
    {
        CancelInvoke(nameof(Task2));//停止task2
    }
    private void SwitchTaskNum()
    {
        p_bullet31.GetComponent<Enemybullet31>().taskNum = 2;//设置taskNum为2
    }


}
