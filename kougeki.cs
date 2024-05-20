using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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