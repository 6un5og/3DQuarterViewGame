using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    float hAxis;
    float vAxis;
    bool wDown;

    Vector3 moveVec;
    Animator anim;

    // Start is called before the first frame update
    void Awake()
    {
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");
        wDown = Input.GetButton("Walk");

        moveVec = new Vector3(hAxis, 0, vAxis).normalized;

        transform.position += moveVec * speed * (wDown ? 0.3f : 1f) * Time.deltaTime;

        anim.SetBool("isRun", moveVec != Vector3.zero);
        anim.SetBool("isWalk", wDown);

        transform.LookAt(transform.position + moveVec); // LookAt : 지정 된 벡터를 향해서 회전시켜주는 함수
    }
}
