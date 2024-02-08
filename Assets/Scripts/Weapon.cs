using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public enum Type { Melee, Range };
    public Type type;
    public int damage;
    public float rate;
    public int maxAmmo;
    public int curAmmo;

    public BoxCollider meleeArea;
    public TrailRenderer trailEffect;
    public Transform bulletPos;
    public GameObject bullet;
    public Transform bulletCasePos;
    public GameObject bulletCase;

    public void Use()
    {
        if (type == Type.Melee)
        {
            StopCoroutine("Swing");     // 실행하던 코루틴 멈추고
            StartCoroutine("Swing");    // 새로 시작하기
        }
        else if (type == Type.Range && curAmmo > 0)
        {
            curAmmo--;
            StartCoroutine("Shot");
        }
    }

    IEnumerator Swing()
    {
        yield return new WaitForSeconds(0.1f);
        meleeArea.enabled = true;
        trailEffect.enabled = true;

        yield return new WaitForSeconds(0.3f);
        meleeArea.enabled = false;

        yield return new WaitForSeconds(0.3f);
        trailEffect.enabled = false;
    }

    IEnumerator Shot()
    {
        // 총알 발사
        GameObject instantBullet = Instantiate(bullet, bulletPos.position, bulletPos.rotation);
        Rigidbody bulletRigid = instantBullet.GetComponent<Rigidbody>();
        bulletRigid.velocity = bulletPos.forward * 50;

        yield return null;
        // 탄피 배출
        GameObject instantCase = Instantiate(bulletCase, bulletCasePos.position, bulletCasePos.rotation);
        Rigidbody CaseRigid = instantCase.GetComponent<Rigidbody>();
        Vector3 caseVec = bulletCasePos.forward * Random.Range(-3, -2) + Vector3.up * Random.Range(2, 3);       // 탄피가 벡터 반대방향으로 나가기 때문에 forward의 마이너스 값 곱하기
        CaseRigid.AddForce(caseVec, ForceMode.Impulse);
        CaseRigid.AddTorque(Vector3.up * 10, ForceMode.Impulse);        // AddTorque 회전을 더해주는 함수
    }

    // Use() 메인루틴 -> Swing() 서브루틴 -> Use() 메인루틴 (교차실행)
    // Use() 메인루틴 + Swing() 코루틴 (동시실행)

}
