using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpyBullet2 : MonoBehaviour
{
    public Vector3 destination;
    [SerializeField] Sprite[] sp;
    [SerializeField] AudioClip[] ac;
    private void Start()
    {
        GetComponent<CircleCollider2D>().enabled = false;
        GetComponent<SpriteRenderer>().sprite = sp[0];
        GetComponent<AudioSource>().clip = ac[0];
        GetComponent<AudioSource>().Play();
    }
    private void FixedUpdate()
    {
        if (transform.position == destination)
        {
            GetComponent<CircleCollider2D>().enabled = true;
            GetComponent<SpriteRenderer>().sprite = sp[1];
            transform.localScale += 2f * new Vector3(-Time.fixedDeltaTime, Time.fixedDeltaTime, 0);
            GetComponent<SpriteRenderer>().color = new Vector4(GetComponent<SpriteRenderer>().color.r, GetComponent<SpriteRenderer>().color.g, GetComponent<SpriteRenderer>().color.b, GetComponent<SpriteRenderer>().color.a / 1.3f);
            Destroy(gameObject, 0.3f);
            if (GetComponent<AudioSource>().clip == ac[0])
            {
                GetComponent<AudioSource>().Stop();
                GetComponent<AudioSource>().clip = ac[1];
            }
            if (!GetComponent<AudioSource>().isPlaying) 
            {
                GetComponent<AudioSource>().Play();
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, destination, 15 * Time.fixedDeltaTime);
        }
    }

}
