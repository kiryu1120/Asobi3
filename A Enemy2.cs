using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : MonoBehaviour
{
    public float speed = 3.0f; // 追尾速度
    public float chaseRange = 8.0f; // 追尾範囲
    private Transform player; // プレイヤーのTransform
    private Animator animator;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform; // "Player" タグを持つオブジェクトのTransformを取得
        animator = GetComponent<Animator>();
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

            animator.SetBool("isChasing", true);
        }
        else
        {
            animator.SetBool("isChasing", false);
        }
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        var LayerName = LayerMask.LayerToName(col.gameObject.layer);
        if (LayerName == "hantei")
        {
            Destroy(this.gameObject);
          
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }
}