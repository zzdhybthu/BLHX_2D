using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemybullet21 : MonoBehaviour
{
    public GameObject p_bullet201;//红毯子prefab
    public GameObject p_bullet202;//无伤子弹prefab
    public GameObject p_bullet203;//子弹prefab
    public float borderUp;//上边界
    public float borderDown;//下边界
    public int taskNum;
    private GameObject bullet201 = null;//红地毯实例
    private GameObject bullet202 = null;//无伤子弹实例
    private GameObject bullet203 = null;//子弹实例
    public float delateCreateBullet203 = 0;//延迟，抵消task2每轮提示间隔和实际产生子弹间隔的时差


    private void Start()
    {
        transform.position = new Vector3(8.8f, borderUp, 0f);//初始化位置
        switch (taskNum)
        {
            case 1:
                InvokeRepeating(nameof(CreateBullet201), 0f, 0.2f);//0,0.2闪烁红地毯
                Invoke(nameof(CancelBullet201), 0.3f);//停止闪烁红地毯
                InvokeRepeating(nameof(CreateBullet203), 0.4f, 0.03f);//产生子弹203
                Invoke(nameof(CancelBullet203), 1.2f);//停止产生子弹203
                Destroy(gameObject, 2.4f);//自我销毁
                break;
            case 2:
                CreateBullet201();//闪烁红地毯
                Invoke(nameof(CreateBullet201), 0.1f);
                InvokeRepeating(nameof(CreateBullet203), 3f + delateCreateBullet203, 0.03f);//产生子弹203
                Invoke(nameof(CancelBullet203), 3.8f + delateCreateBullet203);//停止产生子弹203
                Destroy(gameObject, 5f + delateCreateBullet203);//自我销毁
                break;
            case 31:
                InvokeRepeating(nameof(CreateBullet201), 0f, 0.1f);//闪烁红地毯
                Invoke(nameof(CancelBullet201), 0.25f);//停止闪烁红地毯
                InvokeRepeating(nameof(CreateBullet202), 3f + delateCreateBullet203, 0.03f);//产生子弹202
                Invoke(nameof(CancelBullet202), 3.8f + delateCreateBullet203);//停止产生子弹202
                Destroy(gameObject, 5f + delateCreateBullet203);//自我销毁
                break;
            case 32:
                InvokeRepeating(nameof(CreateBullet203), 3f + delateCreateBullet203, 0.03f);//产生子弹202
                Invoke(nameof(CancelBullet203), 3.8f + delateCreateBullet203);//停止产生子弹202
                Destroy(gameObject, 5f + delateCreateBullet203);//自我销毁
                break;
        }
    }

    //子弹201
    private void CreateBullet201()
    {
        bullet201 = Object.Instantiate(p_bullet201, transform);//产生红地毯
        bullet201.transform.localPosition = new Vector3(-8.8f, -1.25f, 0);//初始化相对位置
        Destroy(bullet201, 0.08f);//定时销毁红地毯
    }
    private void CancelBullet201()
    {
        CancelInvoke(nameof(CreateBullet201));//停止闪烁红地毯
    }

    //子弹202
    private void CreateBullet202()
    {
        for (int i = 0; i < 5; i++)
        {
            bullet202 = Object.Instantiate(p_bullet202, transform);//产生子弹202
            bullet202.transform.localPosition = new Vector3(0f, -0.4f - 0.425f * i, 0);//初始化相对位置
            bullet202.GetComponent<Rigidbody2D>().velocity = new Vector2(-40f, 0);//设置速度
            Destroy(bullet202, 0.5f);//定时销毁
        }
    }
    private void CancelBullet202()
    {
        CancelInvoke(nameof(CreateBullet202));//停止产生无伤子弹
    }

    //子弹203
    private void CreateBullet203()
    {
        for (int i = 0; i < 5; i++)
        {
            bullet203 = Object.Instantiate(p_bullet203, transform);//产生子弹203
            bullet203.transform.localPosition = new Vector3(0f, -0.4f - 0.425f * i, 0);//初始化相对位置
            bullet203.GetComponent<Rigidbody2D>().velocity = new Vector2(-40f, 0);//设置速度
            Destroy(bullet203, 0.5f);//定时销毁
        }
    }
    private void CancelBullet203()
    {
        CancelInvoke(nameof(CreateBullet203));//停止产生子弹203
    }

}
