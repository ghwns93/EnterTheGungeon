using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletStat
{
    public GameObject bullet;
    public Vector3 v;
}

public class EnemyScript : MonoBehaviour
{
    public int hp = 3;                      //적 체력
    public float speed = 1.0f;              //적 속도
    public int roomNumber = 1;    //배치된 위치
    public float attackDistance = 10;    //공격 거리

    public GameObject bulletPrefab;         //총알
    public float shootSpeed = 5.0f;         //총알 속도

    //애니메이션 목록
    public string idleAnime = "EnemyIdle";
    public string attackAnime = "EnemyLeft";
    public string attackFinishAnime = "EnemyLeft";
    public string deadAnime = "EnemyDead";
    public string mjscAnime = "EnemyMjsc";

    //현재 & 이전 애니메이션
    string nowAnimation = "";
    string oldAnimation = "";

    //입력된 이동 값
    float axisH;
    float axisV;
    Rigidbody2D rbody;

    //활성화 여부
    bool isActive = false;
    bool inAttack = false;  //공격 상태

    BulletStat[] bulletStats;

    int count = 0;

    bool isAct = false;

    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();

        bulletStats = new BulletStat[30];
    }

    // Update is called once per frame
    void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            isActive = true;

            //플레이어와의 거리 확인
            Vector3 plpos = player.transform.position;
            float dist = Vector2.Distance(transform.position, plpos);
            
            if (!inAttack)
            {
                //플레이어가 범위 안에 있고 공격 중이 아닌 경우
                if (dist <= attackDistance)
                {
                    inAttack = true;

                    axisH = 0.0f;
                    axisV = 0.0f;
                    nowAnimation = attackAnime;
                }
                //플레이어가 인식 범위를 벗어난 경우
                else if (dist > attackDistance)
                {
                    //플레이어와의 거리를 바탕으로 각도를 구하기
                    float dx = player.transform.position.x - transform.position.x;
                    float dy = player.transform.position.y - transform.position.y;
                    float rad = Mathf.Atan2(dy, dx);
                    float angleZ = rad * Mathf.Rad2Deg;

                    nowAnimation = idleAnime;

                    //이동 벡터
                    axisH = Mathf.Cos(rad) * speed;
                    axisV = Mathf.Sin(rad) * speed;
                }
            }
        }
        else
        {
            rbody.velocity = Vector2.zero;
        }
    }

    private void FixedUpdate()
    {
        if (isActive && hp > 0)
        {
            if (inAttack)
            {
                rbody.velocity = Vector2.zero;

                GameObject player = GameObject.FindGameObjectWithTag("Player");

                if (player != null)
                {
                    if (count < 30)
                    {
                        if (!isAct)
                        {
                            isAct = true;

                            float objX = 0, objY = 0;

                            if (count < 10)
                            {
                                objX = transform.position.x - 1;
                                objY = transform.position.y + (float)(-1 + (0.2 * (count % 10)));
                            }
                            else if (count < 20)
                            {
                                objX = transform.position.x - (float)(-1 + (0.2 * (count % 10)));
                                objY = transform.position.y - (float)(1 - (0.2 * (count % 10)));
                            }
                            else
                            {
                                objX = transform.position.x + 1;
                                objY = transform.position.y + (float)(-1 + (0.2 * (count % 10)));
                            }

                            Vector3 bulletTran = new Vector3(objX, objY);

                            float dx = player.transform.position.x - objX;
                            float dy = player.transform.position.y - objY;

                            //아크탄젠트2 함수로 라디안(호도법) 구하기
                            float rad = Mathf.Atan2(dy, dx);

                            //라디안을 각도(육십분법)로 변환
                            float angle = rad * Mathf.Rad2Deg;

                            //프리팹을 이용하여 총알 오브젝트 만들기 (진행 방향으로 회전)
                            Quaternion r = Quaternion.Euler(0, 0, angle);
                            GameObject bullet = Instantiate(bulletPrefab, bulletTran, r);
                            float x = Mathf.Cos(rad);
                            float y = Mathf.Sin(rad);
                            Vector3 v = new Vector3(x, y) * shootSpeed;

                            bulletStats[count].bullet = bullet;
                            bulletStats[count].v = v;

                            count++;

                            isAct = false;
                        }
                    }
                    else
                    {
                        BulletShoot(bulletStats);
                        Array.Clear(bulletStats, 0, bulletStats.Length);
                        count = 0;
                    }
                }
            }
            else
            {
                //몬스터 이동시키기
                rbody.velocity = new Vector2(axisH, axisV);
            }

            //애니메이션 변경하기
            if (nowAnimation != oldAnimation)
            {
                oldAnimation = nowAnimation;
                Animator animator = GetComponent<Animator>();
                animator.Play(nowAnimation);
            }
        }
    }

    //공격
    void Attack()
    {
        //플레이어 정보를 가져오기
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            #region [ 단일 발사 주석 ]
            ////두 점 사시의 거리를 구하기
            //float dx = player.transform.position.x - transform.position.x;
            //float dy = player.transform.position.y - transform.position.y;

            ////아크탄젠트2 함수로 라디안(호도법) 구하기
            //float rad = Mathf.Atan2(dy, dx);

            ////라디안을 각도(육십분법)로 변환
            //float angle = rad * Mathf.Rad2Deg;

            ////프리팹을 이용하여 총알 오브젝트 만들기 (진행 방향으로 회전)
            //Quaternion r = Quaternion.Euler(0, 0, angle);
            //GameObject bullet = Instantiate(bulletPrefab, transform.position, r);
            //float x = Mathf.Cos(rad);
            //float y = Mathf.Sin(rad);
            //Vector3 v = new Vector3(x, y) * shootSpeed;

            ////총알 발사
            //Rigidbody2D rbody = bullet.GetComponent<Rigidbody2D>();
            //rbody.AddForce(v, ForceMode2D.Impulse);
            #endregion

        }

        nowAnimation = attackFinishAnime;
    }

    void AttackAnimationEnd()
    {
        inAttack = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //보스가 화살을 맞았을 경우
        if (collision.gameObject.tag == "PlayerBullet")
        {
            Debug.Log("Enter!");
            hp--; //체력 감소

            //체력이 0 이하가 되는 경우는 사망처리
            if (hp <= 0)
            {
                GetComponent<BoxCollider2D>().enabled = false;
                GetComponent<Animator>().Play(deadAnime);
                Destroy(gameObject, 1);
            }
            else
            {
                Blink();
            }
        }
    }

    private IEnumerator Blink()
    {
        SpriteRenderer playerSprite = GetComponent<SpriteRenderer>();

        Color defaultColor = playerSprite.color;

        playerSprite.color = new Color(1, 1, 1, 1);

        yield return new WaitForSeconds(0.05f);

        playerSprite.color = defaultColor;
    }

    private void BulletShoot(BulletStat[] bulletStats)
    {
        for (int i = 0; i < bulletStats.Length; i++)
        {
            if (bulletStats[i].bullet != null)
            {
                Rigidbody2D rbody = bulletStats[i].bullet.GetComponent<Rigidbody2D>();
                rbody.AddForce(bulletStats[i].v, ForceMode2D.Impulse);
            }
        }
    }
}
