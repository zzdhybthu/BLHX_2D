using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Spyplayer : MonoBehaviour
{
    public bool isHurt1;
    public bool isHurt2;
    public int blood = 10000;
    private int blood0;
    [SerializeField] private AudioClip[] ac;
    [SerializeField] private GameObject bloodSlider;
    private bool shouldFail;
    [SerializeField] private AudioSource backgroundMusic;

    private void Start()
    {
        isHurt1 = false;
        isHurt2 = false;
        blood0 = blood;
        shouldFail = false;
        Time.timeScale = 1;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("bullet10task1"))
        {
            isHurt1 = true;
            blood -= 100;
            Destroy(collision.gameObject);
            GetComponent<AudioSource>().clip = ac[0];
            GetComponent<AudioSource>().Play();
        }
        else if(collision.gameObject.CompareTag("bullet200"))
        {
            blood -= 1000;
        }
        else if (collision.gameObject.CompareTag("bullet0"))
        {
            InvokeRepeating(nameof(Bullet0Action), 0f, 0.36f);
            Destroy(collision);
        }
        else if(collision.gameObject.CompareTag("bullet10"))
        {
            isHurt2 = true;
            blood -= 100;
            Destroy(collision.gameObject);
            GetComponent<AudioSource>().clip = ac[1];
            GetComponent<AudioSource>().Play();
        }
    }

    private void Bullet0Action()
    {
        blood -= 2000;
    }

    private void Update()
    {
        bloodSlider.GetComponent<Slider>().value = blood * 1f / blood0;
        if (blood <= 0)//失败，其实只会在boss出现后触发
        {
            GetComponent<AudioSource>().clip = ac[2];//失败语音
            if (!GetComponent<AudioSource>().isPlaying)
            {
                if (!shouldFail)
                {
                    GetComponent<AudioSource>().Play();
                    backgroundMusic.Stop();
                    shouldFail = true;
                    Time.timeScale = 0;
                }
                else
                {
                    SceneManager.LoadScene("StartMain");
                }
            }
        }
    }


}
