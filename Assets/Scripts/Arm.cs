using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arm : MonoBehaviour
{
    
    public int stWD;//标准伤害standard weapon damage
    public float stFR;//标准射速standard fire rate
    public int bulletNumx;//连射x次
    public int bulletNumy;//每次y轮
    public int bulletNumz;//每轮z发
    public int strikingDis;//索敌距离
    public int strikingAngle;//索敌角度
    public float bulletSpeed;//子弹速度
    public GameObject p_bullet;//子弹的prefab
    private GameObject bullet = null;//子弹实例
    private Rigidbody2D bulletRD = null;//子弹的RD组件
    private GameObject enemy = null;//锁定的敌人
    private Vector2 enemyPosition2D;//2D下的enemy位置
    private Vector2 relativePosition;//2D下的enemy相对player的位置
    [SerializeField] Animator anim;

    private void Start()
    {
        InvokeRepeating(nameof(Fire0), 1f, stFR);//每隔一段时间发射一串子弹
    }

    private void FixedUpdate()
    {
        transform.localPosition = new Vector3(0f, -1.4f, 0f);//子弹位移与player同步
    }


    private void Fire0()
    {
        enemy = GameObject.FindGameObjectWithTag("enemy");//索敌
        enemyPosition2D = enemy.GetComponent<Rigidbody2D>().position;//获取敌人位置
        relativePosition = enemyPosition2D - GetComponent<Rigidbody2D>().position;//获取相对位置
        if (relativePosition.magnitude > strikingDis || Vector2.Angle(Vector2.right, relativePosition) > strikingAngle / 2)//判断是否在索敌范围内
        {
            InvokeRepeating(nameof(Fire10), 0f, 0.3f);
            Invoke(nameof(CancelFire10), (bulletNumx - 0.5f) * 0.3f);
        }
        else
        {
            InvokeRepeating(nameof(Fire1), 0f, 0.3f);
            Invoke(nameof(CancelFire1), (bulletNumx - 0.5f) * 0.3f);
        }
        Invoke(nameof(SetIsFireTrue), 0f);
        Invoke(nameof(SetIsFireFalse), 1.5f);
    }
    private void CancelFire1()
    {
        CancelInvoke(nameof(Fire1));
    }
    private void CancelFire10()
    {
        CancelInvoke(nameof(Fire10));
    }
    private void Fire1()//索敌成功，向敌人发射子弹
    {
        relativePosition = enemyPosition2D - GetComponent<Rigidbody2D>().position;
        InvokeRepeating(nameof(Fire2), 0f, 0.02f);
        Invoke(nameof(CancelFire2), (bulletNumy - 0.5f) * 0.02f);
    }
    private void Fire10()//如果索敌失败，向right发射子弹
    {
        relativePosition = Vector2.right;
        InvokeRepeating(nameof(Fire2), 0f, 0.02f);
        Invoke(nameof(CancelFire2), (bulletNumy - 0.5f) * 0.02f);

    }
    private void CancelFire2()
    {
        CancelInvoke(nameof(Fire2));
    }
    private void Fire2()
    {
        for (int i = 0; i < bulletNumz; i++)
        {
            bullet = Object.Instantiate(p_bullet, transform);//创建子弹
            bulletRD = bullet.GetComponent<Rigidbody2D>();//获取rigid组件
            bullet.transform.localPosition = new Vector3(0f, 0.5f - (0.5f * 2) / (bulletNumz - 1) * i, 4f);//设置初始位置
            bulletRD.rotation = Vector2.SignedAngle(Vector2.right, relativePosition) + 80;
            bulletRD.velocity = relativePosition.normalized * bulletSpeed;//设置速度
            Destroy(bullet, 1f);//销毁子弹
        }
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
