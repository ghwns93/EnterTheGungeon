using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class GunController : MonoBehaviour
{
    // 총 애니메이터
    private Animator gunAnimator;

    // 애니메이션
    public string pilotGunHold = "PilotGunHold";
    public string pilotGunReady = "PilotGunReady";
    public string pilotGunFire = "PilotGunFire";
    public string pilotGunReturn = "PilotGunReturn";
    public string pilotGunReload = "PilotGunReload";

    string gameState;           //PlayerController 에서 가져다 저장할곳

    public float shootSpeed;    //화살 속도
    public float shootDelay;    //발사 간격 0.5로 인스펙터에 되있음

    public int pilotGunBulletCount;     //PilotGunBullet총알 개수
    public int redGunBulletCount;     //총알 개수

    public int gunNumber;       //총 종류 식별 숫자
    public int gunMaxCount;     //가지고 있는 총의 개수    

    public GameObject pilotGunPrefab;       //파일럿 총 프리팹
    public GameObject redGunPrefab;         //빨간 총 프리팹
    public GameObject pilotGunBulletPrefab; //파일럿총알 프리팹
    public GameObject redGunBulletPrefab;   //빨간총알 프리팹

    public GameObject otherHandPrefab;  //총든손 반대편 손 프리팹
    public GameObject reloadBack;       //장전시 긴 막대
    public GameObject reloadBar;        //장전시 작은 막대
    public GameObject reloadText;       //재장전 프리팹
    public GameObject blankBullet;      //공포탄 프리팹

    public GameObject pilotgunFireEffectPrefab; //

    GameObject gunObj;                  //총오브젝트
    GameObject gunGateObj;              //포구
    GameObject LeftHandObj;             //왼손
    GameObject RightHandObj;            //오른손
    GameObject ReloadBack;              //R누르면 만들어지는 긴 막대
    GameObject ReloadBar;               //R누르면 만들어지는 작은 막대
    GameObject ReloadText;              //재장전 문구(3d legacy text)
    GameObject bulletObj;               //총알 조건문안에 안들어가서 선언해둠
    GameObject pilotgunFireEffectObj;   //pilotGun 총쏠때 이팩트

    public bool canAttack;              //공격 딜레이 할때 사용
    private bool isReloading;           //장전하는중 장전 안되게
    bool isLeftHand;        //왼손있는지
    bool isRightHand;       //오른손있는지
    bool inlobby;           //PlayerController의 변수 가져와서 저장할 변수
    bool isReloadText;       //재장전 문구 생성한개만할려고

    TextMesh textMesh;
    Color textColor;

    PlayerController playerController;  //PlayerController의 함수,변수 사용하기위해 선언해둠

    Vector3 mousePosition;

    // Start is called before the first frame update
    void Start()
    {
        InstantiateGun();

        gunNumber = 1;
        gunMaxCount = 2;
        pilotGunBulletCount = 8;
        redGunBulletCount = 20;

        inlobby = GetComponent<PlayerController>().inlobby;            

        canAttack = true;
        isReloading = false;
        isReloadText = false;

        //Cursor.lockState = CursorLockMode.Confined;     // 커서 화면밖으로 못나가게함
    }

    // Update is called once per frame
    void Update()
    {
        gameState = GetComponent<PlayerController>().gameState;
        // gameover 일때는 아무 것도 하지 않음
        if (gameState == "gameover")
        {
            gunObj.GetComponent<SpriteRenderer>().enabled=false;
            return;
        }
        // 마우스 휠 스크롤 값을 얻기
        float scrollValue = Input.GetAxis("Mouse ScrollWheel");

        // 위로 스크롤 
        if (scrollValue > 0.0f)
        {            
            gunNumber++;        // 총번호 바꾸기
            if (gunNumber > gunMaxCount)
                gunNumber = 1;
            Destroy(gunObj);    //들고있던총 없애기
            InstantiateGun();   //총 생성
            Destroy(ReloadText);    //재장전 문구있으면 없애기
            isReloadText = false;
        }
        else if (scrollValue < 0.0f)    // 아래쪽으로 스크롤
        {
            gunNumber--;
            if (gunNumber <= 0)
                gunNumber = gunMaxCount;
            Destroy(gunObj);
            InstantiateGun();
            Destroy(ReloadText);    //재장전 문구있으면 없애기
            isReloadText = false;
        }

        SpriteRenderer gunSpr = gunObj.GetComponent<SpriteRenderer>();          //총의 SpriteRenderer
        Transform childTransform = gunObj.transform.Find("PilotHand");          //자식에 접근하기위해
        SpriteRenderer handSpr = childTransform.GetComponent<SpriteRenderer>(); //손의 SpriteRenderer
        PlayerController plmv = GetComponent<PlayerController>();               //player의 SpriteRenderer
        
        // playerController의 마우스 포지션 변수 가져오기
        mousePosition = FindObjectOfType<PlayerController>().mousePosition; 

        // 총,손,총구 위치,회전
        if (mousePosition .x> transform.position.x) // 마우스가 캐릭터 오른쪽에 있을때
        {
            gunObj.GetComponent<SpriteRenderer>().flipY = false;                                    //반전취소
            gunObj.transform.rotation = Quaternion.Euler(0, 0, plmv.angleZ - 90);                   //마우스에 따라 총회전          
            gunObj.transform.position = transform.position + new Vector3(0.35f, -0.2f, 0);          //총위치 캐릭터 오른쪽으로 new Vector3(0.2f, -0.15f, 0);

            gunGateObj.transform.position = gunObj.transform.position +
                new Vector3(0.5f * Mathf.Cos(plmv.angleZ * Mathf.Deg2Rad), 0.5f * Mathf.Sin(plmv.angleZ * Mathf.Deg2Rad)+0.2f);//총구 위치
            if (plmv.angleZ < -45 && plmv.angleZ > -135)
                gunGateObj.transform.position = gunObj.transform.position +
                new Vector3(0.5f * Mathf.Cos(plmv.angleZ * Mathf.Deg2Rad) + 0.15f, 0.5f * Mathf.Sin(plmv.angleZ * Mathf.Deg2Rad) );//밑에 볼때 총구위치 조정
            else if (plmv.angleZ < 120 && plmv.angleZ > 60)
                gunGateObj.transform.position = gunObj.transform.position +
                new Vector3(0.5f * Mathf.Cos(plmv.angleZ * Mathf.Deg2Rad) + 0.15f, 0.5f * Mathf.Sin(plmv.angleZ * Mathf.Deg2Rad) + 0.3f);//위에 볼때 총구위치 조정

            childTransform.position = transform.position + new Vector3(0.35f, -0.2f, 0);            //손위치
            childTransform.rotation = Quaternion.Euler(0, 0, 0);                                    //손회전 x                        
            if(!isLeftHand)
            {
                isLeftHand = true;
                LeftHandObj = Instantiate(otherHandPrefab,
                    transform.position + new Vector3(-0.35f, -0.2f, 0), Quaternion.Euler(0, 0, 0)); //왼손 생성                
            }
            LeftHandObj.transform.position = transform.position + new Vector3(-0.35f, -0.2f, 0);    //왼손 캐릭터에 붙어다니게
            Destroy(RightHandObj);  //총에 손이 있어서 원래 있던 손 삭제
            isRightHand = false;
            if (GetComponent<PlayerController>().inlobby)   //로비에서 손2개
            {
                if (!isRightHand)
                {
                    isRightHand = true;
                    RightHandObj = Instantiate(otherHandPrefab,
                        transform.position + new Vector3(0.35f, -0.2f, 0), Quaternion.Euler(0, 0, 0));  //오른손 생성
                }
                RightHandObj.transform.position = transform.position + new Vector3(+0.2f, -0.15f, 0);   //오른손 캐릭터에 붙어다니게
            }
        }
        else // 마우스가 캐릭터 왼쪽에 있을때
        {
            gunObj.GetComponent<SpriteRenderer>().flipY = true;                                     //반전           
            gunObj.transform.rotation = Quaternion.Euler(0, 0, plmv.angleZ + 90);                     //마우스에 따라 총회전
            gunObj.transform.position = transform.position + new Vector3(-0.35f, -0.2f, 0);         //총위치 캐릭터 왼쪽으로

            gunGateObj.transform.position = gunObj.transform.position + 
                new Vector3(0.5f * Mathf.Cos(plmv.angleZ * Mathf.Deg2Rad), 0.5f * Mathf.Sin(plmv.angleZ * Mathf.Deg2Rad)+0.2f);//총구 위치
            if(plmv.angleZ<-45 && plmv.angleZ>-135)
                gunGateObj.transform.position = gunObj.transform.position +
                new Vector3(0.5f * Mathf.Cos(plmv.angleZ * Mathf.Deg2Rad) -0.15f, 0.5f * Mathf.Sin(plmv.angleZ * Mathf.Deg2Rad) );//밑에 볼때 총구위치 조정
            else if (plmv.angleZ < -60 && plmv.angleZ > -120)
                gunGateObj.transform.position = gunObj.transform.position +
                new Vector3(0.5f * Mathf.Cos(plmv.angleZ * Mathf.Deg2Rad) + 0.3f, 0.5f * Mathf.Sin(plmv.angleZ * Mathf.Deg2Rad) + 0.5f);//위에 볼때 총구위치 조정


            childTransform.position = transform.position + new Vector3(-0.35f, -0.2f, 0);           //손위치
            childTransform.rotation = Quaternion.Euler(0, 0, 0);                                    //손회전 x
            if (!isRightHand)                                                               
            {
                RightHandObj = Instantiate(otherHandPrefab,
                    transform.position + new Vector3(0.35f, -0.2f, 0), Quaternion.Euler(0, 0, 0));  //오른손 생성
                isRightHand = true;
            }
            RightHandObj.transform.position = transform.position + new Vector3(+0.35f, -0.2f, 0);   //오른손 캐릭터에 붙어다니게
            Destroy(LeftHandObj);   //총에 손이 있어서 원래 있던 손 삭제
            isLeftHand = false;

            if (GetComponent<PlayerController>().inlobby)   //로비에서 손2개 생성
            {
                if (!isLeftHand)
                {
                    LeftHandObj = Instantiate(otherHandPrefab,
                        transform.position + new Vector3(-0.35f, -0.2f, 0), Quaternion.Euler(0, 0, 0)); //왼손 생성
                    isLeftHand = true;
                }
                LeftHandObj.transform.position = transform.position + new Vector3(-0.35f, -0.2f, 0);    //왼손 캐릭터에 붙어다니게
            }                                                
        }

        // 우선순위 판정
        if (plmv.angleZ > 30 && plmv.angleZ < 150)  // 윗방향
        {
            // 총,활 우선순위
            gunSpr.sortingOrder = 1;
            handSpr.sortingOrder = 1;
            if(RightHandObj)
                RightHandObj.GetComponent<SpriteRenderer>().sortingOrder = 1;
            if (LeftHandObj)
                LeftHandObj.GetComponent<SpriteRenderer>().sortingOrder = 1;
        }
        else
        {
            gunSpr.sortingOrder = 5;    //캐릭터 OrderInLayer == 4
            handSpr.sortingOrder = 6;   //손이 총보다위
            if (RightHandObj)
                RightHandObj.GetComponent<SpriteRenderer>().sortingOrder = 6;
            if (LeftHandObj)
                LeftHandObj.GetComponent<SpriteRenderer>().sortingOrder = 6;
        }
        if (GetComponent<PlayerController>().gameState == "falling")    //떨어지고 있을때
        {
            Destroy(RightHandObj);                                                                  //오른손 삭제
            isRightHand = false;
            Destroy(LeftHandObj);                                                                   //왼손 삭제
            isLeftHand = false;
        }
                
        if (GetComponent<PlayerController>().isDodging) // 구를때
        {
            Destroy(RightHandObj);                                      //오른손 삭제
            isRightHand = false;
            Destroy(LeftHandObj);                                       //왼손 삭제
            isLeftHand = false;
            if (gunObj)
            {
                gunObj.GetComponent<SpriteRenderer>().enabled = false;  //마우스 오른쪽 입력시 총안보이게
            }
        }
        else
        {
            if (gunObj)
            {
                gunObj.GetComponent<SpriteRenderer>().enabled = true;
            }
        }

        // 마우스 왼클릭시 공격
        if (Input.GetMouseButton(0) && canAttack&&!inlobby &&!isReloading)
        {            
            if(gunNumber ==1)
            {
                if(pilotGunBulletCount > 0) 
                {                    
                    // 공격 키 입력 및 딜레이 시작
                    StartCoroutine(AttackWithDelay());

                }
            }
            else if(gunNumber ==2) 
            {
                if(redGunBulletCount > 0)
                {
                    // 공격 키 입력 및 딜레이 시작
                    StartCoroutine(AttackWithDelay());
                }
            }            
        }

        // 총알 다썼을때
        if (!isReloadText)
        {
            if (gunNumber ==1) 
            {
                if(pilotGunBulletCount == 0)
                {
                    // 재장전 문구 생성
                    ReloadText = Instantiate(reloadText, transform.position + new Vector3(-0.5f, 0.9f, 0), transform.rotation);
                    ReloadText.transform.SetParent(transform);  // 플레이어 따라다니게 자식으로 설정
                    isReloadText = true;
                    // 텍스트 깜빡거리게 하기
                    textMesh = ReloadText.GetComponent<TextMesh>();
                    textColor = textMesh.color;
                    Invoke("TextInVisible", 1f);
                }
            }
            else if (gunNumber == 2)
            {
                if (redGunBulletCount == 0)
                {
                    // 재장전 문구 생성
                    ReloadText = Instantiate(reloadText, transform.position + new Vector3(-0.5f, 0.9f, 0), transform.rotation);
                    ReloadText.transform.SetParent(transform);  // 플레이어 따라다니게 자식으로 설정
                    isReloadText = true;
                    // 텍스트 깜빡거리게 하기
                    textMesh = ReloadText.GetComponent<TextMesh>();
                    textColor = textMesh.color;
                    Invoke("TextInVisible", 1f);
                }
            }
        }


        // 총알 다썼을 때 마우스 왼클릭시 장전
        if (Input.GetMouseButtonDown(0) && canAttack && !inlobby)
        {
            if(gunNumber==1) 
            {
                if(pilotGunBulletCount == 0)
                    Reload();
            }
            else if (gunNumber == 2)
            {
                if (redGunBulletCount == 0)
                    Reload();
            }
        }

        // 마우스 왼클릭을 땔때
        if (Input.GetMouseButtonUp(0) && !inlobby &&!isReloading)
        {

            if (gunNumber == 1)
                gunAnimator.Play(pilotGunReturn);
            else if (gunNumber == 2)
                gunAnimator.Play("RedGunHold");
        }
        
        // R키누르면 장전
        if (Input.GetKeyDown(KeyCode.R) && !inlobby && !isReloading)
        {
            if (gunNumber == 1)
            {
                if (pilotGunBulletCount < 8)
                    Reload();
            }
            else if (gunNumber == 2)
            {
                if (redGunBulletCount < 20)
                    Reload();
            }
        }

        if(ReloadBar)
        {
            // ReloadBar를 오른쪽으로 이동
            float barspeed = 0.8f;
            ReloadBar.transform.Translate(Vector3.right * barspeed * Time.deltaTime, Space.Self);
        }        

        // Q눌러서 공포탄 사용
        if(Input.GetKeyDown(KeyCode.Q) && !inlobby && ItemData.hasBlankBullets>0)
        {
            ItemData.hasBlankBullets--;
            Vector2 blankBulletPos = new Vector2(transform.position.x -4.7f,transform.position.y - 4.7f);   //공포탄 생성위치
            GameObject blankBulletInstance = Instantiate(blankBullet, blankBulletPos, transform.rotation);  //공포탄 객체생성,객체 변수
            GameObject.Find("BlankBullet(Clone)").GetComponent<Animator>().Play("BlankBulletFire");
            Destroy(blankBulletInstance, 2.0f);

            GameObject[] enemyBullets = GameObject.FindGameObjectsWithTag("EnemyBullet");   //"EnemyBullet"태그있는 오브젝트들 찾기
            foreach (GameObject enemyBullet in enemyBullets)      //배열들 하나마다
            {
                Destroy(enemyBullet);
            }
        }
    }

    void FixedUpdate()
    {        
    }

    public void InstantiateGun()
    {
        // 총을 플레이어 위치에 배치
        if (gunNumber == 1)
        {
            Vector3 pos = transform.position;
            gunObj = Instantiate(pilotGunPrefab, pos, Quaternion.identity);
            gunObj.transform.SetParent(transform);  //플레이어 객체를 총 객체의 부모로 설정
        }
        if (gunNumber == 2)
        {
            Vector3 pos = transform.position;
            gunObj = Instantiate(redGunPrefab, pos, Quaternion.identity);
            gunObj.transform.SetParent(transform);  //플레이어 객체를 총 객체의 부모로 설정
        }

        // 포구에 배치한 오브젝트 가져오기
        Transform tr = gunObj.transform.Find("GunGate");
        gunGateObj = tr.gameObject;

        // 애니메이터 가져오기
        gunAnimator = gunObj.GetComponent<Animator>();

        // (기본) 애니메이션 설정
        if (gunNumber == 1)
            gunAnimator.Play(pilotGunHold);
        else if (gunNumber == 2)
            gunAnimator.Play("RedGunHold");

        if (gunNumber == 1)
        {
            shootDelay = 0.5f;
        }
        else if (gunNumber == 2)
        {
            shootDelay = 0.2f;
        }
    }

    IEnumerator AttackWithDelay()
    {
        Attack();

        // 딜레이 설정 
        canAttack = false;
        yield return new WaitForSeconds(shootDelay);
        // 딜레이 후에 다시 공격 가능으로 설정
        canAttack = true;
    }

    // 총알 발사
    public void Attack()
    {
        if(gunNumber == 1)
        {
            pilotGunBulletCount--;
            gunAnimator.Play(pilotGunFire, 0, 0f);  // 애니메이션 키포인트 처음으로 이동후 실행
        }
        else if(gunNumber == 2)
        {
            redGunBulletCount--;
            gunAnimator.Play("RedGunFire", 0, 0f);
        }
                    
        PlayerController playerCnt = GetComponent<PlayerController>();
        // 회전에 사용할 각도
        float angleZ = playerCnt.angleZ;        

        // 총알 생성 위치
        Vector3 pos = new Vector3(gunGateObj.transform.position.x,
                                          gunGateObj.transform.position.y,
                                          transform.position.z);
                
        // 총알 오브젝트 생성 (캐릭터 진행 방향으로 회전)
        Quaternion r = Quaternion.Euler(0, 0, angleZ + 90 );     //총알 자체 각도
        if(gunNumber ==1)
            bulletObj = Instantiate(pilotGunBulletPrefab, pos, r);
        else if(gunNumber==2)
            bulletObj = Instantiate(redGunBulletPrefab, pos, r);

        // 총 쏠때 이펙트 생성
        if (gunNumber == 1)
        {
            pilotgunFireEffectObj = Instantiate(pilotgunFireEffectPrefab, pos, r);
            Destroy(pilotgunFireEffectObj,0.5f);
        }            
        else if (gunNumber == 2)
        {
            // redgun은 효과 안넣음
        }
        
        // -5~5사이 랜덤으로 더할 각도
        int randomInt = Random.Range(-5, 5);

        // 총알을 발사하기 위한 벡터 생성
        Vector3 gateToMouseVec;
        float directionX;
        float directionY;

        // 총알 마우스 포인터 방향으로 날아가게 조정
        if (angleZ < 0 && angleZ > -90)         // 4사분면      
        {
            directionX = Mathf.Cos((angleZ-4 + randomInt) * Mathf.Deg2Rad);
            directionY = Mathf.Sin((angleZ-4 + randomInt) * Mathf.Deg2Rad);
        }
        else if (angleZ < -135 && angleZ > -180) // 3사분면 위쪽  
        {
            directionX = Mathf.Cos((angleZ  + randomInt) * Mathf.Deg2Rad);
            directionY = Mathf.Sin((angleZ  + randomInt) * Mathf.Deg2Rad);
        }
        else if (angleZ < -90 && angleZ > -135) // 3사분면 아래쪽 
        {
            directionX = Mathf.Cos((angleZ+5 + randomInt) * Mathf.Deg2Rad);
            directionY = Mathf.Sin((angleZ+5 + randomInt) * Mathf.Deg2Rad);
        }
        else if (angleZ > 60 && angleZ < 90)          // 1사분면 위쪽    
        {
            directionX = Mathf.Cos((angleZ +8 + randomInt) * Mathf.Deg2Rad);
            directionY = Mathf.Sin((angleZ +8 + randomInt) * Mathf.Deg2Rad);
        }
        else if (angleZ > 0 && angleZ < 60)          // 1사분면 아래쪽   
        {
            directionX = Mathf.Cos((angleZ  + randomInt) * Mathf.Deg2Rad);
            directionY = Mathf.Sin((angleZ  + randomInt) * Mathf.Deg2Rad);
        }
        else if (angleZ > 90 && angleZ < 135)        // 2사분면 위쪽
        {
            directionX = Mathf.Cos((angleZ -3 + randomInt) * Mathf.Deg2Rad);
            directionY = Mathf.Sin((angleZ -3 + randomInt) * Mathf.Deg2Rad);
        }
        else if (angleZ >= 135 && angleZ < 180)    // 2사분면 아래쪽
        {
            directionX = Mathf.Cos((angleZ  + randomInt) * Mathf.Deg2Rad);
            directionY = Mathf.Sin((angleZ  + randomInt) * Mathf.Deg2Rad);
        }
        else                                    // 0,180,90,-90
        {
            directionX = Mathf.Cos((angleZ + randomInt) * Mathf.Deg2Rad);
            directionY = Mathf.Sin((angleZ + randomInt) * Mathf.Deg2Rad);
        }
        gateToMouseVec = new Vector3(directionX, directionY) * shootSpeed;
        // 지정한 각도와 방향으로 화살을 발사
        Rigidbody2D body = bulletObj.GetComponent<Rigidbody2D>();
        body.AddForce(gateToMouseVec, ForceMode2D.Impulse);

    }       

    void TextVisible()
    {
        textColor.a =1;
        if(ReloadText)
        {
            ReloadText.GetComponent<TextMesh>().color = textColor;
        }
        Invoke("TextInVisible", 1f);
    }
    void TextInVisible()
    {
        textColor.a = 0;
        if (ReloadText)
        {
            ReloadText.GetComponent<TextMesh>().color = textColor;
        }
        Invoke("TextVisible", 1f);
    }

    void Reload()
    {
        isReloading = true;
        canAttack = false;
        Destroy(ReloadText);
        isReloadText = false;

        if (gunNumber == 1)
            gunAnimator.Play(pilotGunReload, 0, 0f);    // 애니메이션 키포인트 처음으로 이동후 실행
        else if (gunNumber == 2)
            gunAnimator.Play("RedGunReload", 0, 0f);

        ReloadBack = Instantiate(reloadBack, transform.position + new Vector3(-0.1f, 0.6f, 0), transform.rotation);   //긴막대 생성            
        ReloadBar = Instantiate(reloadBar, transform.position + new Vector3(-0.5f, 0.64f, 0), transform.rotation);   //짧은막대 생성
        ReloadBack.transform.SetParent(transform);  //플레이어 따라다니게
        ReloadBar.transform.SetParent(transform);   //플레이어 객체를 총 객체의 부모로 설정
        Destroy(ReloadBack, 1.3f);
        Destroy(ReloadBar, 1.3f);        //1.3초뒤 삭제
        Invoke("ChangeVariable", 1.3f);  //1.3초뒤 isReloading=false ,canAttack =true로 바꾸는함수 실행
        if(gunNumber==1)
            pilotGunBulletCount = 8;    //이거 이벤트함수 써서 장전 중간에 끊기면 장전안되게 위에 줄도 거기 넣으면될듯
        if (gunNumber == 2)
            redGunBulletCount = 20;
    }

    public void ChangeVariable()
    {
        isReloading = false;
        canAttack = true;
    }
}


/*

시계총맞고 검은 배경에 GameOver 이후 타이틀화면 or 재시작
-ui버튼만들고 sceneChange,,


총쏘면 화면 흔들리기
(흔들림 수준 설정 옵션)

약간 자잘한거들
-
빨간총 최대 탄창수 설정 ,장전하면 남은총알고려 계산

소모 아이템 구현 - 탄약상자, 하트, 공포탄 

상점구현 ,돈소모


자잘한거들

장전도중에 총바꾸면 장전 취소, 장전 끝나면 총알 다차게
죽으면 어두워지는거 포스트 프로세싱,,

 */