using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossController : MonoBehaviour
{
    // 이동속도
    public float speed = 1.0f;

    public int maxhp = 10;   // 적의 최대 HP
    private int nowhp;       // 적의 현재 HP

    public float attackDistance = 10;    //공격 거리
    public float moveDistance = 10;    //추격 거리

    bool isActive = false;
    bool isAttack = false;  //공격 상태
    bool isMove = false;  //추격 상태
    bool isAct = true;
    bool isHit = false;
    bool isDead = false;

    Rigidbody2D rbody;              // RigidBody 2D 컴포넌트
    float axisH;
    float axisV;

    UnitMove unitMove;
    public int callMoveTime = 20; //새로운 경로 탐색 시간
    private int moveTime = 0;     //이전에 부르고 경과 시간

    GameObject player;

    BossChairManager chairManager;
    MonsterAwakeManager monsterAwakeManager;
    bool awakeOnce = true;

    //ui 슬라이더
    private Slider slider;

    public AudioClip audioDead;
    AudioSource audioSource;

    PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        nowhp = maxhp;

        // Rigidbody2D 가져오기
        rbody = GetComponent<Rigidbody2D>();
        monsterAwakeManager = GetComponent<MonsterAwakeManager>();
        audioSource = GetComponent<AudioSource>();

        chairManager = transform.Find("BossMoveObject").transform.Find("BossChair").GetComponent<BossChairManager>();
        slider = transform.Find("Canvas").transform.Find("Slider").GetComponent<Slider>();

        unitMove = GetComponent<UnitMove>();

        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerController.gameState == "gameover") isActive = false;

        if (isActive)
        {
            if (player != null)
            {
                isActive = true;

                float dis = Vector2.Distance(player.transform.position, gameObject.transform.position);

                // 사거리 보다 플레이어와의 거리가 멀 경우 공격 중지
                if (dis > attackDistance)
                {
                    isAttack = false;
                }
                else 
                {
                    isAttack = true;
                }

                // 추격거리보다 플레이어가 멀 경우 추격
                if(dis > moveDistance)
                {
                    //플레이어와의 거리를 바탕으로 각도를 구하기
                    float dx = player.transform.position.x - transform.position.x;
                    float dy = player.transform.position.y - transform.position.y;
                    float rad = Mathf.Atan2(dy, dx);

                    //이동 벡터
                    axisH = Mathf.Cos(rad) * speed;
                    axisV = Mathf.Sin(rad) * speed;

                    isMove = true;
                }
                else
                {
                    axisH = 0.0f;
                    axisV = 0.0f;

                    isMove = false;
                }
            }
            else
            {
                rbody.velocity = Vector2.zero;
                isActive = false;
            }
        }
        else if(awakeOnce)
        {
            isActive = monsterAwakeManager.isAwake;
            if(isActive) awakeOnce = false;
        }
    }

    // (유니티 초기 설정 기준) 0.02초마다 호출되며, 1초에 총 50번 호출되는 함수
    void FixedUpdate()
    {
        if (isActive && nowhp > 0)
        {
            if (isMove)
            {
                if (moveTime == callMoveTime)
                {
                    unitMove.StartPathFind();
                    moveTime = 0;
                }
                else moveTime++;
            }
            else
            {
                unitMove.StopRoutine();
            }

            chairManager.isAttack = isAttack;
        }
        else
        {
            chairManager.isAttack = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "FallDown")
        {
            isActive = false;

            // 이동 중지
            rbody.velocity = new Vector2(0, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerBullet")
        {
            nowhp--; //체력 감소

            slider.value = ((float)nowhp / (float)maxhp);

            //체력이 0 이하가 되는 경우는 사망처리
            if (nowhp <= 0)
            {
                isActive = false;

                audioSource.PlayOneShot(audioDead);

                SpriteRenderer chairSprite = transform.Find("BossMoveObject").transform.Find("BossChair").GetComponent<SpriteRenderer>();
                GameObject bodySprite = transform.Find("BossMoveObject").transform.Find("BossChair").transform.Find("BossBody").gameObject;

                Color DefaultColor = new Color(1, 1, 1, 1);

                chairSprite.color = DefaultColor;
                bodySprite.SetActive(true);

                chairManager.isDead = true;

                rbody.velocity = Vector2.zero;
                unitMove.StopRoutine();
                GetComponent<CapsuleCollider2D>().enabled = false;

                Destroy(gameObject, 4.0f);
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

        SpriteRenderer chairSprite = transform.Find("BossMoveObject").transform.Find("BossChair").GetComponent<SpriteRenderer>();
        SpriteRenderer bodySprite = transform.Find("BossMoveObject").transform.Find("BossChair").transform.Find("BossBody").GetComponent<SpriteRenderer>();

        Color chairDefaultColor = new Color(1, 1, 1, 1);
        Color bodyDefaultColor = bodySprite.color;

        chairSprite.color = new Color(1, 0, 0, 1);
        bodySprite.color = new Color(1, 0, 0, bodyDefaultColor.a);

        yield return new WaitForSeconds(0.2f);

        chairSprite.color = chairDefaultColor;
        bodySprite.color = bodyDefaultColor;

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