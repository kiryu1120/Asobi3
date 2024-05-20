using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class play : MonoBehaviour
{
    public int maxHP = 10;
    public int HP;
    public Slider slider;

    public float moveSpeed = 5.0f; // 移動速度
    public float jumpForce = 5.0f; // ジャンプ力
    public float groundCheckRadius = 0.76f; // 地面判定の半径
    public LayerMask groundLayer; // 地面のレイヤー
    private Rigidbody2D rb;
    private bool isGrounded;
    private bool isFacingRight = true; // プレイヤーの向き
    private Animator animator;

    public float boostedMoveSpeed = 10.0f; // ブースト時の移動速度
    public float boostedJumpForce = 10.0f; // ブースト時のジャンプ力
    public float boostDuration = 15.0f; // ブーストの持続時間
    public float boostCooldown = 20.0f; // ブーストのクールダウン時間
    public Color boostColor = Color.yellow; // ブースト中の色
    public float originalMoveSpeed;
    public float originalJumpForce;

    private bool isBoosted = false;
    private float boostTimer = 0f;
    private float cooldownTimer = 0f;
    private Color originalColor;
    void Start()
    {
        //Sliderを最大にする。
        slider.value = 1;
        //HPを最大HPと同じ値に。
        HP = maxHP;

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        originalColor = GetComponent<SpriteRenderer>().color;
    }

    void Update()
    {
        // 地面判定
        isGrounded = Physics2D.OverlapCircle(transform.position - new Vector3(0, groundCheckRadius, 0), groundCheckRadius, groundLayer);

        // 移動
        float moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        // プレイヤーの向きを更新
        if (moveInput > 0 && !isFacingRight)
        {
            Flip();
        }
        else if (moveInput < 0 && isFacingRight)
        {
            Flip();
        }

        // ジャンプ
        if (isGrounded && Input.GetButton("Jump"))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            animator.SetBool("jump", true);
            //animator.SetBool("walk", false);
        }
        else
        {
            if(isGrounded)
                animator.SetBool("jump", false);

            if (moveInput == 0)
            {
                animator.SetBool("walk", false);
            }
            else
            {
                animator.SetBool("walk", true);
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            animator.SetTrigger("attack");
        }


        // ブーストのクールダウン
        if (cooldownTimer > 0f)
        {
            cooldownTimer -= Time.deltaTime;
        }

        // ブースト
        if (Input.GetKeyDown(KeyCode.T) && !isBoosted && cooldownTimer <= 0f)
        {
            StartCoroutine(BoostPlayer());
            HP = HP + 5;
            slider.value = (float)HP / (float)maxHP;
        }
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        var LayerName = LayerMask.LayerToName(col.gameObject.layer);
        if (LayerName == "Enemy")
        {
            //HPから1を引く
            HP = HP - 1;

            //HPをSliderに反映。
            slider.value = (float)HP / (float)maxHP;

            if (HP == 0)
            {
                gameObject.SetActive(false);
                SceneManager.LoadScene("Game over");
            }
        }
        if (LayerName == "Dead")
        {
            //HPから1を引く
            HP = HP -  10;

            //HPをSliderに反映。
            slider.value = (float)HP / (float)maxHP;

            if (HP <= 0)
            {
                gameObject.SetActive(false);
                SceneManager.LoadScene("Game over");
            }
        }
    }


    IEnumerator BoostPlayer()
    {
        isBoosted = true;
        boostTimer = boostDuration;
        cooldownTimer = boostCooldown;

        // ブースト中の色を適用
        GetComponent<SpriteRenderer>().color = boostColor;

        // ブースト時の速度とジャンプ力を適用
        moveSpeed = boostedMoveSpeed;
        jumpForce = boostedJumpForce;

        // ブーストの効果が終了したら元の速度とジャンプ力、色に戻す
        yield return new WaitForSeconds(boostDuration);

        moveSpeed = originalMoveSpeed;
        jumpForce = originalJumpForce;

        isBoosted = false;

        GetComponent<SpriteRenderer>().color = originalColor;
    }


    // プレイヤーを水平方向に反転させる関数
    void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position - new Vector3(0, groundCheckRadius, 0), groundCheckRadius);
    }
}
