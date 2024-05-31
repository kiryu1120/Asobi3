using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//シューティングやスタート画面の背景に使用
public class haikei : MonoBehaviour
{
    public float ScrollSpeed;
    public float bgInterval;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        var nextPosX = transform.position.x - (ScrollSpeed * Time.deltaTime);
        transform.position = new Vector2(nextPosX, 0);

        if (transform.position.x <= -bgInterval)
        {
            transform.position = new Vector2(bgInterval, 0);
        }
    }
}
