using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImagePerImage : MonoBehaviour
{

    [SerializeField] private Sprite[] images;
    private int i = 0;
    private int index = 0;
    [SerializeField] private int imax = 100; //调整这个可以调整出现的速度
    private bool isFade;
    private bool isAppear;
    private int j;
    [SerializeField] int deleteJ = 5; //控制图片渐变的速度，越大越慢

    private void OnEnable()
    {
        i = imax;
        j = 255;
        isFade = false;
        isAppear = false;
        GetComponent<Image>().sprite = images[index];
        GetComponent<Image>().color = Vector4.one;
    }

    private void FixedUpdate()
    {
        if(isFade || isAppear)
        {
            if(isFade)
            {
                j -= deleteJ;
            }
            else
            {
                j += deleteJ;
            }
            GetComponent<Image>().color = new Vector4(1f, 1f, 1f, j / 255f);

            if (j <= 50 + deleteJ)
            {
                GetComponent<Image>().sprite = images[index];
                isFade = false;
                isAppear = true;
            }
            else if (isAppear && j >= 255 - deleteJ)
            {
                GetComponent<Image>().color = new Vector4(1f, 1f, 1f, 1f);
                j = 255;
                isAppear = false;
            }
        }
        else
        {
            i--;
            if (i <= 0)
            {
                index++;
                if (index >= images.Length)
                {
                    index = 0;
                }
                isFade = true;
                i = imax;
            }
        }
    }
}
