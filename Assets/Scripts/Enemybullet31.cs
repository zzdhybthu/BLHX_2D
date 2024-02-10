using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemybullet31 : MonoBehaviour
{
    public GameObject p_bullet301;//引用bullet301的prefab
    private GameObject bullet301 = null;//bullet301的实例
    public float rad = 1f;//风眼半径
    public int taskNum = 0;//任务序号
    public float delateCreateBullet301;//task2延迟时间

    void Start()
    {
        switch(taskNum)
        {
            case 1:
                transform.position = new Vector3(Random.Range(-5f, 1f), Random.Range(-3f, 3f), 0);//设置随机初始位置
                Invoke(nameof(HideThis), 0.4f);//闪烁
                Invoke(nameof(UnhideThis), 0.8f);//闪烁
                Invoke(nameof(HideThis), 2.5f);//隐藏
                Invoke(nameof(CreateBullet301), 2.5f);//创建子弹301
                Destroy(gameObject, 3.5f);//自我销毁
                break;
            case 2:
                transform.position = new Vector3(Random.Range(-5f, 1f), Random.Range(-3f, 3f), 0);//设置随机初始位置
                Invoke(nameof(HideThis), 0.15f);//闪烁
                Invoke(nameof(UnhideThis), 0.3f);//闪烁
                Invoke(nameof(HideThis), 0.5f);//隐藏
                Invoke(nameof(CreateBullet301), 3.5f + delateCreateBullet301);//延迟创建子弹301
                Destroy(gameObject, 4.5f + delateCreateBullet301);//延迟自我销毁
                break;
        }
    }

    void CreateBullet301()
    {
        GetComponentInChildren<AudioSource>().Play();
        for (int i = 0; i < 18; i++)
        {
            float x = rad * Mathf.Cos(360 / 18 * i * Mathf.PI / 180);//x相对坐标
            float y = rad * Mathf.Sin(360 / 18 * i * Mathf.PI / 180);//y相对坐标
            bullet301 = Object.Instantiate(p_bullet301, transform);//创建子弹301
            bullet301.transform.localPosition = new Vector3(x, y, 0);//设置相对坐标
            bullet301.GetComponent<Rigidbody2D>().velocity = new Vector2(x, y);//设置初始速度
            Destroy(bullet301, 1f);//销毁子弹301
        }
    }

    void HideThis()
    {
        GetComponent<SpriteRenderer>().enabled = false;//隐藏自己
    }
    void UnhideThis()
    {
        GetComponent<SpriteRenderer>().enabled = true;//取消隐藏
    }
}
