using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    private bool isFacingRight = true;
    public float speed = 2.0f; // 移動速度
    public float distance = 5.0f; // 移動する距離

    private bool movingRight = true;
    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        // 移動制御
        if (movingRight)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }

        // 一定の距離を移動したら方向を逆にする
        if (Vector3.Distance(startPosition, transform.position) >= distance)
        {
            movingRight = !movingRight;
            Flip();
        }

    }
    void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
        private void OnTriggerEnter2D(Collider2D col)
    {
        var LayerName = LayerMask.LayerToName(col.gameObject.layer);
        if (LayerName == "hantei")
        {
            Destroy(this.gameObject);
        }
    }
}
