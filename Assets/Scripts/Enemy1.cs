using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : MonoBehaviour
{
    public GameObject player;//引用船
    public GameObject p_bullet1;//引用子弹1prefab
    public GameObject bullet2;//引用子弹2
    public GameObject bullet3;//引用子弹3
    private GameObject bullet1 = null;//子弹1实例
    private BulletScript testBloodPlayer = null;//血量控制的脚本
    private Enemybullet1 testBloodBullet1 = null;//子弹1的脚本
    private Vector2 randomTarget = Vector2.zero;//随机移动的目标位置
    private Vector2 position2D;//当前位置
    private Animator anim;

    void Start()
    {
        Time.timeScale = 1f;
        anim = GetComponent<Animator>();
        InvokeRepeating(nameof(OneProcess), 0f, 78f);//发射弹幕
        InvokeRepeating(nameof(SetTimeScale), 78f, 78f);//每经过一轮便加速游戏
    }


    void Update()
    {
        position2D = this.GetComponent<Rigidbody2D>().position;
        if (position2D == randomTarget)
        {
            randomTarget = Random.insideUnitCircle * 3 + new Vector2(4f, 0f);//随机选取目标点
        }
        this.GetComponent<Rigidbody2D>().position = Vector2.MoveTowards(position2D, randomTarget, 2f * Time.deltaTime);//朝目标点以随机速度移动
    }


    private void OneProcess()
    {
        testBloodPlayer = player.GetComponent<BulletScript>();//获取血量控制的脚本
        testBloodPlayer.bloodTest = true;
        InvokeRepeating(nameof(CreateBullet1), 0, 4f);//0,4,8,12s时创建子弹1
        Invoke(nameof(CancelBullet1), 13);//13s时停止创建子弹1
        Invoke(nameof(CreateBullet2), 17);//17s时激活子弹2
        Invoke(nameof(CancelBullet2), 49);//49s时删除子弹2
        Invoke(nameof(CreateBullet3), 50);//50s时激活子弹3
        Invoke(nameof(CancelBullet3), 77);//77s时删除子弹3
    }

    private void SetTimeScale()
    {
        Time.timeScale *= 1.15f;
    }

    //创建子弹3
    private void CreateBullet3()
    {
        bullet3.SetActive(true);
    }
    //停止创建子弹3
    private void CancelBullet3()
    {
        bullet3.SetActive(false);
    }



    //激活子弹2
    private void CreateBullet2()
    {
        bullet2.SetActive(true);
    }
    //删除子弹2
    private void CancelBullet2()
    {
        bullet2.SetActive(false);
    }


    //创建子弹1
    private void CreateBullet1()
    {
        bullet1 = Object.Instantiate(p_bullet1, null);//创建子弹1实例
        bullet1.transform.position = transform.position;
        testBloodBullet1 = bullet1.GetComponent<Enemybullet1>();//获取子弹1实例的脚本
        if(testBloodPlayer.bloodTest)//传递血量控制脚本中的血量检测给子弹1实例，并将血量控制脚本中的血量检测倒置
        {
            testBloodBullet1.bloodTest = true;
            testBloodPlayer.bloodTest = false;
        }
        else
        {
            testBloodBullet1.bloodTest = false;
            testBloodPlayer.bloodTest = true;
        }
        testBloodPlayer.allowReverse = true;//允许血量检测脚本再次开始血量检测
        Invoke(nameof(SetIsFireTrue), 0);
        Invoke(nameof(SetIsFireFalse), 2);
    }
    //停止创建子弹1
    private void CancelBullet1()
    {
        CancelInvoke("CreateBullet1");//停止创建子弹1
    }

    private void SetIsFireTrue()
    {
        anim.SetBool("isfire", true);
    }
    private void SetIsFireFalse()
    {
        anim.SetBool("isfire", false);
    }
}
