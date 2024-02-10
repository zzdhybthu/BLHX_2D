using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CreateMath : MonoBehaviour
{
    [SerializeField] private GameObject add;
    [SerializeField] private GameObject minus;
    [SerializeField] private GameObject plus;
    [SerializeField] private GameObject divide;
    [SerializeField] private GameObject enemy;
    [SerializeField] private GameObject boss;
    [SerializeField] private GameObject player;
    private GameObject math1 = null;
    private GameObject math2 = null;
    private GameObject myEnemy = null;
    [SerializeField] private int minNum = 20;//最小的可以使用除法的volume
    [SerializeField] private int maxNum = 200;//最大的可以使用乘法的volume
    public int NumOfPlayer;//玩家所形成的volume
    public int DefaultNumOfPlayer;//理论上应有的volume的最大值
    public float myVelocity;//教材移动速度
    private int enemyOrTask;//决定召唤埃姆登还是教材
    private float maxRate;//控制乘法数字的大小，随着游戏的进行数字逐渐变大，但不超过maxnum
    public int myTime = 5;//轮数，可以由玩家决定
    private bool shouldFail = false;//用于防止在失败音效未结束时便退出游戏
    [SerializeField] private AudioSource backgroundMusic;//背景音乐

    private void Start()
    {
        Time.timeScale = 1.2f;
        InvokeRepeating(nameof(MyCreateMath), 2f, 5f);//一轮次所进行的动作
        Invoke(nameof(CancelInvokeMyCreateMath), myTime * 5f);//结束轮次
        Invoke(nameof(MyCreateBoss), myTime * 5f + 5f);//召唤boss
        NumOfPlayer = 1;//初始volume
        DefaultNumOfPlayer = 1;//初始理论最大volume
        enemyOrTask = 2;//开始时召唤教材
        maxRate = 0.1f;//初始maxnum比例
    }

    private void MyCreateMath()
    {
        if (enemyOrTask > 0) 
        {
            int sequence = Random.Range(0, 2);//产生随机数，随机分配乘法加法/除法减法的上下位置
            if (NumOfPlayer < minNum)
            {
                MyCreatePlus();
            }
            else if (NumOfPlayer <= maxNum)
            {
                int num = Random.Range(0, 2);
                if (num == 0)
                {
                    MyCreatePlus();
                }
                else
                {
                    MyCreateDivide();
                }
            }
            else
            {
                MyCreateDivide();
            }
            math1.transform.localPosition = new Vector3(0, 5 * sequence, 0);
            math2.transform.localPosition = new Vector3(0, 5 * (1 - sequence), 0);
            math1.GetComponent<Rigidbody2D>().velocity = new Vector2(-myVelocity, 0f);
            math2.GetComponent<Rigidbody2D>().velocity = new Vector2(-myVelocity, 0f);
            math1.GetComponent<RunnerAdd>().myTwin = math2;//让两本教材互相识别，防止与玩家多次trigger
            math2.GetComponent<RunnerAdd>().myTwin = math1;
            Destroy(math1, 25f / myVelocity);
            Destroy(math2, 25f / myVelocity);
            --enemyOrTask;
        }
        else
        {
            myEnemy = Instantiate(enemy);
            myEnemy.transform.position = new Vector3(4.5f, 0, 0);
            if (NumOfPlayer >= 20)
            {
                myEnemy.GetComponent<RunnerEnemy>().bulletNum = Random.Range(5, NumOfPlayer - 12);
            }
            else
            {
                myEnemy.GetComponent<RunnerEnemy>().bulletNum = Random.Range(2, 5);
            }
            myEnemy.GetComponent<RunnerEnemy>().RunnerPlayer = player;
            maxRate += (3 - maxRate) / 8;//每次召唤埃姆登后增加倍率，从而增加难度。但倍率始终低于3
            enemyOrTask = Random.Range(1, 3);
        }
    }


    private void MyCreatePlus()
    {
        math1 = Instantiate(plus, this.transform);
        math2 = Instantiate(add, this.transform);
        int myNum = Random.Range(10 / NumOfPlayer + 2, Mathf.CeilToInt(maxNum * maxRate / NumOfPlayer));//给定范围的随机数
        math1.GetComponent<RunnerAdd>().Num = myNum;
        math1.GetComponent<RunnerAdd>().myTag = 'P';//指定tag，用于与玩家的识别
        int moreOrLess = Random.Range(-5, 6);//随机给出加法相对于乘法的偏移量，玩家需要找出哪个更大
        math2.GetComponent<RunnerAdd>().Num = NumOfPlayer * (myNum - 1) + moreOrLess;
        math2.GetComponent<RunnerAdd>().myTag = 'A';
        math1.GetComponentInChildren<Text>().text = "× " + $"{myNum}";//显示数字
        math2.GetComponentInChildren<Text>().text = "+ " + $"{NumOfPlayer * (myNum - 1) + moreOrLess}";
        if (moreOrLess > 0)//修改默认最大volume的值
        {
            DefaultNumOfPlayer += math2.GetComponent<RunnerAdd>().Num;
        }
        else
        {
            DefaultNumOfPlayer *= myNum;
        }
    }
    private void MyCreateDivide()
    {
        math1 = Instantiate(divide, this.transform);
        math2 = Instantiate(minus, this.transform);
        int myNum = Random.Range(2, Mathf.Min(10, NumOfPlayer / 10 + 1));
        math1.GetComponent<RunnerAdd>().Num = myNum;
        math1.GetComponent<RunnerAdd>().myTag = 'D';
        int moreOrLess = Random.Range(-5, 6);
        math2.GetComponent<RunnerAdd>().Num = NumOfPlayer - NumOfPlayer / myNum + moreOrLess;
        math2.GetComponent<RunnerAdd>().myTag = 'M';
        math1.GetComponentInChildren<Text>().text = "÷ " + $"{myNum}";
        math2.GetComponentInChildren<Text>().text = "- " + $"{NumOfPlayer - NumOfPlayer / myNum + moreOrLess}";
        if (moreOrLess > 0)
        {
            DefaultNumOfPlayer /= myNum;
        }
        else
        {
            DefaultNumOfPlayer -= math2.GetComponent<RunnerAdd>().Num;
        }
    }

    private void CancelInvokeMyCreateMath()
    {
        CancelInvoke(nameof(MyCreateMath));
    }

    private void MyCreateBoss()
    {
        boss.SetActive(true);
        boss.transform.position = new Vector3(10f, 0, 0);
        boss.GetComponent<RunnerBoss>().bulletNum = DefaultNumOfPlayer - 1;//若成功，最后剩余volume恰为1
        boss.GetComponent<RunnerBoss>().RunnerPlayer = player;
    }


    private void Update()
    {
        if (NumOfPlayer <= 0)//失败
        {
            if(!GetComponent<AudioSource>().isPlaying)
            {
                if(!shouldFail)
                {
                    GetComponent<AudioSource>().Play();
                    backgroundMusic.Stop();
                    shouldFail = true;
                    Time.timeScale = 0;
                }
                else
                {
                    SceneManager.LoadScene(0);
                }
            }
        }
    }
}
