using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemybullet301 : MonoBehaviour
{
    void Update()
    {
        transform.localScale += new Vector3(1.5f, 1.5f, 1.5f) * Time.deltaTime;//逐渐变大
        GetComponent<Rigidbody2D>().velocity += GetComponent<Rigidbody2D>().velocity.normalized * Time.deltaTime * 40;//逐渐加速
    }
}
