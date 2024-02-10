using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{
    public float maxScore = 0;//最高分
    public bool canShowOff = false;//是否显示showoff面板

    private static GameObject _instance = null;
    public static  GameObject Instance { get { return _instance; } }
 
    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);//再次切换场景时避免重复创建该对象
        }
        else
        {
            _instance = gameObject;
        }
    }
    void Start()
    {
        maxScore = 0;
        DontDestroyOnLoad(gameObject);
    }
}
