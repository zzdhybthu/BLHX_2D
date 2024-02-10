using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemybullet2 : MonoBehaviour
{
    private GameObject child = null;//子对象
    public GameObject[] children;//儿子们的prefab
    public GameObject chains;//链条
    private GameObject theChain = null;//链条实例
    private int countChains;//链条计数
    private int countTask2;//task2延迟时间比例
    
    void OnEnable()
    {
        countChains = 0;//链条计数
        countTask2 = 0;//task2延迟时间比例
        InvokeRepeating(nameof(CreateChains), 0f, 0.02f);//创建链条
        Invoke(nameof(StopCreateChains), 0.6f);//停止创建
        Invoke(nameof(SetTask1), 2f);//2s传递消息：任务1
        InvokeRepeating(nameof(Task1), 2.1f, 2.5f);//2.1s-9.6s重复任务1 4次
        Invoke(nameof(StopTask1), 10f);//10s取消任务1
        Invoke(nameof(SetTask2), 10.7f);//10.7s传递消息：任务2
        InvokeRepeating(nameof(Task2), 12f, 0.3f);//12s-13.2s重复任务2 5次
        Invoke(nameof(StopTask2), 13.3f);//13.3s取消任务2
        InvokeRepeating(nameof(Task3), 21.5f, 0.3f);//21.5s-22.7s重复任务3 5次
        Invoke(nameof(StopTask3), 22.8f);//22.8s取消任务3
    }

    private void CreateChains()
    {
        for (int i = 0; i < 5; i++)
        {
            theChain = Object.Instantiate(chains, transform);//创建链条
            theChain.transform.position = new Vector3(8.8f - countChains * 0.6f, 5f - i * 2.5f, 0);
            Destroy(theChain, 30f);//定时删除
        }
        ++countChains;//链条计数，用于确定位置x
    }
    private void StopCreateChains()
    {
        CancelInvoke(nameof(CreateChains));//停止创建链条
    }



    private void Task3()
    {
        for (int i = 0; i < 4; i++)
        {
            children[i].GetComponent<Enemybullet21>().taskNum = 32;//给每个子对象传递消息：任务32,32为普通子弹
        }

        int random = Random.Range(0, 4);//得到0-3的随机整数

        children[random].GetComponent<Enemybullet21>().taskNum = 31;//给选中的子对象传递消息：任务31,31为普通子弹
        for (int i = 0; i < 4; i++)
        {
            child = Object.Instantiate(children[i], transform);//创建儿子们
            child.GetComponent<Enemybullet21>().delateCreateBullet203 = 0.7f * countTask2;//指定延迟时间
        }
        ++countTask2;//增加延迟时间
    }
    private void StopTask3()
    {
        CancelInvoke(nameof(Task3));//停止任务1
    }


    private void SetTask2()
    {
        for (int i = 0; i < 4; i++)
        {
            children[i].GetComponent<Enemybullet21>().taskNum = 2;//给每个子对象传递消息：任务2
        }
    }

    private void Task2()
    {
        int random1 = Random.Range(0, 4);//得到0-3的随机整数
        int random2 = Random.Range(0, 3);//得到0-2的随机整数

        if (random2 >= random1)
        {
            ++random2;
        }
        child = Object.Instantiate(children[random1], transform);//创建指定儿子
        child.GetComponent<Enemybullet21>().delateCreateBullet203 = 0.7f * countTask2;//指定延迟时间
        child = Object.Instantiate(children[random2], transform);//创建指定儿子
        child.GetComponent<Enemybullet21>().delateCreateBullet203 = 0.7f * countTask2;//指定延迟时间
        ++countTask2;//增加延迟时间
    }
    private void StopTask2()
    {
        CancelInvoke(nameof(Task2));//停止任务1
        countTask2 = 0;
    }



    private void SetTask1()
    {
        for (int i = 0; i < 4; i++)
        {
            children[i].GetComponent<Enemybullet21>().taskNum = 1;//给每个子对象传递消息：任务1
        }
    }
    private void Task1()
    {
        int random = Random.Range(0, 4);//得到0-3的随机整数

        for (int i = 0; i < 4; i++)
        {
            if (i != random)
            {
                Object.Instantiate(children[i], transform);//创建指定儿子
            }
        }
    }
    private void StopTask1()
    {
        CancelInvoke(nameof(Task1));//停止任务1
    }
}
