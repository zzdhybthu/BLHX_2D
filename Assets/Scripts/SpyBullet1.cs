using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpyBullet1 : MonoBehaviour
{

    private GameObject cloneMe;
    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Math"))
        {
            GetComponent<AudioSource>().Play();
            Destroy(collision.gameObject);
            for (int i = 3; i > 0; i--)
            {
                cloneMe = Instantiate(gameObject, transform.parent);
                cloneMe.transform.position = transform.position + new Vector3(0f, 0, 0);
                cloneMe.transform.localEulerAngles = new Vector3(0, 0, 90);
                cloneMe.GetComponent<Rigidbody2D>().velocity = GetComponent<Rigidbody2D>().velocity + new Vector2(5, i * 4);
                Destroy(cloneMe, 3);
                cloneMe = Instantiate(gameObject, transform.parent);
                cloneMe.transform.position = transform.position + new Vector3(0f, 0, 0);
                cloneMe.transform.localEulerAngles = new Vector3(0, 0, 90);
                cloneMe.GetComponent<Rigidbody2D>().velocity = GetComponent<Rigidbody2D>().velocity + new Vector2(5, -i * 4);
                Destroy(cloneMe, 3);
            }
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<CircleCollider2D>().enabled = false;
        }

    }

}
