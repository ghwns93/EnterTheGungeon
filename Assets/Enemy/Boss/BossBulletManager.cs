using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBulletManager : MonoBehaviour
{
    internal enum PattenNumber
    {
        SPIN = 0,
        SHOTGUN = 1,
        FIRE = 2
    }

    //총알 관련 옵션
    public float shootSpeed = 5.0f;         //총알 속도

    public bool isAttack = false;                   //공격 가능한 상태인지 확인

    //회전 공격 옵션
    public float shootWatingTime = 3.0f;    //총알 발사 간격
    public GameObject bulletPrefab;
    public float minAngle = 45.0f;          //최소 각도 (왼쪽 0 , 아랫쪽 90 , 오른쪽 180, 윗쪽 270)
    public float maxAngle = 135.0f;         //최대 각도 (왼쪽 0 , 아랫쪽 90 , 오른쪽 180, 윗쪽 270)
    public float angleCount = 2;            //각 각도 사이에 몇개의 총알을 넣을것 인지 설정
    public int MaxBullet = 10;              //1루틴당 총알 갯수
    public float spinAttackTime = 4.0f;     //총 회전 시간
    public bool bulletTwoWay = true;        //총알 왔다 갔다 설정.

    //샷건 발사
    public GameObject shotgunBulletPrefab;  //샷건 총알 프리팹
    public int shotgunCount = 3;            //샷건 발사 개수
    public int shotgunFireCount = 2;        //샷건 발포 횟수
    private int realFireCount = 0;
    public float shotAngle = 90.0f;         //샷건 탄 퍼짐 각도
    public float shotAttackTime = 0.5f;     //샷건 발사 속도

    //샷건 발사
    public GameObject fireBulletPrefab;     //폭죽 총알 프리팹

    //이하 private
    List<GameObject> bulletStats;           //총알을 표현후 한번에 쏘기 위해 List에 저장
    int count = 0;                          //총알 갯수 카운트
    int routineCount = 0;                   //몇번째 발사중인지 카운트

    float plusAngle = 0.0f;
    float attackTime = 0.0f;

    float rDelay = 0;
    internal PattenNumber patten;

    private void Awake()
    {
        rDelay = shootWatingTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (isAttack)
        {
            if (patten == PattenNumber.SPIN)
            {
                #region [ 스핀 공격 ]
                if (attackTime == 0.0f)
                {
                    attackTime = spinAttackTime;
                }

                if (rDelay >= shootWatingTime)
                {
                    if (patten == PattenNumber.SPIN)
                    {
                        if (attackTime > 0)
                        {
                            SpinAttack();
                        }
                        else
                        {
                            isAttack = false;
                            SpinAttackFinish();
                            attackTime = spinAttackTime;
                        }
                    }

                    rDelay = 0;
                }
                else
                {
                    rDelay += Time.deltaTime;
                    attackTime -= Time.deltaTime;
                }
                #endregion
            }
            else if (patten == PattenNumber.SHOTGUN)
            {
                #region [ 샷건 공격 ]

                if (realFireCount == shotgunFireCount) realFireCount = 0;

                GameObject player = GameObject.FindGameObjectWithTag("Player");

                if (rDelay >= shotAttackTime)
                {
                    if (player != null)
                    {
                        //두 점 사시의 거리를 구하기
                        float dx = player.transform.position.x - transform.position.x;
                        float dy = player.transform.position.y - transform.position.y;

                        //아크탄젠트2 함수로 라디안(호도법) 구하기
                        float rad = Mathf.Atan2(dy, dx);

                        //라디안을 각도(육십분법)로 변환
                        float angle = rad * Mathf.Rad2Deg;

                        ShotgunAttack(angle);

                        if (++realFireCount == shotgunFireCount) isAttack = false;
                    }

                    rDelay = 0;
                }
                else
                {
                    rDelay += Time.deltaTime;
                }
                #endregion
            }
            else if (patten == PattenNumber.FIRE)
            {
                #region [ 폭죽 공격 ]
                Transform tr = transform.Find("FireBulletPos");
                GameObject gate = tr.gameObject;

                //프리팹을 이용하여 총알 오브젝트 만들기 (진행 방향으로 회전)
                Quaternion r = Quaternion.Euler(0, 0, 0);
                GameObject bullet = Instantiate(fireBulletPrefab, gate.transform.position, r);

                isAttack = false;
                #endregion
            }
        }
    }

    private void SpinAttack()
    {
        #region [ 회전 공격 ]

        float objX = 0, objY = 0;

        float rad = 0.0f;
        float halfCount = MaxBullet / 2;
        float limit = 0.0f;

        count = 0;

        while (count <= MaxBullet + 1)
        {
            if (bulletTwoWay)
            {
                if (halfCount < count)
                {
                    limit = (float)(maxAngle - (((maxAngle - minAngle) / halfCount) * ((count - halfCount) - 0.5f)));

                    limit = limit <= minAngle ? minAngle : limit;

                    rad = limit * Mathf.Deg2Rad;
                    objX = transform.position.x + ((float)Math.Cos(rad));
                    objY = transform.position.y - ((float)Math.Sin(rad));
                }
                else
                {
                    limit = (float)(minAngle + (((maxAngle - minAngle) / halfCount) * count));

                    limit = limit >= maxAngle ? maxAngle : limit;

                    rad = limit * Mathf.Deg2Rad;
                    objX = transform.position.x + ((float)Math.Cos(rad));
                    objY = transform.position.y - ((float)Math.Sin(rad));
                }
            }
            else
            {
                limit = (float)(minAngle + (((maxAngle - minAngle) / MaxBullet) * count));

                limit = limit >= maxAngle ? maxAngle : limit;

                rad = (limit + plusAngle + (attackTime * 20)) * Mathf.Deg2Rad;
                objX = transform.position.x + ((float)Math.Cos(rad));
                objY = transform.position.y - ((float)Math.Sin(rad));
            }

            //라디안을 각도(육십분법)로 변환
            float angle = rad * Mathf.Rad2Deg;

            Vector3 v = new Vector3(objX, objY);

            Quaternion r = Quaternion.Euler(0, 0, angle);
            GameObject bullet = Instantiate(bulletPrefab, v, r);

            float dx = transform.position.x - bullet.transform.position.x;
            float dy = transform.position.y - bullet.transform.position.y;

            Vector2 v2 = new Vector3(dx * -1, dy * -1) * shootSpeed;

            Rigidbody2D rbody = bullet.GetComponent<Rigidbody2D>();
            rbody.AddForce(v2, ForceMode2D.Impulse);

            count++;
        }

        if (routineCount < angleCount - 1)
        {
            plusAngle = (((maxAngle - minAngle) / MaxBullet) / angleCount) * (++routineCount);
        }
        else
        {
            plusAngle = 0;
            routineCount = 0;
        }

        #endregion
    }

    private void SpinAttackFinish()
    {
        #region [ 회전 공격 마무리 ]

        float objX = 0, objY = 0;

        float FinishMaxBullet = (MaxBullet * angleCount) * 2;

        float rad = 0.0f;
        float halfCount = FinishMaxBullet / 2;
        float limit = 0.0f;

        count = 0;

        while (count <= FinishMaxBullet + 1)
        {
            limit = (float)(minAngle + (((maxAngle - minAngle) / FinishMaxBullet) * count));

            limit = limit >= maxAngle ? maxAngle : limit;

            rad = limit * Mathf.Deg2Rad;
            objX = transform.position.x + ((float)Math.Cos(rad));
            objY = transform.position.y - ((float)Math.Sin(rad));

            //라디안을 각도(육십분법)로 변환
            float angle = rad * Mathf.Rad2Deg;

            Vector3 v = new Vector3(objX, objY);

            Quaternion r = Quaternion.Euler(0, 0, angle);
            GameObject bullet = Instantiate(bulletPrefab, v, r);

            float dx = transform.position.x - bullet.transform.position.x;
            float dy = transform.position.y - bullet.transform.position.y;

            Vector2 v2 = new Vector3(dx * -1, dy * -1) * shootSpeed;

            Rigidbody2D rbody = bullet.GetComponent<Rigidbody2D>();
            rbody.AddForce(v2, ForceMode2D.Impulse);

            count++;
        }

        #endregion
    }

    private void ShotgunAttack(float angle)
    {
        #region [ 샷건 공격 ]

        float minAngle = angle + (shotAngle / 2);

        //프리팹을 이용하여 총알 오브젝트 만들기 (진행 방향으로 회전)
        for (int i = 0; i < shotgunCount; i++) 
        {
            float plusAngle = (shotAngle / (shotgunCount - 1)) * i;
            Quaternion r = Quaternion.Euler(0, 0, minAngle - plusAngle);
            GameObject bullet = Instantiate(shotgunBulletPrefab, transform.position, r);

            float localRad = (minAngle - plusAngle) * Mathf.Deg2Rad;

            float x = Mathf.Cos(localRad);
            float y = Mathf.Sin(localRad);
            Vector3 v = new Vector3(x, y) * shootSpeed;

            //총알 발사
            Rigidbody2D rbody = bullet.GetComponent<Rigidbody2D>();
            rbody.AddForce(v, ForceMode2D.Impulse);
        }

        #endregion
    }
}
