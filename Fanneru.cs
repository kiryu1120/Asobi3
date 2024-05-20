using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fanneru : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }
    public GameObject BulletPrefab;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(BulletPrefab, transform.position, Quaternion.identity);
            GetComponent<AudioSource>();
        }
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        var layerName = LayerMask.LayerToName(col.gameObject.layer);
        if (layerName == "Enemy")
        {
            Destroy(this.gameObject);
        }
    }
}
