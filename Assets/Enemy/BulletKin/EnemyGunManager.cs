using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGunManager : MonoBehaviour
{
    public GameObject bulletPrefab;         //총알
    public float shootSpeed = 5.0f;         //총알 속도

    public bool isActive = true;

    void Attack()
    {
        if (isActive)
        {
            //발사 위치로 사용할 게임오브젝트 가져오기
            Transform tr = transform.Find("Gate");
            GameObject gate = tr.gameObject;

            //플레이어 정보를 가져오기
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                //두 점 사시의 거리를 구하기
                float dx = player.transform.position.x - gate.transform.position.x;
                float dy = player.transform.position.y - gate.transform.position.y;

                //아크탄젠트2 함수로 라디안(호도법) 구하기
                float rad = Mathf.Atan2(dy, dx);

                //라디안을 각도(육십분법)로 변환
                float angle = rad * Mathf.Rad2Deg;

                //프리팹을 이용하여 총알 오브젝트 만들기 (진행 방향으로 회전)
                Quaternion r = Quaternion.Euler(0, 0, angle);
                GameObject bullet = Instantiate(bulletPrefab, gate.transform.position, r);
                float x = Mathf.Cos(rad);
                float y = Mathf.Sin(rad);
                Vector3 v = new Vector3(x, y) * shootSpeed;

                //총알 발사
                Rigidbody2D rbody = bullet.GetComponent<Rigidbody2D>();
                rbody.AddForce(v, ForceMode2D.Impulse);
            }
        }
    }
}
