using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tama : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    void Update()
    {
        Move(Vector2.right);
        if (transform.position.x >= 20.0f)
        {
            Destroy(this.gameObject);
        }
    }
    private void Move(Vector3 moveDirection)
    {
        var pos = transform.position;
        var moveSpeed = 13;
        pos += moveDirection * moveSpeed * Time.deltaTime;
        transform.position = pos;
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        var LayerName = LayerMask.LayerToName(col.gameObject.layer);
        if (LayerName == "Enemy")
        {
            Destroy(this.gameObject);
        }
        if (LayerName == "Block")
        {
            Destroy(this.gameObject);
        }
    }
}
