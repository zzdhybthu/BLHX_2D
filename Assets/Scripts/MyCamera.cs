using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCamera : MonoBehaviour
{
    private Ray ra;
    private RaycastHit hit;
    private bool is_element = false;
    private int flag = 0;
    private GameObject element;

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            ra = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ra, out hit) && hit.collider.CompareTag("Player"))
            {
                is_element = true;
                element = hit.collider.gameObject;
                if (flag == 0)
                {
                    flag = 1;
                }
                else
                {
                    flag = 0;
                }
            }
        }

        if (flag == 1 && is_element)
        {
            Vector3 targetScreenPos = Camera.main.WorldToScreenPoint(element.transform.position);
            Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, targetScreenPos.z);
            element.transform.position = Camera.main.ScreenToWorldPoint(mousePos);
        }
    }
}
