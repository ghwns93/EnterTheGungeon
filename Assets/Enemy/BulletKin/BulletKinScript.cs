using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletKinScript : MonoBehaviour
{
    // 이동속도
    public float speed = 1.0f;

    // 애니메이션
    public string stopUpAnime = "bulletKinStopUp";
    public string stopDownAnime = "bulletKinStopDown";
    public string stopLeftAnime = "bulletKinStopLeft";

    public string walkUpAnime = "bulletKinWalkUp";
    public string walkDownAnime = "bulletKinWalkDown";
    public string walkLeftAnime = "bulletKinWalkSide";

    public string deadAnime = "bulletKinDead";
    public string fallAnime = "bulletKinFalling";

    public string AttackAnime = "EnemyGunAttack";
    public string GunIdleAnime = "EnemyGunIdle";

    // 현재 애니메이션
    string nowAnimation = "";
    // 이전 애니메이션
    string oldAnimation = "";

    bool isActive = false;
    bool isAttack = false;  //공격 상태
    bool isAct = true;
    bool isHit = false;
    bool isDead = false;

    Rigidbody2D rbody;              // RigidBody 2D 컴포넌트
    float axisH;
    float axisV;

    public int hp = 3;       // 적의 HP

    public float attackDistance = 10;    //공격 거리

    public GameObject EnemyHand;
    public GameObject EnemyGun;

    // 애니메이터
    private Animator animator;
    private Animator gunAnimator;

    UnitMove unitMove;
    public int callMoveTime = 20; //새로운 경로 탐색 시간
    private int moveTime = 0;     //이전에 부르고 경과 시간

    GameObject player;
    EnemyGunManager enemyGunManager;
    MonsterAwakeManager monsterAwakeManager;
    bool awakeOnce = true;

    public AudioClip audioDead;
    AudioSource audioSource;

    PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        // Rigidbody2D 가져오기
        rbody = GetComponent<Rigidbody2D>();

        monsterAwakeManager = GetComponent<MonsterAwakeManager>();

        // 오디오 소스 가져오기
        if (GameObject.Find("SeSoundPrefab") != null)
            audioSource = GameObject.Find("SeSoundPrefab").GetComponent<AudioSource>();
        else
            audioSource = GetComponent<AudioSource>();

        // 애니메이터 가져오기
        animator = GetComponent<Animator>();

        // 총애니메이터 가져오기
        gunAnimator = transform.GetChild(0).transform.GetChild(0).GetComponent<Animator>();

        // 총 조종
        enemyGunManager = transform.GetChild(0).transform.GetChild(0).GetComponent<EnemyGunManager>();

        unitMove = GetComponent<UnitMove>();

        player = GameObject.FindGameObjectWithTag("Player");

        // (기본) 애니메이션 설정
        oldAnimation = stopDownAnime;

        playerController = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerController.gameState == "gameover") isActive = false;

        enemyGunManager.isActive = isActive;

        if (isActive)
        {
            if (player != null)
            {
                isActive = true;

                #region [ 플레이어 방향 에 따라 애니메이션 변경 ]

                float dis = Vector2.Distance(player.transform.position, gameObject.transform.position);

                float angleZ = GetAngle(gameObject.transform.position, player.transform.position);

                GetComponent<SpriteRenderer>().flipX = false;

                // 이동 각도를 바탕으로 방향과 애니메이션을 변경한다
                if (dis > attackDistance) // 사거리 보다 플레이어와의 거리가 멀 경우 이동
                {
                    if (-45 <= angleZ && angleZ < 45)                     //오른쪽
                    {
                        nowAnimation = walkLeftAnime;
                        GetComponent<SpriteRenderer>().flipX = true;
                    }
                    else if (45 <= angleZ && angleZ < 135)                // 위
                    {
                        nowAnimation = walkUpAnime;
                    }
                    else if (135 <= angleZ && angleZ < 180 || -180 <= angleZ && angleZ < -135)     // 왼쪽
                    {
                        nowAnimation = walkLeftAnime;
                    }
                    else if (-135 <= angleZ && angleZ < -45)              // 아래
                    {
                        nowAnimation = walkDownAnime;
                    }

                    isAttack = false;

                    //플레이어와의 거리를 바탕으로 각도를 구하기
                    float dx = player.transform.position.x - transform.position.x;
                    float dy = player.transform.position.y - transform.position.y;
                    float rad = Mathf.Atan2(dy, dx);

                    //이동 벡터
                    axisH = Mathf.Cos(rad) * speed;
                    axisV = Mathf.Sin(rad) * speed;
                }
                else // 공격중에 멈춤
                {
                    if (-45 <= angleZ && angleZ < 45)                     //오른쪽
                    {
                        nowAnimation = stopLeftAnime;
                        GetComponent<SpriteRenderer>().flipX = true;
                    }
                    else if (45 <= angleZ && angleZ < 135)                // 위
                    {
                        nowAnimation = stopUpAnime;
                    }
                    else if (135 <= angleZ && angleZ < 180 || -180 <= angleZ && angleZ < -135)     // 왼쪽
                    {
                        nowAnimation = stopLeftAnime;
                    }
                    else if (-135 <= angleZ && angleZ < -45)              // 아래
                    {
                        nowAnimation = stopDownAnime;
                    }

                    isAttack = true;

                    axisH = 0.0f;
                    axisV = 0.0f;
                }

                // 애니메이션 변경
                if (nowAnimation != oldAnimation)
                {
                    oldAnimation = nowAnimation;
                    GetComponent<Animator>().Play(nowAnimation);
                }
                #endregion

                #region [ 플레이어 방향에 따라 손 위치 조정 ]

                if (EnemyHand != null)
                {
                    if (-90 <= angleZ && angleZ < 90)                     //오른쪽
                    {
                        EnemyHand.transform.position = new Vector2(transform.position.x + 0.2f, EnemyHand.transform.position.y);
                        EnemyGun.transform.position = new Vector2(EnemyHand.transform.position.x + 0.1f, EnemyGun.transform.position.y);
                        EnemyGun.transform.rotation = Quaternion.Euler(0, 0, angleZ);
                        EnemyGun.GetComponent<SpriteRenderer>().flipX = false;
                    }
                    else    // 왼쪽
                    {
                        float reverseAngle = 0.0f;

                        if (angleZ >= 90) reverseAngle = (180 - angleZ) * -1;
                        else reverseAngle = (angleZ + 180);

                        EnemyHand.transform.position = new Vector2(transform.position.x - 0.2f, EnemyHand.transform.position.y);
                        EnemyGun.transform.position = new Vector2(EnemyHand.transform.position.x - 0.1f, EnemyGun.transform.position.y);
                        EnemyGun.transform.rotation = Quaternion.Euler(0, 0, reverseAngle);
                        EnemyGun.GetComponent<SpriteRenderer>().flipX = true;
                    }
                }

                #endregion
            }
            else
            {
                rbody.velocity = Vector2.zero;
                isActive = false;
            }
        }
        else if (awakeOnce)
        {
            isActive = monsterAwakeManager.isAwake;
            if (isActive) awakeOnce = false;
        }
    }

    // (유니티 초기 설정 기준) 0.02초마다 호출되며, 1초에 총 50번 호출되는 함수
    void FixedUpdate()
    {
        ////Debug.Log(hp);

        if (isActive && hp > 0)
        {
            ////Debug.Log("Move1");
            if (!isAttack)
            {
                gunAnimator.Play(GunIdleAnime);

                ////Debug.Log("Move");
                //몬스터 이동시키기
                //rbody.velocity = new Vector2(axisH, axisV);
                if (moveTime == callMoveTime)
                {
                    unitMove.StartPathFind();
                    moveTime = 0;
                }
                else moveTime++;
            }
            else
            {
                gunAnimator.Play(AttackAnime);

                //rbody.velocity = Vector2.zero;
                unitMove.StopRoutine();
            }
        }
        else
        {
            unitMove.StopRoutine();
        }
    }

    // p1에서 p2까지의 각도를 계산한다
    float GetAngle(Vector2 p1, Vector2 p2)
    {
        float angle;

        // p1과 p2의 차를 구하기 (원점을 0으로 하기 위해)
        float dx = p2.x - p1.x;
        float dy = p2.y - p1.y;

        // 아크탄젠트 함수로 각도(라디안) 구하기
        float rad = Mathf.Atan2(dy, dx);

        // 라디안으로 변환
        angle = rad * Mathf.Rad2Deg;

        //// 축 방향에 관계없이 캐릭터가 움직이고 있을 경우 각도 변경
        //if (axisH != 0 || axisV != 0 )
        
        return angle;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {   
        if (collision.gameObject.tag == "FallDown")
        {
            isActive = false;

            // 이동 중지
            rbody.velocity = new Vector2(0, 0);

            // 추락 애니메이션 재생
            nowAnimation = fallAnime;
            animator.Play(fallAnime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerBullet")
        {
            hp--; //체력 감소

            //체력이 0 이하가 되는 경우는 사망처리
            if (hp <= 0)
            {
                isActive = false;

                audioSource.PlayOneShot(audioDead);

                rbody.velocity = Vector2.zero;
                GetComponent<BoxCollider2D>().enabled = false;
                GetComponent<Animator>().Play(deadAnime);

                Destroy(gameObject, 1);
            }
            else
            {
                Vector3 toPos = (transform.position - player.transform.position).normalized;
                rbody.AddForce(new Vector2(toPos.x * 4, toPos.y * 4), ForceMode2D.Impulse);

                if (!isHit) StartCoroutine(Blink());

                Invoke("DamageEnd", 0.1f);
            }

            Destroy(collision.gameObject);
        }
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

    // 데미지 처리 종료
    void DamageEnd()
    {
        rbody.velocity = Vector2.zero;
    }

    // 게임오버 처리
    void GameOver()
    {
        Destroy(gameObject);
    }
}

// 키 입력 관련 함수 목록
/*
    // 키보드의 특정 키 입력에 대한 검사
    bool down = Input.GetKeyDown(KeyCode.Space);
    bool press = Input.GetKey(KeyCode.Space);
    bool up = Input.GetKeyUp(KeyCode.Space);

    // 마우스 버튼 입력 및 터치 이벤트에 대한 검사
    // 0 : 마우스 왼쪽 버튼
    // 1 : 마우스 오른쪽 버튼
    // 2 : 마우스 휠 버튼
    bool down = Input.GetMouseButtonDown(0);
    bool press = Input.GetMouseButton(0);
    bool up = Input.GetMouseButtonUp(0);

    // Input Manager에서 설정한 문자열을 기반으로 하는 키 입력 검사
    bool down = Input.GetButtonDown("Jump");
    bool press = Input.GetButton("Jump");
    bool up = Input.GetButtonUp("Jump");

    // 가상의 축에 대한 키 입력 검사
    float axisH = Input.GetAxis("Horizontal");
    float axisV = Input.GetAxisRaw("Vertical");
*/