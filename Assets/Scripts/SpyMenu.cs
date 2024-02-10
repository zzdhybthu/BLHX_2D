using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpyMenu : MonoBehaviour
{
    public GameObject menuList;//menu
    private bool menuKeys = true;//暂停按钮被点击是否有效
    [SerializeField] private AudioSource bgm;//背景音乐组件
    private float recordTimeScale;//记录原本的timescale

    private void Start()
    {
        menuKeys = true;
    }
    public void ActivateMenuList()
    {
        if (menuKeys)
        {
            recordTimeScale = Time.timeScale;//记录原本的timescale
            menuList.SetActive(true);//显示menu
            menuKeys = false;//防止连续点击
            bgm.Pause();//音乐暂停
            menuList.GetComponent<AudioSource>().Play();
            Time.timeScale = 0f;//时间静止
        }
        else
        {
            menuList.SetActive(false);//显示menu
            menuKeys = true;//防止连续点击
            bgm.Play();//音乐继续
            Time.timeScale = recordTimeScale;
        }
    }
    public void ExitMenuList()
    {
        if (!menuKeys)
        {
            menuList.SetActive(false);//隐藏menu
            menuKeys = true;//防止连续点击
            bgm.Play();//音乐继续
            Time.timeScale = recordTimeScale;
        }
    }
    public void ExitGame()
    {
        SceneManager.LoadScene(0);
    }
}
