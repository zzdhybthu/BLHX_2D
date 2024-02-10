using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerEnemy : MonoBehaviour
{
    public int bulletNum;
    [SerializeField] private GameObject bullet;
    private GameObject myBullet;
    public GameObject RunnerPlayer;


    //注意，事实上这个埃姆登的出现对游戏是否胜利几乎没有任何直接的关联，不管玩家是否吃到子弹。唯一的关系是埃姆登的出现会增加maxnum的倍率，从而增加后续游戏难度
    private void Start()
    {
        Invoke(nameof(CreateBullet), 1f);
        Invoke(nameof(RunAway), 2f);
    }
    private void CreateBullet()
    {
        InvokeRepeating(nameof(CreateOneBullet), 0f, 1f / bulletNum);
        Invoke(nameof(CancelInvokeCreateOneBullet), 1f - 0.5f / bulletNum);
    }
    private void CreateOneBullet()
    {
        myBullet = Instantiate(bullet, transform);
        myBullet.transform.localPosition = Vector3.zero;
        myBullet.GetComponent<Rigidbody2D>().velocity = 50 * (RunnerPlayer.GetComponent<Rigidbody2D>().position - GetComponent<Rigidbody2D>().position).normalized;
        Destroy(myBullet, 1f);
    }
    private void CancelInvokeCreateOneBullet()
    {
        CancelInvoke(nameof(CreateOneBullet));
    }
    private void RunAway()
    {
        GetComponent<Rigidbody2D>().velocity = 10 * Vector2.right;
        Destroy(gameObject, 1f);
    }
}
