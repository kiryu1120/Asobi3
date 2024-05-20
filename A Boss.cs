using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Boss : MonoBehaviour
{

    public int maxHP = 20;
    public int HP;
    public Slider slider;
    public GameObject Effect;

    public float chaseRange = 5.0f;
    public float speed = 6.0f;

    private float shootTimer = 0.0f;
    public float shootInterval = 2.0f;
    public GameObject ProjectilePrefab;
    private Vector2 Up;
    private Vector2 currentPosition;
    private float upSpeed = 3f;
    public TextMeshProUGUI tmpro;
    

    private Transform player;

    public enum ActionPattern
    {
        Appear,
        Attack,
        Tousou,
        kirikae,
    }
    private ActionPattern currentAction;


    // Start is called before the first frame update
    void Start()
    {
        //Sliderを最大にする。
        slider.value = 1;
        //HPを最大HPと同じ値に。
        HP = maxHP;
        player = GameObject.FindWithTag("Player").transform;
        Up = currentPosition + Vector2.up * 12.0f;
        tmpro.gameObject.SetActive(false);


    }

    // Update is called once per frame
    void Update()
    {


        switch (currentAction)
        {
            case ActionPattern.Appear:
                search();
                break;
            case ActionPattern.Attack:
                attack();
                break;
            case ActionPattern.Tousou:
                tousou();
                break;
            case ActionPattern.kirikae:
                kirikae();
                break;
        }
    }
    private void search()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // 追尾範囲内にプレイヤーが入ったら追尾
        if (distanceToPlayer <= chaseRange)
        {
            currentAction = ActionPattern.Attack;
        }
    }
    private void attack()
    {
        // シュートタイマーを更新する
        shootTimer += Time.deltaTime;

        // 再度撃つのに十分な時間が経過したかチェックする
        if (shootTimer >= shootInterval)
        {
            // タイマーをリセットする
            shootTimer = 0.0f;

            // 射的を行うメソッドを呼び出す（実際の射的のロジックに置き換えてください）。
            ShootProjectile();
        }
        void ShootProjectile()
        {
            // ボスからプレイヤーへの方向を求める
            Vector3 directionToPlayer = (player.position - transform.position).normalized;

            // 投射物を生成する（'ProjectilePrefab' を実際の投射物のプレハブに置き換えてください）
            GameObject projectile = Instantiate(ProjectilePrefab, transform.position, Quaternion.identity);

            // 投射物の速度を方向と希望する速度に基づいて設定する
            float projectileSpeed = 10.0f; // 必要に応じて速度を調整してください
            Rigidbody2D projectileRb = projectile.GetComponent<Rigidbody2D>();
            projectileRb.velocity = directionToPlayer * projectileSpeed;
        }
        if(HP <= 0)
        {
            currentAction = ActionPattern.Tousou;
        }
    }
    private void tousou()
    {
        
        if (transform.position.y <= 15f)
        {
            StartCoroutine(BossRunaway(Up));
            currentAction = ActionPattern.kirikae;
        }
    }

    IEnumerator BossRunaway(Vector2 targetPosition)
    {
        float startTime = Time.time;
        while (Time.time - startTime < 0.1f)
        {
            float t = (Time.time - startTime) * upSpeed; 
            transform.position = Vector3.Lerp(currentPosition,targetPosition,t);
            yield return null;
        }
        transform.position = targetPosition;

    }
    private void kirikae()
    {
        StartCoroutine("ChangeScene");
       
    }

    IEnumerator ChangeScene()
    {
        tmpro.gameObject.SetActive(true);
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("shooting");

    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        var LayerName = LayerMask.LayerToName(col.gameObject.layer);
        if (LayerName == "hantei")
        {
            Instantiate(Effect, transform.position, Quaternion.identity);
            HP = HP - 1;

            //HPをSliderに反映。
            slider.value = (float)HP / (float)maxHP;
        }

    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }
}
