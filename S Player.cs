using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;//UIを使うときに書きます。
using UnityEngine.SceneManagement;

public class Splayer : MonoBehaviour
{
    //最大HPと現在のHP。
    int maxHp = 10;
    int Hp;
    //Slider
    public Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        //Sliderを最大にする。
        slider.value = 1;
        //HPを最大HPと同じ値に。
        Hp = maxHp;
    }

    // Update is called once per frame
    void Update()
    {
        var x = Input.GetAxisRaw("Horizontal");
        var y = Input.GetAxisRaw("Vertical");
        var moveDirection = new Vector2(x, y).normalized;
        Move(moveDirection);
    }

    private void Move(Vector3 moveDirection)
    {
        var pos = transform.position;
        var moveSpeed = 7;
        pos += moveDirection * moveSpeed * Time.deltaTime;
        pos.x = Mathf.Clamp(pos.x, -8.4f, 8.4f);
        pos.y = Mathf.Clamp(pos.y, -4.5f, 4.5f);
        transform.position = pos;
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        var layerName = LayerMask.LayerToName(col.gameObject.layer);
        if (layerName == "Enemy")
        {
            Hp = Hp - 1;

            //HPをSliderに反映。
            slider.value = (float)Hp / (float)maxHp;  
        }
        if (Hp == 0)
        {
            gameObject.SetActive(false);
            SceneManager.LoadScene("Game over1");
        }
    }
}
