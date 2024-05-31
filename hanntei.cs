using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Aplayの攻撃
public class hanntei : MonoBehaviour
{
    public float deleteTime = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(this.gameObject, deleteTime);
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
