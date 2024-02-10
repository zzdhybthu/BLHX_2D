using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemybullet1 : MonoBehaviour
{
    private Rigidbody2D rb = null;
    public float speedx;//子弹1速度，沿x
    public GameObject p_bullet11;//子对象子弹11的prefab
    private GameObject bullet11 = null;//子弹11实例
    public int bullet11Num;//每圈子弹11的数量
    public bool bloodTest;//血量检测
    private Vector3 target = new Vector3(-3f, 0f, 0f);//目标点
    private float speed;//速度

    void Start()
    {
        speed = (target - transform.position).magnitude / 1.5f;//在固定时间内移动到目标点，计算所需速度
        rb = GetComponent<Rigidbody2D>();
        InvokeRepeating("CreateBullet11", 1.5f, 0.06f);//每0.06s创建创建子弹11
        Invoke(nameof(CancelBullet11), 3f);//3s后停止创建子弹11
        Invoke("SelfDestroy", 4f);//4s后自我销毁
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);//移动
    }

    //创建子弹11
    void CreateBullet11()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        if(bloodTest)
        {
            p_bullet11.transform.localScale = new Vector3(0.5f, 0.5f, 0);//设置大小
        }
        else
        {
            p_bullet11.transform.localScale = new Vector3(1f, 0.2f, 0);
        }
        for (int i = 0; i < bullet11Num; i++)
        {
            bullet11 = Object.Instantiate(p_bullet11, transform);//创建子弹11
            bullet11.transform.localPosition = new Vector3(-0.5f, 0f, 0.1f);//设置初始位置
            if (bloodTest)
            {
                bullet11.transform.RotateAround(transform.position, Vector3.forward, 162.5f - 325f / (bullet11Num - 1) * i);//绕bullet1旋转一定角度
            }
            else
            {
                bullet11.transform.RotateAround(transform.position, Vector3.forward, 15f + 330f / (bullet11Num - 1) * i);
            }
            bullet11 .GetComponent<Rigidbody2D>().velocity = new Vector2(bullet11.transform.right.x, bullet11.transform.right.y) * 15f;//运动
            Destroy(bullet11, 1f);//1s后销毁
        }
    }

    //停止创建子弹11
    void CancelBullet11()
    {
        CancelInvoke("CreateBullet11");
    }


    //自我销毁
    void SelfDestroy()
    {
        Destroy(gameObject);
    }


}
