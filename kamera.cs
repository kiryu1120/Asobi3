using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // フォローする対象（プレイヤーキャラクターなど）
    public float smoothing = 5f; // カメラの追従スムーズネス
    public float minY = -1f;
    Vector3 offset; // カメラとターゲットの距離

    void Start()
    {
        offset = transform.position - target.position;
    }

    void FixedUpdate()
    {
        Vector3 targetCamPos = target.position + offset;
        targetCamPos.y = Mathf.Max(targetCamPos.y, minY);
        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
    }
}