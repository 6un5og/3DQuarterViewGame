using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour
{
    public Transform target;
    public float orbitSpeed;
    Vector3 offset;

    void Start()
    {
        offset = transform.position - target.position;      // 수류탄의 위치에서 target(플레이어)위치를 뺌
    }

    void Update()
    {
        transform.position = target.position + offset;      // 보정값
        transform.RotateAround(target.position, Vector3.up, orbitSpeed * Time.deltaTime);
        offset = transform.position - target.position;      // 다시 offset업데이트 해주기
    }
}
