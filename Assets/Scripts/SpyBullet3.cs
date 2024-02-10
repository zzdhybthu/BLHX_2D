using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpyBullet3 : MonoBehaviour
{
    void Update()
    {
        transform.Rotate(Vector3.forward, 100 * Time.deltaTime);
        
    }
}
