using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleMove : MonoBehaviour
{
    [SerializeField]private float v;
    public float vx;
    public float vy;

    private bool enterDragging;
    private bool isDragging;
    [SerializeField] Sprite[] sp;
    private int spNum;
    private int aniNum;
    [SerializeField]private int maxAniNum;
    [SerializeField] private GameObject businessPanel;
    [SerializeField] private GameObject businessCanvas;
    [SerializeField] private AudioClip[] businessAudio;
    private int chooseBusinessAudio;
    [SerializeField] private AudioSource backgroundMusic;
    [SerializeField] private GameObject startMainCanvas;

    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(v * Mathf.Cos(45), v * Mathf.Sin(45));
        spNum = 0;
        aniNum = 1;
        enterDragging = false;
        chooseBusinessAudio = 0;
    }


    private void LateUpdate()//务必注意！lateupdate!//留意函数等调用次序
    {
        isDragging = GetComponent<Drag>().isDragging;
        if (isDragging && !enterDragging)
        {
            enterDragging = true;
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            GetComponent<Animator>().SetInteger("num", 0);
        }
        else if (!isDragging && enterDragging)
        {
            enterDragging = false;
            int angle = Random.Range(0, 360);
            GetComponent<Rigidbody2D>().velocity = new Vector2(v * Mathf.Cos(angle), v * Mathf.Sin(angle));
            GetComponent<Animator>().SetInteger("num", aniNum);
            spNum++;
            if(!PlayerPrefs.HasKey("maxScore"))
            {
                if (spNum >= sp.Length - 1)
                {
                    spNum = 0;
                }
            }
            else if (spNum >= sp.Length) 
            {
                spNum = 0;
                //奸商专属
                businessPanel.GetComponent<Expand>().initialPos = Camera.main.WorldToScreenPoint(transform.position);
                businessCanvas.SetActive(true);
                businessPanel.GetComponent<AudioSource>().clip = businessAudio[chooseBusinessAudio];
                chooseBusinessAudio = (chooseBusinessAudio + 1) % 2;
                businessPanel.GetComponent<AudioSource>().Play();
                backgroundMusic.Pause();
                startMainCanvas.SetActive(false);
            }
            GetComponent<SpriteRenderer>().sprite = sp[spNum];
        }
        else if(isDragging)
        {
            GetComponent<SpriteRenderer>().sprite = sp[spNum];
        }
        else if(!isDragging)
        {
            vx = GetComponent<Rigidbody2D>().velocity.x;
            vy = GetComponent<Rigidbody2D>().velocity.y;
            if (vx >= 0 && vy >= 0)
            {
                transform.localScale = new Vector3(3, 3, 0);
            }
            else if (vx >= 0 && vy < 0)
            {
                transform.localScale = new Vector3(3, -3, 0);
            }
            else if (vy >= 0)
            {
                transform.localScale = new Vector3(-3, 3, 0);
            }
            else
            {
                transform.localScale = new Vector3(-3, -3, 0);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("colliderMagic")&& !isDragging)
        {
            aniNum++;
            if (aniNum > maxAniNum)
            {
                aniNum = 1;
            }
            GetComponent<Animator>().SetInteger("num", aniNum);
        }
    }

    public void HideBusinessPanel()
    {
        businessCanvas.SetActive(false);
        backgroundMusic.Play();
        startMainCanvas.SetActive(true);
    }
}