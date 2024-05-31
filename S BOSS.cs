using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SBOSS : MonoBehaviour
{
    public int HP = 100;
    public float speed = 7f;
    private Camera mainCamera;
    private float minX, maxX, minY, maxY;
    private Animator animator;

    public  Color newColor = Color.red;
    public enum ActionPattern
    {
        Apper,
        Attack,
        Ikari,
    }
    private ActionPattern currentAction;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        float camHeight = 2f * mainCamera.orthographicSize;
        float camWidth = camHeight * mainCamera.aspect;

        minX = mainCamera.transform.position.x - camWidth / 2f;
        maxX = mainCamera.transform.position.x - camWidth / 2f;
        minY = mainCamera.transform.position.y - camHeight / 2f;
        maxY = mainCamera.transform.position.y - camHeight / 2f;

        Renderer renderer = GetComponent<Renderer>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentAction)
        {
            case ActionPattern.Apper:
                UpdateAppearAction();
                break;
            case ActionPattern.Attack:
                UpdateAttack();
                break;
            case ActionPattern.Ikari:
                UpdateIkari();
                break;
        }
    }
    private void Move(Vector3 moveDirection)
    {
        var pos = transform.position;
        var moveSpeed = 6;
        pos += moveDirection * moveSpeed * Time.deltaTime;
        transform.position = pos;
    }
    private void UpdateAppearAction()
    {
        Move(Vector2.left);
        if (transform.position.x <= 6f)
        {
            currentAction = ActionPattern.Attack;
        }
    }
    private bool isMoving = false; // 移動中かどうかを管理するフラグ
    private Vector3 targetPosition; // 移動の目標位置

    private void UpdateAttack()
    {
        if (!isMoving)
        {
            // 新しいランダムな目標位置を生成
            float randomX = Random.Range(-8.5f, 8.5f);
            float randomY = Random.Range(-4.5f, 4.5f);
            targetPosition = new Vector3(randomX, randomY, 0f);

            // 移動速度を設定
            float moveSpeed = 6f;

            // 移動を開始
            StartCoroutine(MoveToTarget(targetPosition, moveSpeed));
        }
        if(HP == 40)
        {
         currentAction = ActionPattern.Ikari;
        }
    }

    private IEnumerator MoveToTarget(Vector3 target, float speed)
    {
        isMoving = true;
        while (transform.position != target)
        {
            // 目標位置に向かってBossを移動
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

            yield return null;
        }
        isMoving = false;

    }
    private void UpdateIkari()
    {
        if(GetComponent<Renderer>() != null)
        {
            GetComponent<Renderer>().material.color= newColor;
        }
        if (!isMoving)
        {
            // 新しいランダムな目標位置を生成
            float randomX = Random.Range(-8.5f, 8.5f);
            float randomY = Random.Range(-4.5f, 4.5f);
            targetPosition = new Vector3(randomX, randomY, 0f);

            // 移動速度を設定
            float moveSpeed = 21f;

            // 移動を開始
            StartCoroutine(MoveToTarget(targetPosition, moveSpeed));
        }
    }
    

private void OnTriggerEnter2D(Collider2D col)
    {
        if (currentAction == ActionPattern.Apper)
        {
            return;
        }
        var LayerName = LayerMask.LayerToName(col.gameObject.layer);
        if (LayerName == "Bullet")
        {
            HP -= 1;
            animator.SetTrigger("hit");
            //Debug.Log("damaged");
            if (HP == 0)
            {
                Destroy(this.gameObject);
                SceneManager.LoadScene("Game Clear");
            }
        }
    }
}
