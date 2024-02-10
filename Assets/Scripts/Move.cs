using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//这是控制对象在规定范围内移动的脚本，其中swift move被注释掉了。不含其它任何功能。
public class Move : MonoBehaviour
{

    Rigidbody2D rb = null;

    public float playerSpeed = 5f;//移动速率
    private float moveX;
    private float moveY;
    private float myMoveX;//x移速倍率//使移动存在一个渐变速度
    private float myMoveY;//y移速倍率
    private float spaceX;//x坐标
    private float spaceY;//y坐标

    public float speedDownRate = 0.1f;//右侧左向牵制倍率，越小牵制倍率越大
    //private bool isSwiftMoveOn = true;//时空穿梭冷却终止与否
    //public float swiftMovePause = 0.1f;//时空穿梭冷却时间间隔
    //public float swiftMoveRate = 1.5f;//时空穿梭位移倍率
    //public bool swiftMoveTran = false;//时空穿梭信息传递
    [SerializeField] private float scaleY;//边界移动修正，可根据对象大小调整
    [SerializeField] private float scaleX;
    [SerializeField] private float speedDownX;//减速带边际
    [SerializeField] private float gradualSpeedUpRate;//单位固定时间渐变加速增幅
    private bool shouldSurfMusicOn;
    [SerializeField] private GameObject backGround;//用于播放滑水声音

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        myMoveX = myMoveY = 0;
        shouldSurfMusicOn = true;
    }


    void Update()
    {
        moveX = Input.GetAxis("Horizontal");//-1——0——1
        moveY = Input.GetAxis("Vertical");
        spaceX = this.transform.position.x;
        spaceY = this.transform.position.y;
        ////时空穿梭
        //if (Input.GetKeyDown(KeyCode.J) && isSwiftMoveOn)
        //{
        //    swiftMoveTran = true;
        //    isSwiftMoveOn = false;
        //    Invoke(nameof(SetSwiftMoveOn), swiftMovePause);
        //}
    }


    private void FixedUpdate()
    {
        //渐变
        myMoveX += (moveX - myMoveX) * gradualSpeedUpRate;
        myMoveY += (moveY - myMoveY) * gradualSpeedUpRate;
        //音效
        if (moveX == 1 || moveX == -1 || moveY == 1 || moveY == -1)
        {
            if(shouldSurfMusicOn)
            {
                backGround.GetComponent<AudioSource>().Play();
                shouldSurfMusicOn = false;
            }
        }
        else
        {
            shouldSurfMusicOn = true;
        }
        //移动
        MyMove();
        //if (swiftMoveTran)
        //{
        //    SwiftMove();
        //    swiftMoveTran = false;
        //}
    }


    //移动
    private void MyMove()
    {

        //移动
        if (spaceX < 0f - speedDownX)
        {
            rb.velocity = new Vector2(myMoveX * playerSpeed, myMoveY * playerSpeed);
        }
        else
        {
            rb.velocity = new Vector2((myMoveX - (spaceX + speedDownX) / speedDownRate) * playerSpeed, myMoveY * playerSpeed);
        }

        //边际修正
        if (spaceX < -8.8f + scaleX) 
        {
            transform.position = new Vector3(-8.8f + scaleX, spaceY, 0f);
        }
        if (spaceY > 5f - scaleY)
        {
            transform.position = new Vector3(transform.position.x, 5f - scaleY, 0f);
        }
        else if (spaceY < -5f + scaleY)
        {
            transform.position = new Vector3(transform.position.x, -5f + scaleY, 0f);
        }
    }


    //时空穿梭
    //private void SwiftMove()
    //{
    //    if (spaceX <= 0)
    //    {
    //        float deltaX = rb.velocity.normalized.x;
    //        float deltaY = rb.velocity.normalized.y;
    //        Vector3 deltaV = new Vector3(deltaX, deltaY, 0);
    //        transform.position += deltaV * swiftMoveRate;
    //    }
    //}


    ////时空穿梭时间间隔
    //private void SetSwiftMoveOn()
    //{
    //    isSwiftMoveOn = true;
    //}
}
