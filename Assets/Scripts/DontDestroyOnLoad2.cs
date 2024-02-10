using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoad2 : MonoBehaviour
{
    public bool isSuccess = false;

    private static GameObject _instance = null;
    public static GameObject Instance { get { return _instance; } }
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
        DontDestroyOnLoad(gameObject);
    }
}
