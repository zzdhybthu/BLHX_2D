using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RunnerCollider : MonoBehaviour
{
    [SerializeField] private CreateMath cMath;
    [SerializeField] private Text bloodText;
    [SerializeField] private AudioClip[] audios;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Math"))
        {
            Destroy(collision.GetComponent<RunnerAdd>().myTwin.GetComponent<Collider2D>());
            char itsTag = collision.GetComponent<RunnerAdd>().myTag;
            int itsNum = collision.GetComponent<RunnerAdd>().Num;
            switch(itsTag)
            {
                case 'A':
                    cMath.NumOfPlayer += itsNum;
                    GetComponent<AudioSource>().clip = audios[0];
                    break;
                case 'M':
                    cMath.NumOfPlayer -= itsNum;
                    GetComponent<AudioSource>().clip = audios[1];
                    break;
                case 'P':
                    cMath.NumOfPlayer *= itsNum;
                    GetComponent<AudioSource>().clip = audios[0];
                    break;
                case 'D':
                    cMath.NumOfPlayer /= itsNum;
                    GetComponent<AudioSource>().clip = audios[1];
                    break;
            }
            GetComponent<AudioSource>().Play();
            transform.localScale = new Vector3(0.1f * Mathf.Pow(cMath.NumOfPlayer, 0.3f), 0.1f * Mathf.Pow(cMath.NumOfPlayer, 0.3f), 0);
            Destroy(collision);
        }
        else if(collision.gameObject.CompareTag("mybullet"))
        {
            --cMath.NumOfPlayer;
            --cMath.DefaultNumOfPlayer;
            GetComponent<AudioSource>().clip = audios[2];
            GetComponent<AudioSource>().Play();
            transform.localScale = new Vector3(0.1f * Mathf.Pow(cMath.NumOfPlayer, 0.3f), 0.1f * Mathf.Pow(cMath.NumOfPlayer, 0.3f), 0);
            Destroy(collision.gameObject);
        }

        if (cMath.NumOfPlayer > 0)//显示volume，玩家可以指定是否显示
        {
            bloodText.text = "Volume: " + cMath.NumOfPlayer;
        }
        else
        {
            bloodText.text = "Volume: 0";
        }
    }
}
