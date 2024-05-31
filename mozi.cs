using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
//Textに使用
public class mozi : MonoBehaviour
{
    public TextMeshProUGUI tmpro;
    // Start is called before the first frame update
    void Start()
    {
        tmpro.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            gameObject.SetActive(false);
            tmpro.gameObject.SetActive(false);
        }
    }
}
