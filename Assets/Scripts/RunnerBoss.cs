using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RunnerBoss : MonoBehaviour
{
    [SerializeField] private GameObject bossBullet;
    private GameObject myBossBullet;
    public int bulletNum;
    public GameObject RunnerPlayer;
    [SerializeField] AudioClip ac;
    [SerializeField] private AudioSource backgroundMusic;//背景音乐
    [SerializeField] RunnerMenu rm;//记录使用暂停的次数

    private void OnEnable()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.left * 4f;
        Invoke(nameof(StopMove), 1f);
        InvokeRepeating(nameof(CreateBullet), 2f, 0.05f);
        Invoke(nameof(StopCreateBulletAndLeave), 2f);
    }

    private void StopCreateBulletAndLeave()
    {
        Invoke(nameof(StopCreateBullet), -0.02f + 0.05f * bulletNum);
        Invoke(nameof(Leave), 0.5f + 0.05f * bulletNum);
    }

    private void StopMove()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        GetComponent<AudioSource>().Play();
    }

    private void CreateBullet()
    {
        myBossBullet = Instantiate(bossBullet, transform);
        myBossBullet.transform.localPosition = Vector3.zero;
        myBossBullet.GetComponent<Rigidbody2D>().velocity = 100 * (RunnerPlayer.GetComponent<Rigidbody2D>().position - GetComponent<Rigidbody2D>().position).normalized;
        Destroy(myBossBullet, 0.5f);
    }
    private void StopCreateBullet()
    {
        CancelInvoke(nameof(CreateBullet));
    }

    private void Leave()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.right * 2f;
        Success();
    }

    private void Success()
    {
        backgroundMusic.Stop();
        GetComponent<AudioSource>().clip = ac;//成功语音
        GetComponent<AudioSource>().Play();
        PlayerPrefs.SetInt("isSuccess2", 1);
        if (!PlayerPrefs.HasKey("pauseRecord") || rm.pauseRecord < PlayerPrefs.GetInt("pauseRecord"))
        {
            PlayerPrefs.SetInt("pauseRecord", rm.pauseRecord);
        }
        Invoke(nameof(ExitGame), 5f);   
    }
    private void ExitGame()
    {
        SceneManager.LoadScene(0);
    }
}
