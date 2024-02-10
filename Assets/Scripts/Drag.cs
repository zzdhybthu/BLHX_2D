using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drag: MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector3 mousePosition;
    private Vector3 objectPosition;
    private float distance;// 声明一个浮点数变量，用于记录鼠标与物体的距离
    public bool isDragging;
    [SerializeField] private float xl;
    [SerializeField] private float xr;
    [SerializeField] private float yu;
    [SerializeField] private float yd;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            // 如果碰撞到了物体
            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                // 开始拖拽物体
                isDragging = true;
                rb.isKinematic = true;
                // 设置物体为运动学物体，以便拖拽
                mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                // 记录鼠标位置
                objectPosition = transform.position - mousePosition;
                // 记录物体位置
                distance = Vector3.Distance(transform.position, Camera.main.transform.position);
                // 记录鼠标与物体的距离
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
            rb.isKinematic = false;
            
        }

        if (isDragging)
        {
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (mousePosition.x + objectPosition.x < xr && mousePosition.x + objectPosition.x > xl && mousePosition.y + objectPosition.y < yu && mousePosition.y + objectPosition.y > yd)
            {
                transform.position = new Vector3(mousePosition.x + objectPosition.x, mousePosition.y + objectPosition.y, distance);
            }
        }

    }
}


