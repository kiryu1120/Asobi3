using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SEnemy1 : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Move(Vector2.left);
        if (transform.position.x <= -20.0f)
        {
            Destroy(this.gameObject);
        }
    }
    private void Move(Vector3 moveDirection)
    {
        var pos = transform.position;
        var moveSpeed = 10;
        pos += moveDirection * moveSpeed * Time.deltaTime;
        transform.position = pos;
    }
public GameObject Effect;
    private void OnTriggerEnter2D(Collider2D col)
    {
        var LayerName = LayerMask.LayerToName(col.gameObject.layer);
        if (LayerName == "Bullet")
        {
            GameManager.DefeatCount1++;
            //Debug.Log("sibou");
            
            Destroy(this.gameObject);
            Instantiate(Effect, transform.position, Quaternion.identity);
        }
        if (LayerName == "Player")
        {
            GameManager.DefeatCount1++;
            Destroy(this.gameObject);
            Instantiate(Effect, transform.position, Quaternion.identity);
        }

    }
}
