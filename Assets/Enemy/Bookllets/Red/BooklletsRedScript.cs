using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BooklletsRedScript : MonoBehaviour
{
    public int hp = 3;                      //적 체력
    public float speed = 1.0f;              //적 속도
    public int roomNumber = 1;    //배치된 위치
    public float attackDistance = 10;    //공격 거리

    //애니메이션 목록
    public string idleAnime = "EnemyIdle";
    public string attackAnime = "EnemyLeft";
    public string attackingAnime = "EnemyAttaking";
    public string attackFinishAnime = "EnemyLeft";
    public string deadAnime = "EnemyDead";
    public string mjscAnime = "EnemyMjsc";

    public GameObject bulletPrefab;         //총알
    public float shootSpeed = 5.0f;         //총알 속도

    //현재 & 이전 애니메이션
    string nowAnimation = "";
    string oldAnimation = "";

    //입력된 이동 값
    float axisH;
    float axisV;
    Rigidbody2D rbody;

    //활성화 여부
    bool isActive = false;
    bool isAttack = false;  //공격 상태
    bool isAct = true;
    bool isHit = false;

    List<GameObject> bulletStats; //총알을 표현후 한번에 쏘기 위해 List에 저장
    int count = 0; //총알 갯수 카운트

    int bolletShape = 0;
    private const int NshapeMaxBullet = 30;
    private const int RshapeMaxBullet = 22;
    private const int PshapeMaxBullet = 32;

    //R형태 총알 날라가는 간격
    int rDelay = 0;

    bool RShoot = false;
    MonsterAwakeManager monsterAwakeManager;
    bool awakeOnce = true;

    public AudioClip audioCharge;
    public AudioClip audioShot;
    public AudioClip audioRShot;
    public AudioClip audioDead;
    AudioSource audioSource;

    GameObject player;
    PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        bulletStats = new List<GameObject>();
        monsterAwakeManager = GetComponent<MonsterAwakeManager>();
        audioSource = GetComponent<AudioSource>();

        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerController.gameState == "gameover")
        {
            isActive = false;
            Animator animator = GetComponent<Animator>();
            animator.Play(idleAnime);
        }

        if (isActive)
        {
            if (player != null)
            {
                //플레이어와의 거리 확인
                Vector3 plpos = player.transform.position;
                float dist = Vector2.Distance(transform.position, plpos);

                if (!isAttack)
                {
                    //플레이어가 범위 안에 있고 공격 중이 아닌 경우
                    if (dist <= attackDistance)
                    {
                        isAttack = true;

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
        else if(awakeOnce)
        {
            isActive = monsterAwakeManager.isAwake;
            if (isActive) awakeOnce = false;
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
            if (!isAttack)
            {
                //몬스터 이동시키기
                rbody.velocity = new Vector2(axisH, axisV);
            }
            else
            {
                if (isAttack)
                {
                    rbody.velocity = Vector2.zero;

                    GameObject player = GameObject.FindGameObjectWithTag("Player");

                    if (player != null)
                    {
                        if (!isAct)
                        {
                            switch (bolletShape)
                            {
                                case 0:
                                    #region [ N 형태 발사 ]
                                    if (count < NshapeMaxBullet)
                                    {
                                        //Debug.Log("Charge" + count);
                                        isAct = true;
                                        float objX = 0, objY = 0;

                                        if (count < 10)
                                        {
                                            objX = transform.position.x - 1;
                                            objY = transform.position.y + (float)(-1 + (0.2 * (count % 10)));
                                        }
                                        else if (count < 20)
                                        {
                                            objX = transform.position.x + (float)(-1 + (0.2 * (count % 10)));
                                            objY = transform.position.y + (float)(1 - (0.2 * (count % 10)));
                                        }
                                        else
                                        {
                                            objX = transform.position.x + 1;
                                            objY = transform.position.y + (float)(-1 + (0.2 * (count % 10)));
                                        }

                                        Vector3 bulletTran = new Vector3(objX, objY);

                                        //프리팹을 이용하여 총알 오브젝트 만들기 (진행 방향으로 회전)
                                        Quaternion r = Quaternion.Euler(0, 0, 0);
                                        GameObject bullet = Instantiate(bulletPrefab, bulletTran, r);

                                        if (bullet != null) bulletStats.Add(bullet);

                                        count++;

                                        isAct = false;
                                    }
                                    #endregion
                                    break;
                                case 1:
                                    #region [ R 형태 발사 ]
                                    if (count < RshapeMaxBullet)
                                    {
                                        //Debug.Log("Charge" + count);
                                        isAct = true;
                                        float objX = 0, objY = 0;

                                        float minX = -0.5f, maxX = 0.5f;
                                        float minY = -0.75f, maxY = 0.75f;

                                        if (count < 5)
                                        {
                                            objX = transform.position.x + minX;
                                            objY = transform.position.y + (float)(minY + (((maxY - minY) / 5.0f) * count));
                                        }
                                        else if(count < 8)
                                        {
                                            objX = transform.position.x + (float)(minX + ((minX + 1.25f) / 3) * (count - 5));
                                            objY = transform.position.y + maxY;
                                        }
                                        else if (count < 14)
                                        {
                                            objX = transform.position.x - 0.6f + (Mathf.Cos((0 + (float)Math.Truncate(Math.Abs((count - 10.5f)))) * 0.3f));
                                            objY = transform.position.y + maxY - (maxY * ((float)(count - 8) / 6));

                                            //Debug.Log(objY);
                                        }
                                        else if (count < 17)
                                        {
                                            objX = transform.position.x - (float)(0.2f * (count - 14));
                                            objY = transform.position.y;
                                        }
                                        else
                                        {
                                            objX = transform.position.x + (float)((0.5f / 6.0f) * (count - 17));
                                            objY = transform.position.y - (float)((1.0f / 6.0f) * (count - 17));
                                        }

                                        Vector3 bulletTran = new Vector3(objX, objY);

                                        //프리팹을 이용하여 총알 오브젝트 만들기 (진행 방향으로 회전)
                                        Quaternion r = Quaternion.Euler(0, 0, 0);
                                        GameObject bullet = Instantiate(bulletPrefab, bulletTran, r);

                                        if (bullet != null) bulletStats.Add(bullet);

                                        count++;

                                        isAct = false;
                                    }
                                    #endregion
                                    break;
                                case 2:
                                    #region [ Φ 형태 발사 ]
                                    if (count < PshapeMaxBullet)
                                    {
                                        //Debug.Log("Charge" + count);
                                        isAct = true;
                                        float objX = 0, objY = 0;

                                        float minY = -1.2f, maxY = 1.2f;

                                        if (count < 22)
                                        {
                                            float rad = (float)((count * 360.0f / 22.0f) * Math.PI / 180.0f);

                                            objX = transform.position.x + (float)Math.Sin(rad);
                                            objY = transform.position.y + (float)Math.Cos(rad);
                                        }
                                        else
                                        {
                                            float newCount = count - 22;
                                            float remainBulletCnt = PshapeMaxBullet - 22 - 1;

                                            //Debug.Log(maxY - ((maxY - minY) * (newCount / remainBulletCnt)));

                                            objX = transform.position.x;
                                            objY = transform.position.y + (float)maxY - ((maxY - minY) * (newCount / remainBulletCnt));
                                        }

                                        Vector3 bulletTran = new Vector3(objX, objY);

                                        //프리팹을 이용하여 총알 오브젝트 만들기 (진행 방향으로 회전)
                                        Quaternion r = Quaternion.Euler(0, 0, 0);
                                        GameObject bullet = Instantiate(bulletPrefab, bulletTran, r);

                                        if (bullet != null) bulletStats.Add(bullet);

                                        count++;

                                        isAct = false;
                                    }
                                    #endregion
                                    break;
                            }
                        }
                        else if(RShoot)
                        {
                            if (rDelay == 0)
                            {
                                if (bulletStats.Count > 0)
                                {
                                    try
                                    {
                                        var bs = bulletStats[0];

                                        float dx = player.transform.position.x - bs.transform.position.x;
                                        float dy = player.transform.position.y - bs.transform.position.y;

                                        //아크탄젠트2 함수로 라디안(호도법) 구하기
                                        float rad = Mathf.Atan2(dy, dx);

                                        //라디안을 각도(육십분법)로 변환
                                        float angle = rad * Mathf.Rad2Deg;

                                        bs.transform.rotation = Quaternion.Euler(0, 0, angle);

                                        float x = Mathf.Cos(rad);
                                        float y = Mathf.Sin(rad);
                                        Vector3 v = new Vector3(x, y) * shootSpeed;

                                        //Debug.Log("Shoot2");

                                        Rigidbody2D rbody = bs.GetComponent<Rigidbody2D>();
                                        rbody.AddForce(v, ForceMode2D.Impulse);

                                        //Debug.Log("Shoot3");
                                        bulletStats.Remove(bs);

                                        audioSource.PlayOneShot(audioRShot);
                                    }
                                    catch (Exception e) 
                                    {
                                        //Debug.Log(e);
                                        bulletStats.Clear();
                                    }
                                }

                                if (bulletStats.Count == 0)
                                {
                                    count = 0;
                                    RShoot = false;
                                    nowAnimation = attackFinishAnime;
                                }
                            }
                            else if(rDelay == 4) rDelay = -1;

                            rDelay++;
                        }
                    }
                }
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

            bolletShape = UnityEngine.Random.Range(0, 3);
            //bolletShape = 2;

            isAct = false;
        }
    }

    void AttackAnimationEnd()
    {
        isAct = true;
        isAttack = false;
    }

    void InBulletCharge()
    {
        if (bolletShape == 1)
            audioSource.PlayOneShot(audioCharge);
        else
            audioSource.PlayOneShot(audioShot);


        nowAnimation = attackingAnime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerBullet")
        {
            hp--; //체력 감소
            Destroy(collision.gameObject);

            //체력이 0 이하가 되는 경우는 사망처리
            if (hp <= 0)
            {
                audioSource.PlayOneShot(audioDead);

                rbody.velocity = Vector2.zero;
                GetComponent<BoxCollider2D>().enabled = false;
                GetComponent<Animator>().Play(deadAnime);

                foreach (var bs in bulletStats)
                {
                    Destroy(bs);
                }

                Destroy(gameObject, 1);
            }
            else
            {
                if(!isHit)StartCoroutine(Blink());
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

    private IEnumerator Blink()
    {
        isHit = true;

        //Debug.Log("blink");
        SpriteRenderer playerSprite = GetComponent<SpriteRenderer>();

        Color defaultColor = new Color(1, 1, 1, 1);

        playerSprite.color = new Color(1, 0, 0, 1);

        yield return new WaitForSeconds(0.2f);

        playerSprite.color = defaultColor;

        isHit = false;
    }

    private void BulletShoot()
    {
        if (bolletShape == 0)
        {
            #region [ N 모양 일제히 플레이어에게 발사 ]
            if (count == NshapeMaxBullet)
            {
                //Debug.Log("Shoot!");
                isAct = true;

                GameObject player = GameObject.FindGameObjectWithTag("Player");

                foreach (var bs in bulletStats)
                {
                    if (bs != null)
                    {
                        float dx = player.transform.position.x - bs.transform.position.x;
                        float dy = player.transform.position.y - bs.transform.position.y;

                        //아크탄젠트2 함수로 라디안(호도법) 구하기
                        float rad = Mathf.Atan2(dy, dx);

                        //라디안을 각도(육십분법)로 변환
                        float angle = rad * Mathf.Rad2Deg;

                        bs.transform.rotation = Quaternion.Euler(0, 0, angle);

                        float x = Mathf.Cos(rad);
                        float y = Mathf.Sin(rad);
                        Vector3 v = new Vector3(x, y) * shootSpeed;

                        Rigidbody2D rbody = bs.GetComponent<Rigidbody2D>();
                        rbody.AddForce(v, ForceMode2D.Impulse);
                    }
                }

                bulletStats.Clear();
                count = 0;

                nowAnimation = attackFinishAnime;
            }
            #endregion
        }
        else if(bolletShape == 1)
        {
            #region [ R 모양 한개씩 발사 ]
            if (count == RshapeMaxBullet)
            {
                //Debug.Log("Shoot!");
                RShoot = true;

                isAct = true;
            }
            #endregion
        }
        else if (bolletShape == 2)
        {
            #region [ Φ 모양 일제히 플레이어에게 발사 ]
            if (count == PshapeMaxBullet)
            {
                //Debug.Log("Shoot!");
                isAct = true;

                foreach (var bs in bulletStats)
                {
                    if (bs != null)
                    {
                        float dx = transform.position.x - bs.transform.position.x;
                        float dy = transform.position.y - bs.transform.position.y;

                        //아크탄젠트2 함수로 라디안(호도법) 구하기
                        float rad = Mathf.Atan2(dy, dx);

                        //라디안을 각도(육십분법)로 변환
                        float angle = rad * Mathf.Rad2Deg;

                        bs.transform.rotation = Quaternion.Euler(0, 0, angle);

                        float x = Mathf.Cos(rad) * -1;
                        float y = Mathf.Sin(rad) * -1;
                        Vector3 v = new Vector3(x, y) * shootSpeed;

                        Rigidbody2D rbody = bs.GetComponent<Rigidbody2D>();
                        rbody.AddForce(v, ForceMode2D.Impulse);
                    }
                }

                bulletStats.Clear();
                count = 0;

                nowAnimation = attackFinishAnime;
            }
            #endregion
        }
    }
}