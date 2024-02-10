using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject menuList;//menu
    private bool menuKeys = true;//暂停按钮被点击是否有效
    [SerializeField] private AudioSource bgm;//背景音乐组件
    [SerializeField] private Enemy1Script enemy1Script;//处理暂停时敌人仍闪烁的bug
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
            if (enemy1Script.canSetTimeScale == false && enemy1Script.canResetTimeScale == true)
            {
                enemy1Script.canCountDeltaTime = false;//敌人不再闪烁
            }
            Time.timeScale = 0f;//时间静止
        }
        else
        {
            menuList.SetActive(false);//显示menu
            menuKeys = true;//防止连续点击
            bgm.Play();//音乐继续
            if (enemy1Script.canSetTimeScale == false && enemy1Script.canResetTimeScale == true)
            {
                enemy1Script.canCountDeltaTime = true;//敌人继续闪烁
            }
            Time.timeScale = recordTimeScale;
        }
    }
    public void ExitMenuList()
    {
        if(!menuKeys)
        {
            menuList.SetActive(false);//显示menu
            menuKeys = true;//防止连续点击
            bgm.Play();//音乐继续
            if (enemy1Script.canSetTimeScale == false && enemy1Script.canResetTimeScale == true)
            {
                enemy1Script.canCountDeltaTime = true;//敌人继续闪烁
            }
            Time.timeScale = recordTimeScale;
        }
    }

}
