using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Expand : MonoBehaviour
{
    public Vector3 initialScale;
    public Vector3 ultiScale;
    public Vector3 initialPos;
    public Vector3 ultiPos;
    public float myTime;
    private Vector3 deltaScale;
    private Vector3 deltaPos;
    [SerializeField] RectTransform rt;


    private void OnEnable()
    {
        initialPos = transform.InverseTransformPoint(initialPos);//将世界坐标转化为本地坐标

        deltaScale = (ultiScale - initialScale) * Time.fixedDeltaTime / myTime;
        deltaPos = (ultiPos - initialPos) * Time.fixedDeltaTime / myTime;

        rt.localScale = initialScale;
        rt.localPosition = initialPos;
        StartCoroutine(Enlarge());
    }


    IEnumerator Enlarge()
    {
        while(true)
        {
            rt.localScale += deltaScale;
            rt.localPosition += deltaPos;
            yield return new WaitForFixedUpdate();
            if (rt.localScale.x >= ultiScale.x)
            {
                break;
            }
        }
    }
}
