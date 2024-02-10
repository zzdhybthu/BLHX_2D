using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{
    [SerializeField] private Text scoreText;
    [SerializeField] private Text startText;
    [SerializeField] private GameObject showOffButton;
    [SerializeField] private GameObject showOffCanvas;
    [SerializeField] private Text showOffScoreText;
    [SerializeField] private GameObject tipsPanel;
    [SerializeField] private GameObject fightCanvas;
    [SerializeField] private GameObject fightButttom;
    [SerializeField] private GameObject startMainCanvas;
    [SerializeField] private Button warmUp2Button;
    [SerializeField] private Button startButton;
    [SerializeField] private Text warmUp1Text;
    [SerializeField] private Text warmUp2Text;
    [SerializeField] private Text runAwayText;
    [SerializeField] private GameObject plotCanvas;
    [SerializeField] private GameObject imagePanelButton;
    [SerializeField] private GameObject imagePanel;
    [SerializeField] private GameObject fightPanel;
    public GameObject[] plot0Buttons;
    public GameObject[] plot1Buttons;
    public GameObject[] plot2Buttons;
    public GameObject[] plot3Buttons;

    void Start()
    {
        Time.timeScale = 1;
        if (PlayerPrefs.HasKey("maxScore"))
        {
            scoreText.text = "Max Score: " + $"{PlayerPrefs.GetInt("maxScore") / 10f:000.0}";//传递最高分
            startText.enabled = true;
            startText.text = "TryAgain!";
            startText.fontSize = 40;
            if (PlayerPrefs.GetInt("maxScore") != 0)
            {
                showOffButton.SetActive(true);
                imagePanelButton.SetActive(true);
            }
            runAwayText.enabled = false;
            warmUp2Text.enabled = false;
            warmUp1Text.enabled = false;
            startButton.enabled = true;
            warmUp2Button.enabled = true;
        }
        else if (PlayerPrefs.HasKey("isSuccess2")) 
        {
            startText.enabled = true;
            runAwayText.enabled = true;
            warmUp2Text.enabled = false;
            warmUp1Text.enabled = false;
            startButton.enabled = true;
            warmUp2Button.enabled = true;
        }
        else if (PlayerPrefs.HasKey("isSuccess1"))
        {
            startText.enabled = false;
            runAwayText.enabled = true;
            warmUp2Text.enabled = true;
            warmUp1Text.enabled = false;
            startButton.enabled = false;
            warmUp2Button.enabled = true;
        }
        else
        {
            startText.enabled = false;
            runAwayText.enabled = true;
            warmUp2Text.enabled = false;
            warmUp1Text.enabled = true;
            startButton.enabled = false;
            warmUp2Button.enabled = false;
        }

        if(!PlayerPrefs.HasKey("plotNum"))
        {
            PlayerPrefs.SetInt("plotNum", 0);
            PlayerPrefs.SetInt("plotPicNum", 0);
            ShowPlot();
        }
        else
        {
            GetComponent<AudioSource>().Play();
        }
    }


    //切换showoff界面
    public void ShowOff()
    {
        startMainCanvas.SetActive(false);
        showOffCanvas.SetActive(true);
        showOffScoreText.text = $"{PlayerPrefs.GetInt("maxScore")}";
    }

    //停止showoff
    public void StopShowOff()
    {
        showOffCanvas.SetActive(false);
        startMainCanvas.SetActive(true);
    }

    //退出游戏
    public void ExitGame()
    {
        Application.Quit();
    }

    //查看提示
    public void LookForTips()
    {
        tipsPanel.SetActive(true);
        fightButttom.SetActive(false);
    }

    //退出提示
    public void HideTips()
    {
        tipsPanel.SetActive(false);
        fightButttom.SetActive(true);
    }

    
    public void ShowFightCanvas()
    {
        fightCanvas.SetActive(true);
        startMainCanvas.SetActive(false);
        GetComponent<AudioSource>().Stop();
    }

    public void HideFightCanvas()
    {
        fightCanvas.SetActive(false);
        startMainCanvas.SetActive(true);
        GetComponent<AudioSource>().Play();
    }

    public void StartWarmUp1()
    {
        if (PlayerPrefs.GetInt("plotNum") == 0)
        {
            PlayerPrefs.SetInt("plotNum", 1);
            ShowPlot();
        }
        else
        {
            SceneManager.LoadScene("Spy");
        }
    }

    public void StartWarmUp2()
    {
        if (PlayerPrefs.GetInt("plotNum") == 1)
        {
            PlayerPrefs.SetInt("plotNum", 2);
            ShowPlot();
        }
        else
        {
            SceneManager.LoadScene("Runner");
        }
    }

    public void StartMenu()
    {
        if (PlayerPrefs.GetInt("plotNum") == 2)
        {
            PlayerPrefs.SetInt("plotNum", 3);
            ShowPlot();
        }
        else
        {
            SceneManager.LoadScene("Play");
        }
    }

    public void ShowImagePanel()
    {
        imagePanel.SetActive(true);
        fightPanel.SetActive(false);
    }

    public void HideImagePanel()
    {
        fightPanel.SetActive(true);
        imagePanel.SetActive(false);
    }

    public void ShowPlot()
    {
        startMainCanvas.SetActive(false);
        fightCanvas.SetActive(false);
        plotCanvas.SetActive(true);
        switch(PlayerPrefs.GetInt("plotNum"))
        {
            case 0:
                plot0Buttons[PlayerPrefs.GetInt("plotPicNum")].SetActive(true);
                break;
            case 1:
                plot1Buttons[PlayerPrefs.GetInt("plotPicNum")].SetActive(true);
                break;
            case 2:
                plot2Buttons[PlayerPrefs.GetInt("plotPicNum")].SetActive(true);
                break;
            case 3:
                plot3Buttons[PlayerPrefs.GetInt("plotPicNum")].SetActive(true);
                break;
        }
        PlayerPrefs.SetInt("plotPicNum", PlayerPrefs.GetInt("plotPicNum") + 1);
    }

    //因为二维数组无法序列化，姑且这样吧，以后需要时再找好些的方法

    public void SwitchPlotPic()
    {
        switch(PlayerPrefs.GetInt("plotNum"))
        {
            case 0:
                if (PlayerPrefs.GetInt("plotPicNum") < 2)//这里直接填写图片数量
                {
                    plot0Buttons[PlayerPrefs.GetInt("plotPicNum")].SetActive(true);
                    plot0Buttons[PlayerPrefs.GetInt("plotPicNum") - 1].SetActive(false);
                    PlayerPrefs.SetInt("plotPicNum", PlayerPrefs.GetInt("plotPicNum") + 1);
                }
                else
                {
                    plot0Buttons[PlayerPrefs.GetInt("plotPicNum") - 1].SetActive(false);
                    PlayerPrefs.SetInt("plotPicNum", 0);
                    startMainCanvas.SetActive(true);
                    plotCanvas.SetActive(false);
                    GetComponent<AudioSource>().Play();
                }
                break;
            case 1:
                if (PlayerPrefs.GetInt("plotPicNum") < 1)//这里直接填写图片数量
                {
                    plot1Buttons[PlayerPrefs.GetInt("plotPicNum")].SetActive(true);
                    plot1Buttons[PlayerPrefs.GetInt("plotPicNum") - 1].SetActive(false);
                    PlayerPrefs.SetInt("plotPicNum", PlayerPrefs.GetInt("plotPicNum") + 1);
                }
                else
                {
                    plot1Buttons[PlayerPrefs.GetInt("plotPicNum") - 1].SetActive(false);
                    PlayerPrefs.SetInt("plotPicNum", 0);
                    plotCanvas.SetActive(false);
                    SceneManager.LoadScene("Spy");
                }
                break;
            case 2:
                if (PlayerPrefs.GetInt("plotPicNum") < 2)//这里直接填写图片数量
                {
                    plot2Buttons[PlayerPrefs.GetInt("plotPicNum")].SetActive(true);
                    plot2Buttons[PlayerPrefs.GetInt("plotPicNum") - 1].SetActive(false);
                    PlayerPrefs.SetInt("plotPicNum", PlayerPrefs.GetInt("plotPicNum") + 1);
                }
                else
                {
                    plot2Buttons[PlayerPrefs.GetInt("plotPicNum") - 1].SetActive(false);
                    PlayerPrefs.SetInt("plotPicNum", 0);
                    plotCanvas.SetActive(false);
                    SceneManager.LoadScene("Runner");
                }
                break;
            case 3:
                if (PlayerPrefs.GetInt("plotPicNum") < 1)//这里直接填写图片数量
                {
                    plot3Buttons[PlayerPrefs.GetInt("plotPicNum")].SetActive(true);
                    plot3Buttons[PlayerPrefs.GetInt("plotPicNum") - 1].SetActive(false);
                    PlayerPrefs.SetInt("plotPicNum", PlayerPrefs.GetInt("plotPicNum") + 1);
                }
                else
                {
                    plot3Buttons[PlayerPrefs.GetInt("plotPicNum") - 1].SetActive(false);
                    PlayerPrefs.SetInt("plotPicNum", 0);
                    plotCanvas.SetActive(false);
                    SceneManager.LoadScene("Play");
                }
                break;
        }

    }
}
