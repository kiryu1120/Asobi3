using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//A playのEを押したときhanteiを出す処理
public class kougeki : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }
    public GameObject hantei;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Instantiate(hantei, transform.position, Quaternion.identity);
        }
    }
}
