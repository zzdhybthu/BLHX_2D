using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1 : MonoBehaviour
{
    
    Rigidbody2D rb = null;

    public float playerSpeed = 5f;//移动速率
    private float moveX;//x移速倍率
    private float moveY;//y移速倍率
    private float spaceX;//x坐标
    private float spaceY;//y坐标

    public float speedDownRate = 3f;//右侧左向牵制倍率
    private bool isSwiftMoveOn = true;//时空穿梭冷却终止与否
    public float swiftMovePause = 0.1f;//时空穿梭冷却时间间隔
    public float swiftMoveRate = 1.5f;//时空穿梭位移倍率
    public bool swiftMoveTran = false;//时空穿梭信息传递

    public ParticleSystem playerPS = null;//粒子特效系统

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        moveX = Input.GetAxis("Horizontal");//-1——0——1
        moveY = Input.GetAxis("Vertical");
        spaceX = this.transform.position.x;
        spaceY = this.transform.position.y;

        //时空穿梭
        if (Input.GetKeyDown(KeyCode.J) && isSwiftMoveOn)
        {
            swiftMoveTran = true;
            isSwiftMoveOn = false;
            Invoke(nameof(SetSwiftMoveOn), swiftMovePause);
        }

        //粒子系统
        if (moveX != 0 || moveY != 0) 
        {
            playerPS.Play();
        }
    }


    private void FixedUpdate()
    {
        //移动
        Move();
        if(swiftMoveTran)
        {
            SwiftMove();
            swiftMoveTran = false;
        }
    }

    
    //移动
    private void Move()
    {

        //移动
        if (spaceX < 0f)
        {
            rb.velocity = new Vector2(moveX * playerSpeed, moveY * playerSpeed);
        }
        else
        {
            rb.velocity = new Vector2((moveX - spaceX / speedDownRate) * playerSpeed, moveY * playerSpeed);
        }

        //边际修正
        if (spaceX < -8)
        {
            transform.position = new Vector3(-8f, spaceY, 0f);
        }
        if (spaceY > 4f)
        {
            transform.position = new Vector3(transform.position.x, 4f, 0f);
        }
        else if (spaceY < -4f) 
        {
            transform.position = new Vector3(transform.position.x, -4f, 0f);
        }
    }


    //时空穿梭
    private void SwiftMove()
    {
        if(spaceX<=0)
        {
            float deltaX = rb.velocity.normalized.x;
            float deltaY = rb.velocity.normalized.y;
            Vector3 deltaV = new Vector3(deltaX, deltaY, 0);
            transform.position += deltaV * swiftMoveRate;
        }
    }


    //时空穿梭时间间隔
    private void SetSwiftMoveOn()
    {
        isSwiftMoveOn = true;
    }
}
