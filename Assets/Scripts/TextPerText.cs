using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TextPerText : MonoBehaviour
{
    private string str;
    private Text tex;
    private int i = 0;   
    private int index = 0;
    private string str1 = "";
    private bool ison = true;
    [SerializeField]private int imax = 5; //调整这个可以调整出现的速度
    private int j = 0;
    [SerializeField]private int jmax = 30;//调整音效播放速度


    private void OnEnable()
    {
        tex = GetComponent<Text>();
        str = tex.text;
        tex.text = "";
        i = imax;
        j = jmax;
        index = 0;
        str1 = "";
        ison = true;
    }

    private void FixedUpdate()
    {
        if (ison)
        {
            --i;
            --j;
            if (i <= 0)
            {
                if (index >= str.Length)
                {
                    ison = false;
                    return;
                }
                str1 += str[index].ToString();
                tex.text = str1;
                index++;
                i = imax;
            }
            if (j <= 0) 
            {
                if (str[index - 1] != ' ') 
                {
                    GetComponent<AudioSource>().Play();
                }
                j = jmax + Random.Range(-5, 10);
            }
        }
    }

}



