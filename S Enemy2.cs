using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SEnemy2 : MonoBehaviour
{

    int Hp = 10;
    public float speed = 5.0f; // 追尾速度
    public float chaseRange = 8.0f; // 追尾範囲
    private Transform player; // プレイヤーのTransfor

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform; // "Player" タグを持つオブジェクトのTransformを取得
    }

    void Update()
    {
        // プレイヤーとの距離を計算
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // 追尾範囲内にプレイヤーが入ったら追尾
        if (distanceToPlayer <= chaseRange)
        {
            // プレイヤーの方に向かって移動
            transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);

        }

    }
    public GameObject Effect;
    private void OnTriggerEnter2D(Collider2D col)
    {
        var LayerName = LayerMask.LayerToName(col.gameObject.layer);
        if (LayerName == "Bullet")
        {
            Hp -= 1;
            if (LayerName == "Player")
                Hp -= 1;
            if (Hp == 0)
            {
                GameManager.DefeatCount++;
                
                Destroy(this.gameObject);
                Instantiate(Effect, transform.position, Quaternion.identity);
            }
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }
}
