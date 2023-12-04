using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

    public string deadAnime1 = "PilotDead1";
    public string deadAnime2 = "PilotDead2";
    public string fallAnime = "PilotFall";
    public string pilotOpenAnime = "PilotOpenItem";

    public GameObject deadSquareUp;     //죽을때 위에서 내려오는 네모
    public GameObject deadSquareDown;
    public GameObject deadShadow;           //죽으면 밑에 그림자
    public GameObject watch1;               //시계1프리팹
    public GameObject watch2;               //시계2프리팹
    public GameObject bulletBombPrefab;     //총알 터지는 애니메이션가진 프리팹
    public GameObject deadBookOpenPrefab;   //책 펼쳐지는 애니메이션가진 프리팹

    public Canvas bookCanvasPrefab;               //책 안의 버튼있는 캔버스 프리팹

    GameObject deadSquareUpObj;         //여러함수에서 쓸수있게 선언해둠
    GameObject deadSquareDownObj;
    GameObject deadShadowObj;
    GameObject watch1Obj;
    GameObject watch2Obj;
    GameObject bulletBombObj;
    GameObject deadBookOpenObj;

    Canvas bookCanvasObj;

    string nowAnimation = "";       // 현재 애니메이션
    string oldAnimation = "";       // 이전 애니메이션       

    // 애니메이터
    private Animator animator;

    float axisH = 0.0f;             // 가로 입력 (-1.0 ~ 1.0)
    float axisV = 0.0f;             // 세로 입력 (-1.0 ~ 1.0)
    public float angleZ = -90.0f;   // 회전 각도
    float angleDodge = -90.0f;      // 구르기
    int gunNumber;                  // GunController의 총종류 변수 가져다 저장할곳

    Rigidbody2D rbody;              // RigidBody 2D 컴포넌트
    bool isMoving = false;          // 이동 중
    public bool isDodging = false;  // 회피 중
    public bool inlobby = false;    // 로비에 있는지


    public static int hp ;          // 플레이어의 HP
    public static int maxHp;        // maxHp

    public string gameState;        // 게임 상태 (playing, gameover, falling)
    bool inDamage = false;          // 피격 상태
    bool isInvincible;              // 무적일때 피안까지게

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

        maxHp = 6;      //잠시 1로해둠
        // HP 불러오기
        hp = maxHp;
    }

    // Update is called once per frame
    void Update()
    {
        if (bookCanvasObj)      // 죽은후 책이 생겼을 때
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene("MainStage");
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.LoadScene("Lobby");
            }
        }

        // gameover 일때는 이 조건문 밑으로는아무 것도 하지 않음
        if (gameState == "gameover")
        {
            return;
        }

        if (isMoving == false)
        {
            axisH = Input.GetAxisRaw("Horizontal"); // 좌우
            axisV = Input.GetAxisRaw("Vertical");   // 상하
        }

        // 총종류 받아오기
        gunNumber = GetComponent<GunController>().gunNumber;

        // 마우스 위치 받아오기
        mousePosition = Input.mousePosition;

        // 마우스 위치를 월드 좌표로 변환
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        // 마우스 입력을 통하여 이동 각도 구하기
        Vector2 characterPt = transform.position;         
        
        // 키 입력을 통하여 이동 각도 구하기
        Vector2 fromPt = transform.position;
        Vector2 toPt = new Vector2(fromPt.x + axisH, fromPt.y + axisV);

        // 로비에 있을때
        if (inlobby)
        {
            angleZ = GetAngleInLobby(characterPt, toPt);
            // 왼쪽으로 이동할 때 X축 플립
            if (axisH < 0)
            {
                // SpriteRenderer의 flipX를 사용하는 경우
                GetComponent<SpriteRenderer>().flipX = true;
            }
            else if (axisH > 0) // 오른쪽으로 이동할 때 X축 플립 해제
            {
                // SpriteRenderer의 flipX를 사용하는 경우
                GetComponent<SpriteRenderer>().flipX = false;
            }
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(false);
            }
        }
        else
        {
            angleZ = GetAngle(characterPt, mousePosition);

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
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(true);
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
            else if (angleZ > 30 && angleZ < 90)         // 오른위
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

        // 8방향 벡터 구하기
        angleDodge = GetAngleDodge(fromPt, toPt);
        Vector2 dodgePos = new Vector2(Mathf.Cos(angleDodge), Mathf.Sin(angleDodge));
        
        // 키입력을 받은 상태에서 마우스 우클릭을 했을 때만 구르기
        if ((axisH != 0 || axisV != 0) && Input.GetButton("Fire2"))
        {
            // 오른쪽, 오른쪽 아래로 구르기
            if (angleZ > -60 && angleZ < 15)
            {
                // 회피중
                isDodging = true;                
                // 입력된 방향으로 구르기
                rbody.AddForce(dodgePos, ForceMode2D.Impulse);
                // 애니메이션 설정
                nowAnimation = dodgeRightDownAnime;
                animator.Play("PilotDodgeRightDown");
            }
            // 오른쪽 위로 구르기
            else if (angleZ > 30 && angleZ < 60)
            {
                isDodging = true;
                rbody.AddForce(dodgePos, ForceMode2D.Impulse);
                nowAnimation = dodgeRightUpAnime;
                animator.Play("PilotDodgeRightUp");
            }
            // 위로 구르기
            else if (angleZ > 75 && angleZ < 105)
            {
                isDodging = true;
                rbody.AddForce(dodgePos, ForceMode2D.Impulse);
                nowAnimation = dodgeUpAnime;
                animator.Play("PilotDodgeUp");
            }
            // 왼쪽 위로 구르기
            else if (angleZ > 120 && angleZ < 150)
            {
                isDodging = true;
                rbody.AddForce(dodgePos, ForceMode2D.Impulse);
                nowAnimation = dodgeLeftUpAnime;
                animator.Play("PilotDodgeRightUp");
            }
            // 왼쪽, 왼쪽 아래 구르기
            else if (angleZ > 165 && angleZ < 240 || angleZ < -105 && angleZ > -200)
            {
                isDodging = true;
                rbody.AddForce(dodgePos, ForceMode2D.Impulse);
                nowAnimation = dodgeLeftDownAnime;
                animator.Play("PilotDodgeRightDown");
            }
            // 아래로 구르기
            else if (angleZ < -80 && angleZ > -100)
            {
                isDodging = true;
                rbody.AddForce(dodgePos, ForceMode2D.Impulse);
                nowAnimation = dodgeDownAnime;
                animator.Play("PilotDodgeDown");
            }
        }

        // 애니메이션 변경
        if (nowAnimation != oldAnimation)
        {
            oldAnimation = nowAnimation;
            GetComponent<Animator>().Play(nowAnimation);
        }

        
    }

    // (유니티 초기 설정 기준) 0.02초마다 호출되며, 1초에 총 50번 호출되는 함수
    void FixedUpdate()
    {
        // gameover 일때는 아무 것도 하지 않음
        if (gameState == "gameover")
        {
            return;
        }

        // 공격받는 도중에 캐릭터를 점멸시킨다
        if (inDamage)
        {
            isInvincible = true;
            // Time.time : 게임 시작부터 현재까지의 경과시간 (초단위)
            // Sin 함수에 연속적으로 증가하는 값을 대입하면 0~1~0~-1~0... 순으로 변화
            float value = Mathf.Sin(Time.time * 30);
            
            if (value > 0)
            {
                gameObject.GetComponent<SpriteRenderer>().enabled = true;
                GameObject[] handObjects = GameObject.FindGameObjectsWithTag("Hand");   //"Hand"태그있는 오브젝트들 찾기
                foreach (GameObject handObject in handObjects)      //배열들 하나마다
                {
                    // handObject의 SpriteRenderer를 가져옴
                    SpriteRenderer handSpriteRenderer = handObject.GetComponent<SpriteRenderer>();

                    if (handSpriteRenderer != null)
                    {
                        Color handColor = handSpriteRenderer.color;
                        handColor.a = 1;    // 불투명하게
                        handSpriteRenderer.color = handColor;   //투명도 적용
                    }
                }
                // 총에 따라 불투명도 바꾸기
                if (gunNumber == 1)
                    transform.Find("PilotGun(Clone)").GetComponent<SpriteRenderer>().enabled = true;
                if (gunNumber == 2)
                    transform.Find("RedGun(Clone)").GetComponent<SpriteRenderer>().enabled = true;
            }
            else
            {
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
                GameObject[] handObjects = GameObject.FindGameObjectsWithTag("Hand");   //"Hand"태그있는 오브젝트들 찾기
                foreach (GameObject handObject in handObjects)
                {
                    // handObject의 SpriteRenderer를 가져옴
                    SpriteRenderer handSpriteRenderer = handObject.GetComponent<SpriteRenderer>();

                    if (handSpriteRenderer != null)
                    {
                        Color handColor = handSpriteRenderer.color;
                        handColor.a = 0;    // 투명하게
                        handSpriteRenderer.color = handColor;
                    }
                }
                if(gunNumber ==1)
                    transform.Find("PilotGun(Clone)").GetComponent<SpriteRenderer>().enabled = false;
                if (gunNumber == 2)
                    transform.Find("RedGun(Clone)").GetComponent<SpriteRenderer>().enabled = false;
            }
            
        }

        // 이동 속도를 더하여 캐릭터를 움직여준다
        rbody.velocity = new Vector2(axisH, axisV) * speed;        
    }

    public void DodgeUpAnimationEnd()
    {
        nowAnimation = stopUpAnime;
        animator.Play("PilotStopUp");
        isDodging = false;
    }
    public void DodgeRightUpAnimationEnd()
    {
        nowAnimation = stopRightUpAnime;
        animator.Play("PilotStopRightUp");
        isDodging = false;
    }
    public void DodgeRightDownAnimationEnd()
    {
        nowAnimation = stopRightDownAnime;
        animator.Play("PilotStopRightDown");
        isDodging = false;
    }
    public void DodgeDownAnimationEnd()
    {
        nowAnimation = stopDownAnime;
        animator.Play("PilotStopDown");
        isDodging = false;
    }
    

    // p1에서 p2까지의 각도를 계산한다
    public float GetAngle(Vector2 p1, Vector2 p2)
    {
        float angle;

        // p1과 p2의 차를 구하기 (원점을 0으로 하기 위해)
        float dx = p2.x - p1.x;
        float dy = p2.y - p1.y;

        // 아크탄젠트 함수로 각도(라디안) 구하기
        float rad = Mathf.Atan2(dy, dx);

        // 라디안으로 변환
        angle = rad * Mathf.Rad2Deg;

        return angle;
    }

    float GetAngleInLobby(Vector2 p1, Vector2 p2)
    {
        float angle;

        // 축 방향에 관계없이 캐릭터가 움직이고 있을 경우 각도 변경
        if (axisH != 0 || axisV != 0)
        {
            // p1과 p2의 차를 구하기 (원점을 0으로 하기 위해)
            float dx = p2.x - p1.x;
            float dy = p2.y - p1.y;

            // 아크탄젠트 함수로 각도(라디안) 구하기
            float rad = Mathf.Atan2(dy, dx);

            // 라디안으로 변환
            angle = rad * Mathf.Rad2Deg;
        }
        else
        {
            // 캐릭터가 정지 중이면 각도 유지
            angle = angleZ;
        }
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


    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Enemy와 물리적으로 충돌 발생
        if ((collision.gameObject.tag == "Enemy"|| collision.gameObject.tag == "EnemyBullet") && !isDodging)
        {
            // 데미지 계산
            GetDamage(collision.gameObject);

            // 적 총알에 맞았을경우
            if (collision.gameObject.tag == "EnemyBullet")
            {
                if(gameState!="gameover")
                {
                    bulletBombObj = Instantiate(bulletBombPrefab,
                    collision.gameObject.transform.position, collision.gameObject.transform.rotation);   //총알 터지는 효과                
                    Destroy(bulletBombObj, 1f);
                }                
                Destroy(collision.gameObject);      //  총알 삭제
            }
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

            // 로비가 아닐경우 데미지 계산
            if(!inlobby)
            {
                hp--;
                if (hp == 0)
                {
                    // 체력이 없으면 게임오버
                    GameOver();
                }
            }
                

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

        if (!isDodging!&& isInvincible == false)
        {
            // 무적상태가 아닐경우에만 hp 까짐
            hp--;
            if (hp > 0)
            {               
                // 현재 공격받고 있음
                inDamage = true;    //inDamage == true 일경우 FixedUpdate에서 if(inDamage)조건문 실행
                Invoke("DamageEnd", 1.5f);
            }
            else if (hp == 0)
            {
                // 체력이 없으면 게임오버
                GameOver();     
            }
        }
    }

    // 데미지 처리 종료
    void DamageEnd()
    {
        isInvincible = false;
        inDamage = false;

        // 플레이어 투명하면 해제
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        GameObject[] handObjects = GameObject.FindGameObjectsWithTag("Hand");
        foreach (GameObject handObject in handObjects)
        {
            // handObject의 SpriteRenderer를 가져옴
            SpriteRenderer handSpriteRenderer = handObject.GetComponent<SpriteRenderer>();
                        
            if (handSpriteRenderer != null)
            {
                Color handColor = handSpriteRenderer.color;
                handColor.a = 1;    // 손 보이게
                handSpriteRenderer.color = handColor;
            }
        }
        // 총에따라 불투명도 바꾸기
        if (gunNumber == 1)
            transform.Find("PilotGun(Clone)").GetComponent<SpriteRenderer>().enabled = true;
        if (gunNumber == 2)
            transform.Find("RedGun(Clone)").GetComponent<SpriteRenderer>().enabled = true;
    }

    //게임오버 처리
    void GameOver()
    {
        gameState = "gameover";
        // 게임오버 연출

        // 이동 중지
        rbody.velocity = new Vector2(0, 0);
        // 애니메이션 변경        
        animator.Play(deadAnime1);
        Invoke("AfterDead", 3.0f);

        foreach (Transform child in transform)  // 플레이어 자식 오브젝트들 모두 안보이게
        {
            child.gameObject.SetActive(false);  
        }        
        Destroy(GameObject.Find("PilotOtherHand(Clone)"));  // 플레이어 다른손 제거
        Destroy(transform.Find("PilotShadow").gameObject);  // 플레이어 그림자 제거
        // 플레이어 밑에 큰 그림자 생성
        deadShadowObj = Instantiate(deadShadow, transform.position + new Vector3(0, 0f, 0), transform.rotation);

        // 위에서 내려오는 검정박스
        if(!deadSquareUpObj)
            deadSquareUpObj = Instantiate(deadSquareUp,transform.position + new Vector3(0, +8f, 0), transform.rotation);
        deadSquareUpObj.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -3.5f), ForceMode2D.Impulse);
        // 아래에서 올라오는 검정박스
        if (!deadSquareDownObj)
            deadSquareDownObj = Instantiate(deadSquareDown, transform.position+new Vector3(0,-8f,0), transform.rotation);
        deadSquareDownObj.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, +3.5f),ForceMode2D.Impulse);
        Invoke("StopSquare", 1f);
    }
    void StopSquare()
    {
        //올라오거나 내려오는거 정지
        deadSquareUpObj.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        deadSquareDownObj.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
    }
    void AfterDead()
    {
        //시계나오기
        if (!watch1Obj)
            watch1Obj = Instantiate(watch1, transform.position, transform.rotation);
        Invoke("WatchShot", 1.5f);
    }

    //이벤트함수
    void WatchShot()
    {
        Destroy(watch1Obj);
        watch2Obj = Instantiate(watch2, transform.position , transform.rotation); 
        animator.Play(deadAnime2);      //시계총 맞고 쓰러지는 애니메이션
        Destroy(watch2Obj, 1.0f);
        Invoke("DeadBookOpen", 2.0f);
    }

    void DeadBookOpen()
    {
        if (!deadBookOpenObj)
            deadBookOpenObj = Instantiate(deadBookOpenPrefab, transform.position, transform.rotation);
        bookCanvasObj = Instantiate(bookCanvasPrefab, transform.position, transform.rotation);
    }
}

// 키 입력 관련 함수 목록
/*
    죽었을때 검정내려오는거 위치설정

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