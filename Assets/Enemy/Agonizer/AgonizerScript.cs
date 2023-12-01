using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AgonizerScript : MonoBehaviour
{
    public int hp = 3;                      //적 체력
    public float speed = 1.0f;              //적 속도
    public int roomNumber = 1;              //배치된 위치
    public float attackDistance = 10;       //공격 거리
    public float shootWatingTime = 3.0f;    //총알 발사 간격
    public float minAngle = 45.0f;          //최소 각도 (왼쪽 0 , 아랫쪽 90 , 오른쪽 180, 윗쪽 270)
    public float maxAngle = 135.0f;         //최대 각도 (왼쪽 0 , 아랫쪽 90 , 오른쪽 180, 윗쪽 270)
    public int MaxBullet = 10;              //1루틴당 총알 갯수
    public bool bulletTwoWay = true;         //총알 왔다 갔다 설정.

    //애니메이션 목록
    public string idleAnime = "EnemyIdle";
    public string attackAnime = "EnemyLeft";
    public string attackingAnime = "EnemyAttaking";
    public string attackFinishAnime = "EnemyLeft";
    public string deadAnime = "EnemyDead";

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
    
    //R형태 총알 날라가는 간격
    float rDelay = 0;

    bool RShoot = false;

    MonsterAwakeManager monsterAwake;
    bool awakeOnce = true;

    public AudioClip audioShot;
    public AudioClip audioDeath;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        bulletStats = new List<GameObject>();
        monsterAwake = GetComponent<MonsterAwakeManager>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

        if (isActive)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                isActive = true;

                //플레이어와의 거리 확인
                Vector3 plpos = player.transform.position;
                float dist = Vector2.Distance(transform.position, plpos);

                //Debug.Log("dist :" + dist);
                if (dist <= attackDistance)
                {
                    if (!isAttack)
                    {
                        //플레이어가 범위 안에 있고 공격 중이 아닌 경우
                        isAttack = true;
                        nowAnimation = attackAnime;
                    }
                }
                //플레이어가 인식 범위를 벗어난 경우
                else if (dist > attackDistance)
                {
                    if (nowAnimation == attackingAnime || nowAnimation == attackAnime) nowAnimation = attackFinishAnime;
                }
            }
            else
            {
                rbody.velocity = Vector2.zero;
            }
        }
        else if (awakeOnce)
        {
            Debug.Log(monsterAwake.isAwake);
            isActive = monsterAwake.isAwake;
            if (isActive) awakeOnce = false;
        }
    }

    private void FixedUpdate()
    {
        if (isActive && hp > 0)
        {
            if (isAttack)
            {
                rbody.velocity = Vector2.zero;

                GameObject player = GameObject.FindGameObjectWithTag("Player");

                if (player != null)
                {
                    if (!isAct)
                    {
                        Debug.Log("rDelay : " + rDelay);

                        if (count < MaxBullet + 1 && 0 <= count && rDelay == 0)
                        {
                            isAct = true;
                            BulletShoot();
                            rDelay++;
                            isAct = false;
                        }
                        else if(count >= MaxBullet + 1)
                        {
                            //isAct = true;
                            //nowAnimation = attackFinishAnime;
                            count = 0;
                            rDelay++;
                        }
                        else
                        {
                            if (rDelay >= shootWatingTime) rDelay = 0;
                            else rDelay++;
                        }
                    }
                }
            }

            //애니메이션 변경하기
            if (nowAnimation != oldAnimation)
            {
                //Debug.Log("nowAnime : " + nowAnimation);
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
            Debug.Log("AttackIn!");
            isAct = false;
        }
    }

    void AttackAnimationEnd()
    {
        isAct = true;
        isAttack = false;

        nowAnimation = idleAnime;
    }

    void InBulletCharge()
    {
        nowAnimation = attackingAnime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //보스가 화살을 맞았을 경우
        if (collision.gameObject.tag == "PlayerBullet")
        {
            hp--; //체력 감소
            Destroy(collision.gameObject);

            //체력이 0 이하가 되는 경우는 사망처리
            if (hp <= 0)
            {
                audioSource.PlayOneShot(audioDeath);

                rbody.velocity = Vector2.zero;
                GetComponent<CapsuleCollider2D>().enabled = false;
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

        SpriteRenderer playerSprite = GetComponent<SpriteRenderer>();

        Color defaultColor = new Color(1, 1, 1, 1);

        playerSprite.color = new Color(1, 0, 0, 1);

        yield return new WaitForSeconds(0.2f);

        playerSprite.color = defaultColor;

        isHit = false;
    }

    private void BulletShoot()
    {
        float objX = 0, objY = 0;

        float rad = 0.0f;
        float halfCount = MaxBullet / 2;
        float limit = 0.0f;

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

                rad =  limit * Mathf.Deg2Rad;
                objX = transform.position.x + ((float)Math.Cos(rad));
                objY = transform.position.y - ((float)Math.Sin(rad));
            }
        }
        else
        {
            limit = (float)(minAngle + (((maxAngle - minAngle) / MaxBullet) * count));

            limit = limit >= maxAngle ? maxAngle : limit;

            rad =  limit * Mathf.Deg2Rad;
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
        audioSource.PlayOneShot(audioShot);

        rDelay++;
        count++;
    }
}