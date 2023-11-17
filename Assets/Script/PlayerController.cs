using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // 이동속도
    public float speed = 3.0f;

    // 애니메이션
    public string stopUpAnime = "PilotStopUp";
    public string stopDownAnime = "PilotStopDown";
    public string stopLeftUpAnime = "PilotStopRightUp";
    public string stopLeftDownAnime = "PilotStopRightDown";
    public string stopRightUpAnime = "PilotStopRightUp";
    public string stopRightDownAnime = "PilotStopRightDown";

    public string walkUpAnime = "PilotWalkUp";
    public string walkDownAnime = "PilotWalkDown";
    public string walkLeftUpAnime = "PilotWalkRightUp";
    public string walkLeftDownAnime = "PilotWalkRightDown";
    public string walkRightUpAnime = "PilotWalkRightUp";
    public string walkRightDownAnime = "PilotWalkRightDown";

    public string dodgeUpAnime = "PilotDodgeUp";
    public string dodgeDownAnime = "PilotDodgeDown";
    public string dodgeLeftUpAnime = "PilotDodgeRightUp";
    public string dodgeLeftDownAnime = "PilotDodgeRightDown";
    public string dodgeRightUpAnime = "PilotDodgeRightUp";
    public string dodgeRightDownAnime = "PilotDodgeRightDown";

    public string deadAnime = "PlayerDead";
    public string fallAnime = "PilotFall";
    public string pilotOpenAnime = "PilotOpenItem";

    // 현재 애니메이션
    string nowAnimation = "";
    // 이전 애니메이션
    string oldAnimation = "";

    // 애니메이터
    private Animator animator;

    float axisH = 0.0f;             // 가로 입력 (-1.0 ~ 1.0)
    float axisV = 0.0f;             // 세로 입력 (-1.0 ~ 1.0)
    public float angleZ = -90.0f;   // 회전
    float angleDodge = -90.0f;      // 구르기

    Rigidbody2D rbody;              // RigidBody 2D 컴포넌트
    bool isMoving = false;          // 이동 중
    public bool isDodging = false;  // 회피 중

    public bool inlobby = false;    // 로비에 있는지

    public static int hp = 3;       // 플레이어의 HP
    public static string gameState; // 게임 상태
    bool inDamage = false;          // 피격 상태

    Vector2 beforePos = new Vector2(0, 0);

    public Vector3 mousePosition;

    // Start is called before the first frame update
    void Start()
    {
        // Rigidbody2D 가져오기
        rbody = GetComponent<Rigidbody2D>();

        // (기본) 애니메이션 설정
        oldAnimation = stopDownAnime;

        // 애니메이터 가져오기
        animator = GetComponent<Animator>();
                
        // 게임 상태 지정
        gameState = "playing";

        // HP 불러오기
        hp = PlayerPrefs.GetInt("PlayerHP");
    }

    // Update is called once per frame
    void Update()
    {
        // 게임 중이 아니거나 공격받고 있을 경우에는 아무 것도 하지 않음
        if (gameState != "playing" || inDamage)
        {
            return;
        }

        if (isMoving == false)
        {
            axisH = Input.GetAxisRaw("Horizontal"); // 좌우
            axisV = Input.GetAxisRaw("Vertical");   // 상하
        }

        // 마우스 위치 받아오기
        mousePosition = Input.mousePosition;

        // 마우스 위치를 월드 좌표로 변환
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        // 마우스 입력을 통하여 이동 각도 구하기
        Vector2 characterPt = transform.position;
        Vector2 mousePt = new Vector2(characterPt.x + mousePosition.x, characterPt.y + mousePosition.y);                      
        // 키 입력을 통하여 이동 각도 구하기
        Vector2 fromPt = transform.position;
        Vector2 toPt = new Vector2(fromPt.x + axisH, fromPt.y + axisV);
        
        if(inlobby)
        {
            angleZ = GetAngle(fromPt, toPt);
            // 왼쪽으로 이동할 때 X축 플립
            if (axisH < 0)
            {
                // SpriteRenderer의 flipX를 사용하는 경우
                GetComponent<SpriteRenderer>().flipX = true;
            }
            else if (axisH >= 0) // 오른쪽으로 이동할 때 X축 플립 해제
            {
                // SpriteRenderer의 flipX를 사용하는 경우
                GetComponent<SpriteRenderer>().flipX = false;
            }
        }
        else
        {
            angleZ = GetAngle(characterPt, mousePt);

            // 왼쪽으로 이동할 때 X축 플립
            if (mousePosition.x < 0)
            {
                // SpriteRenderer의 flipX를 사용하는 경우
                GetComponent<SpriteRenderer>().flipX = true;
            }
            else if (mousePosition.x >= 0) // 오른쪽으로 이동할 때 X축 플립 해제
            {
                // SpriteRenderer의 flipX를 사용하는 경우
                GetComponent<SpriteRenderer>().flipX = false;
            }
        }

        // 이동 각도를 바탕으로 방향과 애니메이션을 변경한다
        if ( (axisH != 0 || axisV != 0) && !isDodging) // 키 입력이 있는 경우에만 Walk 애니메이션을 재생
        {
            if (angleZ > -60 && angleZ < 15)                     //오른, 오른아래
            {
                nowAnimation = walkRightDownAnime;
            }
            else if (angleZ > 30 && angleZ < 60)                 // 오른위
            {
                nowAnimation = walkRightUpAnime;
            }
            else if (angleZ > 75 && angleZ < 105)                // 위
            {
                nowAnimation = walkUpAnime;
            }
            else if (angleZ > 90 && angleZ < 180)                // 왼위
            {
                nowAnimation = walkLeftUpAnime;
            }
            else if (angleZ > 145 && angleZ < 240 || angleZ < -105 && angleZ > -200)   // 왼, 왼밑
            {
                nowAnimation = walkLeftDownAnime;
            }
            else if (angleZ < -80 && angleZ > -100)              // 아래
            {
                nowAnimation = walkDownAnime;
            }
        }
        else if(axisH == 0.0f && axisV == 0.0f && !isDodging)// 키 입력이 없는 경우에는 Stop 애니메이션을 재생
        {            
            if (angleZ > -60 && angleZ < 15)     //오른, 오른아래
            {
                nowAnimation = stopRightDownAnime;
            }
            else if (angleZ > 0 && angleZ < 90)         // 오른위
            {
                nowAnimation = stopRightUpAnime;
            }
            else if (angleZ > 75 && angleZ < 105)       // 위
            {
                nowAnimation = stopUpAnime;
            }
            else if (angleZ > 90 && angleZ < 180)       // 왼위
            {
                nowAnimation = stopLeftUpAnime;
            }
            else if (angleZ > 145 && angleZ < 240 || angleZ < -105 && angleZ > -200)  // 왼, 왼밑
            {
                nowAnimation = stopLeftDownAnime;
            }
            else if (angleZ < -80 && angleZ > -100)    // 아래
            {
                nowAnimation = stopDownAnime;
            }
        }              

        // 애니메이션 변경
        if (nowAnimation != oldAnimation)
        {
            oldAnimation = nowAnimation;
            GetComponent<Animator>().Play(nowAnimation);
        }

        // 8방향 벡터 구하기
        angleDodge = GetAngleDodge(fromPt, toPt);
        Vector2 dodgePos = new Vector2(Mathf.Cos(angleDodge), Mathf.Sin(angleDodge));
        
        // 키입력을 받은 상태에서 마우스 우클릭을 했을 때만 구르기
        if ((axisH != 0 || axisV != 0) && Input.GetButtonDown("Fire2"))
        {
            // 오른쪽, 오른쪽 아래로 구르기
            if (angleZ > -60 && angleZ < 15)
            {
                // 회피중
                gameState = "dodging";
                // 입력된 방향으로 구르기
                rbody.AddForce(dodgePos, ForceMode2D.Impulse);
                // 애니메이션 설정
                nowAnimation = dodgeRightDownAnime;
                animator.Play("PilotDodgeRightDown");
            }
            // 오른쪽 위로 구르기
            else if (angleZ > 30 && angleZ < 60)               
            {
                // 회피중
                gameState = "dodging";
                // 입력한 방향으로 구르기
                rbody.AddForce(dodgePos, ForceMode2D.Impulse);
                // 애니메이션 설정
                nowAnimation = dodgeRightUpAnime;
                animator.Play("PilotDodgeRightUp");
            }
            // 위로 구르기
            else if (angleZ > 75 && angleZ < 105)             
            {
                // 회피중
                gameState = "dodging";
                // 입력한 방향으로 구르기
                rbody.AddForce(dodgePos, ForceMode2D.Impulse);
                // 애니메이션 설정
                nowAnimation = dodgeUpAnime;
                animator.Play("PilotDodgeUp");
            }
            // 왼쪽 위로 구르기
            else if (angleZ > 120 && angleZ < 150)             
            {
                // 회피중
                gameState = "dodging";
                // 입력한 방향으로 구르기
                rbody.AddForce(dodgePos, ForceMode2D.Impulse);
                // 애니메이션 설정
                nowAnimation = dodgeLeftUpAnime;
                animator.Play("PilotDodgeRightUp");
            }
            // 왼쪽, 왼쪽 아래 구르기
            else if (angleZ > 165 && angleZ < 240 || angleZ < -105 && angleZ > -200) 
            {
                // 회피중
                gameState = "dodging";
                // 입력한 방향으로 구르기
                rbody.AddForce(dodgePos, ForceMode2D.Impulse);
                // 애니메이션 설정
                nowAnimation = dodgeLeftDownAnime;
                animator.Play("PilotDodgeRightDown");
            }
            // 아래로 구르기
            else if (angleZ < -80 && angleZ > -100)         
            {
                // 회피중
                gameState = "dodging";
                // 입력한 방향으로 구르기
                rbody.AddForce(dodgePos, ForceMode2D.Impulse);
                // 애니메이션 설정
                nowAnimation = dodgeDownAnime;
                animator.Play("PilotDodgeDown");
            }
        }
    }

    // (유니티 초기 설정 기준) 0.02초마다 호출되며, 1초에 총 50번 호출되는 함수
    void FixedUpdate()
    {
        // 게임 중이 아니면 아무 것도 하지 않음
        if (gameState != "playing")
        {
            return;
        }

        // 공격받는 도중에 캐릭터를 점멸시킨다
        if (inDamage)
        {
            // Time.time : 게임 시작부터 현재까지의 경과시간 (초단위)
            // Sin 함수에 연속적으로 증가하는 값을 대입하면 0~1~0~-1~0... 순으로 변화
            float value = Mathf.Sin(Time.time * 50);
            Debug.Log(value);
            if (value > 0)
            {
                gameObject.GetComponent<SpriteRenderer>().enabled = true;
            }
            else
            {
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
            }
            return;
        }

        // 이동 속도를 더하여 캐릭터를 움직여준다
        rbody.velocity = new Vector2(axisH, axisV) * speed;
        
    }

    public void DodgeUpAnimationEnd()
    {
        gameState = "playing";
        nowAnimation = stopUpAnime;
        animator.Play("PilotStopUp");
        isDodging = false;
    }
    public void DodgeRightUpAnimationEnd()
    {
        gameState = "playing";
        nowAnimation = stopRightUpAnime;
        animator.Play("PilotStopRightUp");
        isDodging = false;
    }
    public void DodgeRightDownAnimationEnd()
    {
        gameState = "playing";
        nowAnimation = stopRightDownAnime;
        animator.Play("PilotStopRightDown");
        isDodging = false;
    }
    public void DodgeDownAnimationEnd()
    {
        gameState = "playing";
        nowAnimation = stopDownAnime;
        animator.Play("PilotStopDown");
        isDodging = false;
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

    // p1에서 p2까지의 각도를 계산한다
    float GetAngleDodge(Vector2 p1, Vector2 p2)
    {
        float rad;

        // 축 방향에 관계없이 캐릭터가 움직이고 있을 경우 각도 변경
        if (axisH != 0 || axisV != 0)
        {
            // p1과 p2의 차를 구하기 (원점을 0으로 하기 위해)
            float dx = p2.x - p1.x;
            float dy = p2.y - p1.y;

            // 아크탄젠트 함수로 각도(라디안) 구하기
            rad = Mathf.Atan2(dy, dx);
        }
        else
        {
            // 캐릭터가 정지 중이면 각도 유지
            rad = angleDodge;
        }
        return rad;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Enemy와 물리적으로 충돌 발생
        if (collision.gameObject.tag == "Enemy")
        {
            // 데미지 계산
            GetDamage(collision.gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        // FallDown 직전 캐릭터 재생성 위치 저장
        if (collision.gameObject.tag == "BeforePos")
        {
            // 재생성할 위치 저장하기
            beforePos = gameObject.transform.position;
        }
        // FallDown 애니메이션
        if (collision.gameObject.tag == "FallDown" && !isDodging)
        {
            // 게임 상태를 추락중으로 변경
            gameState = "falling";
            // 이동 중지
            rbody.velocity = new Vector2(0, 0);
            // 추락 애니메이션 재생
            animator.Play("PilotFall");

            // 데미지 계산
            //GetDamage(collision.gameObject);

            // 추락 애니메이션이 재생된 후에 떨어지기 전 위치로 이동하기 위해 1초 대기
            Invoke("BeforePos", 1.0f);
        }
    }

    // 추락 애니메이션 종료
    void BeforePos()
    {
        // 플레이어의 위치를 추락 전 저장된 위치로 이동
        gameObject.transform.position = beforePos;

        // 게임상태를 다시 게임중으로 변경
        gameState = "playing";

        nowAnimation = stopDownAnime;
        // 애니메이션을 다시 재생
        animator.Play("PilotStopDown");
    }

    // 데미지 계산
    void GetDamage(GameObject enemy)
    {
        if (gameState == "playing")
        {
            hp--;   // HP감소
            PlayerPrefs.SetInt("PlayerHP", hp); // 현재 HP 갱신
            if (hp > 0)
            {
                // 이동 중지
                rbody.velocity = new Vector2(0, 0);
                // 히트백 (적이 공격한 방향의 반대로)
                Vector3 toPos = (transform.position - enemy.transform.position).normalized;
                rbody.AddForce(new Vector2(toPos.x * 4, toPos.y * 4), ForceMode2D.Impulse);
                // 현재 공격받고 있음
                inDamage = true;
                Invoke("DamageEnd", 0.25f);
            }
            else
            {
                // 체력이 없으면 게임오버
                GameOver();
            }
        }
    }

    // 데미지 처리 종료
    void DamageEnd()
    {
        inDamage = false;
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
    }

    // 게임오버 처리
    void GameOver()
    {
        Debug.Log("게임오버!");
        gameState = "gameover";

        // 게임오버 연출
        // 충돌 판정 비활성화
        GetComponent<CircleCollider2D>().enabled = false;
        // 이동 중지
        rbody.velocity = new Vector2(0, 0);
        // 중력을 통해 플레이어를 위로 살짝 띄우기
        rbody.gravityScale = 1;
        rbody.AddForce(new Vector2(0, 5), ForceMode2D.Impulse);
        // 애니메이션 변경
        GetComponent<Animator>().Play(deadAnime);
        // 1초 후 캐릭터 삭제
        Destroy(gameObject, 1.0f);

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