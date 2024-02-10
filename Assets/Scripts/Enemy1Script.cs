using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy1Script : MonoBehaviour
{
    public int blood;//血量
    private int blood0;//记录初始血量
    private float bloodPer;//储存bloodpercent长度
    private float deltaT = 0;//暂停时间
    private float recordTimeScale;//记录timescale
    public bool canSetTimeScale = true;//防止再次将timescale设为0
    public bool canResetTimeScale = true;//防止再次将timescale还原
    public bool canCountDeltaTime = false;//确定是否应该计算暂停时间并闪烁
    public BulletScript bs;
    [SerializeField] private Text bloodPercentText;//显示血量百分比
    [SerializeField] private GameObject bloodPercent;//显示血条
    [SerializeField] private GameObject enemyBlood;//显示血量的组件
    private Image[] sliderColors = null;//获取slider所有儿孙的image组件


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("mybullet"))//收到己方子弹
        {
            int x = collision.GetComponentInParent<Arm>().stWD;//获取子弹伤害
            blood -= x;//扣血
            Destroy(collision.gameObject);//删除子弹
            sliderColors = bloodPercent.GetComponentsInChildren<Image>();//获取slider所有儿孙的image组
            foreach (Image child in sliderColors)//遍历，slidercolors是可遍历的对象
            {
                child.color = new Vector4(1, Random.Range(0, 125) / 255f, Random.Range(0, 255) / 255f, 1);//随机更换颜色
            }
        }
    }

    private void Start()
    {
        blood = 60000 * (1 + PlayerPrefs.GetInt("pauseRecord"));
        blood0 = blood;//记录初始血量
        canSetTimeScale = true;
        canResetTimeScale = true;
    }

    private void Update()
    {
        if (blood > 0)
        {
            bloodPercentText.text = "X" + Mathf.CeilToInt(blood * 100f / blood0);//显示血量百分
            bloodPer = blood0 * 1f / 100;
            if (blood % bloodPer == 0)
            {
                bloodPercent.GetComponent<Slider>().value = 1f;
            }
            else
            {
                bloodPercent.GetComponent<Slider>().value = blood % bloodPer / bloodPer;
            }
        }
        else //血量为0时，实现其它暂停，enemy闪烁的效果，这里浪费里很多时间，搜不到好的方法，只能用我自己的
        {
            bloodPercentText.text = "X???";//显示血量百分
            bloodPer = blood0 * 1f / 100;
            if ((-blood) % bloodPer == 0)
            {
                bloodPercent.GetComponent<Slider>().value = 1f;
            }
            else
            {
                bloodPercent.GetComponent<Slider>().value = 1 - (-blood) % bloodPer / bloodPer;
            }
            SetTimeScale();
        }
        if (deltaT > 3f)
        {
            ResetTimeScale();
        }
        if (canCountDeltaTime)
        {
            deltaT += Time.unscaledDeltaTime;//计算暂停时间
            if (0f < deltaT && deltaT <= 0.5f || 1f < deltaT && deltaT <= 1.5f)//敌人闪烁
            {
                GetComponent<SpriteRenderer>().enabled = false;
            }
            else
            {
                GetComponent<SpriteRenderer>().enabled = true;
            }
        }
    }

    //将timescale重置
    private void ResetTimeScale()
    {
        if(canResetTimeScale)
        {
            Time.timeScale = recordTimeScale;//恢复原来的时间
            canResetTimeScale = false;
            canCountDeltaTime = false;
        }
    }

    //将timescale设为0并显示面板
    private void SetTimeScale()
    {
        if(canSetTimeScale)
        {
            recordTimeScale = Time.timeScale;//记录timescale
            Time.timeScale = 0f;//暂停
            bs.isSetScore = true;//开始计算分数
            canSetTimeScale = false;
            canCountDeltaTime = true;
        }
    }
}
