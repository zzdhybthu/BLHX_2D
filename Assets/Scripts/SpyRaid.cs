using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpyRaid : MonoBehaviour
{
    private void Update()
    {
        transform.RotateAround(transform.parent.position, Vector3.back, 1000 * Time.deltaTime);
    }
}
