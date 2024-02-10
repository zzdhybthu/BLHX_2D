using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpyEnemy : MonoBehaviour
{
    private Vector2 randomTarget = new(6f, 0);//随机移动的目标位置
    private Vector2 position2D;//当前位置
    [SerializeField] private GameObject player;
    private Vector2 playerPosition;
    [SerializeField] private GameObject bullet1;
    private GameObject myBullet1;
    [SerializeField] private GameObject mirror1;
    private GameObject myMirror1;
    private int hurtInTask1;
    private int hurtInTask1Record;
    public int constHurtInTask1;
    [SerializeField] private GameObject bomb;
    private GameObject myBomb;
    [SerializeField] private GameObject trap;
    private GameObject myTrap;
    [SerializeField] private GameObject bullet2;
    private GameObject myBullet2;
    private Vector3 playerPositionInTask3;
    [SerializeField] GameObject bullet3;
    private GameObject myBullet3;
    [SerializeField]private AudioSource backgroundMusic;
    [SerializeField] private AudioClip ac;


    void Update()
    {
        position2D = GetComponent<Rigidbody2D>().position;
        playerPosition = player.GetComponent<Rigidbody2D>().position;
        if (position2D == randomTarget)
        {
            randomTarget = Random.insideUnitCircle * 2 + new Vector2(5f, 0f);//随机选取目标点
        }
        GetComponent<Rigidbody2D>().position = Vector2.MoveTowards(position2D, randomTarget, 1f * Time.deltaTime);//朝目标点以随机速度移动
    }

    private void Start()
    {
        hurtInTask1 = 0;
        hurtInTask1Record = 0;
        constHurtInTask1 = 0;
        InvokeRepeating(nameof(Task10), 0f, 6f);
        InvokeRepeating(nameof(Task1), 2f, 6f);
    }

    private void Task1()
    {
        myBullet1 = Instantiate(bullet1, transform);
        myBullet1.transform.position = transform.position + new Vector3(-3f, -0.6f, 0);
        myBullet1.GetComponent<Rigidbody2D>().velocity = 10 * (playerPosition - myBullet1.GetComponent<Rigidbody2D>().position).normalized - GetComponent<Rigidbody2D>().velocity;
        Destroy(myBullet1, 5f);
    }

    private void Task10()
    {
        if (hurtInTask1Record == hurtInTask1)
        {
            constHurtInTask1 = 0;
        }
        else
        {
            ++constHurtInTask1;
        }
        hurtInTask1Record = hurtInTask1;
        if (constHurtInTask1 == 5)
        {
            CancelInvoke(nameof(Task1));
            CancelInvoke(nameof(Task10));
            Invoke(nameof(Task2), 2f);
            return;
        }
        myMirror1 = Instantiate(mirror1);
        myMirror1.transform.position = new Vector3(-8.9f, Random.Range(-3f, 3f), 0);
        Destroy(myMirror1, 6f);
    }

    private void Task2()
    {
        if(player.GetComponent<Spyplayer>().isHurt1)
        {
            InvokeRepeating(nameof(CreateBullet1InTask2), 0f, 0.1f);
        }
        else
        {
            Invoke(nameof(Task3), 0);
        }
    }

    private void CreateBullet1InTask2()
    {
        if (player.GetComponent<Spyplayer>().blood <= 2000) 
        {
            CancelInvoke(nameof(CreateBullet1InTask2));
            Invoke(nameof(Task3), 3);
        }
        for (int i = -6; i <= 6; i++)
        {
            myBullet1 = Instantiate(bullet1, transform);
            myBullet1.transform.localScale = new Vector3(0.2f, 0.2f, 0);
            myBullet1.transform.position = transform.position + new Vector3(-3f, i, 0);
            myBullet1.GetComponent<Rigidbody2D>().velocity = new Vector2(-10, 0);
            Destroy(myBullet1, 2f);
        }
    }

    private void Task3()
    {
        for (int i = -2; i <= 2; i++)
        {
            myBomb = Instantiate(bomb);
            myBomb.transform.position = new Vector3(i * 2.5f - 3f, -i * 2, 0);
        }
        InvokeRepeating(nameof(Task30), 2, 5);
        InvokeRepeating(nameof(Task31), 0f, 1f);
        Invoke(nameof(CancelInvokeTask30), 23f);//23f
        Invoke(nameof(CancelInvokeTask31), 26f);//26f
    }

    private void Task30()
    {
        InvokeRepeating(nameof(CreateTrap), 0, 0.1f);
        Invoke(nameof(CancelCreateTrap), 2f);
        Invoke(nameof(CreateBullet2InTask3), 3f);
    }

    private void CancelInvokeTask30()
    {
        CancelInvoke(nameof(Task30));
    }

    private void Task31()
    {
        myBullet3 = Instantiate(bullet3, transform);
        myBullet3.transform.position = new Vector3(Random.Range(3f, 10f), Random.Range(-5f, 5f), 0);
        myBullet3.GetComponent<Rigidbody2D>().velocity = new Vector2(-5, 0);
        myBullet3.GetComponent<SpriteRenderer>().color = new Vector4(Random.Range(0f,1f), Random.Range(0f, 1f), Random.Range(0f, 1f),1);
        Destroy(myBullet3, 10f);
    }
    private void CancelInvokeTask31()
    {
        CancelInvoke(nameof(Task31));
        Invoke(nameof(End), 6f);
    }


    private void CreateTrap()
    {
        myTrap = Instantiate(trap);
        myTrap.transform.position = player.transform.position;
        Destroy(myTrap, 0.5f);
    }

    private void CancelCreateTrap()
    {
        CancelInvoke(nameof(CreateTrap));
        playerPositionInTask3 = player.transform.position;
    }

    private void CreateBullet2InTask3()
    {
        myBullet2 = Instantiate(bullet2, transform);
        myBullet2.transform.position = new Vector3(Random.Range(-8, 8), 8f, 0);
        myBullet2.GetComponent<SpyBullet2>().destination = playerPositionInTask3;
    }

    private void End()
    {
        if(player.GetComponent<Spyplayer>().isHurt2==true)
        {
            player.GetComponent<Spyplayer>().isHurt1 = false;
            player.GetComponent<Spyplayer>().isHurt2 = false;
            hurtInTask1 = 0;
            hurtInTask1Record = 0;
            constHurtInTask1 = 0;
            InvokeRepeating(nameof(Task10), 0f, 6f);
            InvokeRepeating(nameof(Task1), 2f, 6f);
        }
        else
        {
            PlayerPrefs.SetInt("isSuccess1", 1);
            if (!PlayerPrefs.HasKey("blood") || player.GetComponent<Spyplayer>().blood > PlayerPrefs.GetInt("blood"))
            {
                PlayerPrefs.SetInt("blood", player.GetComponent<Spyplayer>().blood);
            }
            backgroundMusic.clip = ac;
            backgroundMusic.Play();
            Invoke(nameof(Exit), 3.5f);
        }
    }

    private void Exit()
    {
        SceneManager.LoadScene("StartMain");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("bullet10task1"))
        {
            ++hurtInTask1;
            Destroy(collision.gameObject);
        }
    }
}
