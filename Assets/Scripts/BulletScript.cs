using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class BulletScript : MonoBehaviour
{
    public int blood;//血量
    private float blood0;//初始血量
    public GameObject bullet1;//引用子弹1prefab
    public bool bloodTest = true;//血量检测
    public bool allowReverse = true;//是否允许血量检测
    [SerializeField]private GameObject bloodSlide;//血条显示

    public Text scoreText;//分数显示
    private int score = 0;//分数
    public bool isSetScore = false;//是否开始计算分数

    public float deltaT;//计算死亡时延迟的时间

    [SerializeField] private AudioClip[] audios;

    private void Start()
    {
        blood = PlayerPrefs.GetInt("blood");
        score = 0;//每0.1s增加分数
        blood0 = blood;
        deltaT = 0;
        InvokeRepeating(nameof(SetScore), 0f, 0.1f);
    }
    //设置分数
    private void SetScore()
    {
        if(isSetScore)
        {
            scoreText.text = $"{score / 10f:000.0}" + "s";//0.1s增加0.1分
            ++score;
        }
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        string tag = collision.gameObject.tag;
        if (tag == "bullet10")
        {
            Destroy(collision.gameObject);//删除子弹
            blood -= 12;//扣血
            GetComponent<AudioSource>().clip = audios[1];
            GetComponent<AudioSource>().Play();
        }
        else if (tag == "bullet10task1") //接触子弹11
        {
            Destroy(collision.gameObject);//删除子弹11
            if (allowReverse)//首次接触子弹11后血量检测倒置
            {
                if (bloodTest)
                {
                    bloodTest = false;
                }
                else
                {
                    bloodTest = true;
                }
                allowReverse = false;
            }
            blood -= 40;//扣血
            GetComponent<AudioSource>().clip = audios[0];
            GetComponent<AudioSource>().Play();
        }
        else if (tag == "bullet0")
        {
            Destroy(collision.gameObject);//删除子弹
            GetComponent<AudioSource>().clip = audios[1];
            GetComponent<AudioSource>().Play();
        }
        else if (tag == "bullet500") 
        {
            Destroy(collision.gameObject);//删除子弹
            blood -= 200;
            GetComponent<AudioSource>().clip = audios[2];
            GetComponent<AudioSource>().Play();
        }
        
    }
    

    private void Update()
    {
        if (blood <= 0)
        {
            if (deltaT < 2f)  //死亡机制
            {
                Time.timeScale = 0f;
                deltaT += Time.unscaledDeltaTime - Time.deltaTime;
            }
            else
            {
                Time.timeScale = 1f;
                ExitGame();//回到主面板
            }
            if (0f < deltaT && deltaT <= 0.5f || 1f < deltaT && deltaT <= 1.5f)//player闪烁
            {
                GetComponent<SpriteRenderer>().enabled = false;
            }
            else
            {
                GetComponent<SpriteRenderer>().enabled = true;
            }
        }
        else
        {
            bloodSlide.GetComponent<Slider>().value = blood / blood0;//血条显示
        }
    }

    public void ExitGame()
    {
        if (!PlayerPrefs.HasKey("maxScore")|| PlayerPrefs.GetInt("maxScore") < score) 
        {
            PlayerPrefs.SetInt("maxScore", score);
        }
        SceneManager.LoadScene("StartMain");
    }
}
