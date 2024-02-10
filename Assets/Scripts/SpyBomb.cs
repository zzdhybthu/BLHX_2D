using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpyBomb : MonoBehaviour
{
    [SerializeField] GameObject raid;
    private GameObject myRaid;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("bullet200"))
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Invoke(nameof(CreateRaid), 27f);//27f
        Destroy(gameObject, 32f);
    }

    private void CreateRaid()
    {
        myRaid = Instantiate(raid, transform);
        myRaid.transform.localPosition = new Vector3(30f, 0, 0);
        Invoke(nameof(RaidRotate), 2f);
        Destroy(myRaid, 4f);
    }

    private void RaidRotate()
    {
        myRaid.GetComponent<SpyRaid>().enabled = true;
        myRaid.GetComponent<Collider2D>().enabled = true;
    }
}
