using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//ABossの玉
public class birdstrike : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x >= -10.0f)
        {
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        var LayerName = LayerMask.LayerToName(col.gameObject.layer);
        if (LayerName == "player")
        {
            Destroy(this.gameObject);
        }
        if (LayerName == "hantei")
        {
            Destroy(this.gameObject);
        }
    }
}
