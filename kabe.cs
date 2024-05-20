using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kabe : MonoBehaviour
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
        var moveSpeed = 2;
        pos += moveDirection * moveSpeed * Time.deltaTime;
        transform.position = pos;
    }
}
